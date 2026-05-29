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
        private readonly IBootStateTransition _transition;

        public SplashState(
            BootFlowSettings settings,
            IUIScreenFactory screenFactory,
            IBootStateTransition transition)
        {
            _settings = settings;
            _screenFactory = screenFactory;
            _transition = transition;
        }

        public async UniTask EnterAsync(CancellationToken cancellationToken)
        {
            _screenFactory.Show(UIScreenCode.Splash);
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.SplashSeconds), cancellationToken: cancellationToken);
            await _transition.EnterStateAsync(BootStateCode.Load, cancellationToken);
        }

        public UniTask ExitAsync(CancellationToken cancellationToken)
        {
            _screenFactory.HideCurrent();
            return UniTask.CompletedTask;
        }
    }
}
