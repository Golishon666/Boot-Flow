using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace BootFlow.StateMachine
{
    public sealed class StatesController<TStateCode> : IStatesController<TStateCode>
    {
        private readonly IReadOnlyDictionary<TStateCode, IState> _states;
        private readonly IEqualityComparer<TStateCode> _codeComparer;
        private IState _currentState;
        private TStateCode _currentCode;
        private bool _hasCurrentState;

        public StatesController(
            IReadOnlyDictionary<TStateCode, IState> states,
            IEqualityComparer<TStateCode> codeComparer = null)
        {
            _states = states ?? throw new ArgumentNullException(nameof(states));
            _codeComparer = codeComparer ?? EqualityComparer<TStateCode>.Default;
        }

        public async UniTask EnterStateAsync(TStateCode code, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!_states.TryGetValue(code, out var nextState))
            {
                throw new KeyNotFoundException($"State '{code}' is not registered.");
            }

            if (_hasCurrentState && _codeComparer.Equals(_currentCode, code))
            {
                return;
            }

            if (_currentState != null)
            {
                await _currentState.ExitAsync(cancellationToken);
            }

            _currentState = nextState;
            _currentCode = code;
            _hasCurrentState = true;

            await nextState.EnterAsync(cancellationToken);
        }
    }
}
