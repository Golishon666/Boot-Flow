using System;
using System.Threading;
using BootFlow.StateMachine;
using BootFlow.UI;
using Cysharp.Threading.Tasks;

namespace BootFlow.Boot.States
{
    public sealed class SplashState : IState
    {
        private readonly BootFlowSettings _settings;
        private readonly IUIScreenFactory _screenFactory;
        private readonly Lazy<IStatesController<BootStateCode>> _statesController;

        public SplashState(
            BootFlowSettings settings,
            IUIScreenFactory screenFactory,
            Lazy<IStatesController<BootStateCode>> statesController)
        {
            _settings = settings;
            _screenFactory = screenFactory;
            _statesController = statesController;
        }

        public async UniTask EnterAsync(CancellationToken cancellationToken)
        {
            _screenFactory.Show(UIScreenCode.Splash);
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.SplashSeconds), cancellationToken: cancellationToken);
            await _statesController.Value.EnterStateAsync(BootStateCode.Load, cancellationToken);
        }

        public UniTask ExitAsync(CancellationToken cancellationToken)
        {
            _screenFactory.HideCurrent();
            return UniTask.CompletedTask;
        }
    }
}
