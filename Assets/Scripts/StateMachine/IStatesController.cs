using System.Threading;
using Cysharp.Threading.Tasks;

namespace BootFlow.StateMachine
{
    public interface IStatesController<TStateCode>
    {
        UniTask EnterStateAsync(TStateCode code, CancellationToken cancellationToken);
    }
}
