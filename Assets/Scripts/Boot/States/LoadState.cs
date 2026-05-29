using System;
using System.Threading;
using BootFlow.StateMachine;
using BootFlow.UI;
using Cysharp.Threading.Tasks;
using R3;

namespace BootFlow.Boot.States
{
    public sealed class LoadState : IState, ILoadProgressProvider, IDisposable
    {
        private readonly BootFlowSettings _settings;
        private readonly IUIScreenFactory _screenFactory;
        private readonly Lazy<IStatesController<BootStateCode>> _statesController;
        private readonly ReactiveProperty<float> _progress = new ReactiveProperty<float>(0f);

        public LoadState(
            BootFlowSettings settings,
            IUIScreenFactory screenFactory,
            Lazy<IStatesController<BootStateCode>> statesController)
        {
            _settings = settings;
            _screenFactory = screenFactory;
            _statesController = statesController;
        }

        public float CurrentProgress => _progress.CurrentValue;
        public Observable<float> ProgressChanged => _progress;

        public async UniTask EnterAsync(CancellationToken cancellationToken)
        {
            _progress.Value = 0f;
            _screenFactory.Show(UIScreenCode.Loading);

            for (var step = 1; step <= _settings.LoadSteps; step++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_settings.LoadStepSeconds), cancellationToken: cancellationToken);
                _progress.Value = (float)step / _settings.LoadSteps;
            }

            await _statesController.Value.EnterStateAsync(BootStateCode.Menu, cancellationToken);
        }

        public UniTask ExitAsync(CancellationToken cancellationToken)
        {
            _screenFactory.HideCurrent();
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _progress.Dispose();
        }
    }
}
