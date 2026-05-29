using System;
using System.Threading;
using BootFlow.StateMachine;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace BootFlow.Boot
{
    public sealed class BootFlowEntryPoint : IStartable, IDisposable
    {
        private readonly IStatesController<BootStateCode> _statesController;
        private readonly CancellationTokenSource _lifetimeCts = new CancellationTokenSource();

        public BootFlowEntryPoint(IStatesController<BootStateCode> statesController)
        {
            _statesController = statesController;
        }

        public void Start()
        {
            StartAsync(_lifetimeCts.Token).Forget();
        }

        public void Dispose()
        {
            _lifetimeCts.Cancel();
            _lifetimeCts.Dispose();
        }

        private async UniTaskVoid StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _statesController.EnterStateAsync(BootStateCode.Splash, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
            }
        }
    }
}
