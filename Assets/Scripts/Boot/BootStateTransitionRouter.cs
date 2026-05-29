using System;
using System.Threading;
using BootFlow.StateMachine;
using Cysharp.Threading.Tasks;

namespace BootFlow.Boot
{
    public sealed class BootStateTransitionRouter : IBootStateTransition
    {
        private IStatesController<BootStateCode> _statesController;

        public void Bind(IStatesController<BootStateCode> statesController)
        {
            _statesController = statesController ?? throw new ArgumentNullException(nameof(statesController));
        }

        public UniTask EnterStateAsync(BootStateCode stateCode, CancellationToken cancellationToken)
        {
            if (_statesController == null)
            {
                throw new InvalidOperationException("Boot state transition router is not bound to a states controller.");
            }

            return _statesController.EnterStateAsync(stateCode, cancellationToken);
        }
    }
}
