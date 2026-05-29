using System;
using System.Threading;
using BootFlow.StateMachine;
using BootFlow.UI;
using Cysharp.Threading.Tasks;
using R3;

namespace BootFlow.Boot.States
{
    public sealed class MenuState : IState
    {
        private readonly IUIScreenFactory _screenFactory;
        private readonly MenuUIViewModel _viewModel;
        private readonly IBootStateTransition _transition;
        private readonly CompositeDisposable _bindings = new CompositeDisposable();
        private CancellationToken _stateLifetimeToken;
        private bool _isRestarting;

        public MenuState(
            IUIScreenFactory screenFactory,
            MenuUIViewModel viewModel,
            IBootStateTransition transition)
        {
            _screenFactory = screenFactory;
            _viewModel = viewModel;
            _transition = transition;
        }

        public UniTask EnterAsync(CancellationToken cancellationToken)
        {
            _isRestarting = false;
            _stateLifetimeToken = cancellationToken;
            _screenFactory.Show(UIScreenCode.Menu);
            _viewModel.RestartRequested.Subscribe(_ => RestartAsync().Forget()).AddTo(_bindings);
            return UniTask.CompletedTask;
        }

        public UniTask ExitAsync(CancellationToken cancellationToken)
        {
            _bindings.Clear();
            _screenFactory.HideCurrent();
            return UniTask.CompletedTask;
        }

        private async UniTaskVoid RestartAsync()
        {
            if (_isRestarting)
            {
                return;
            }

            _isRestarting = true;

            try
            {
                await _transition.EnterStateAsync(BootStateCode.Load, _stateLifetimeToken);
            }
            catch (OperationCanceledException) when (_stateLifetimeToken.IsCancellationRequested)
            {
            }
        }
    }
}
