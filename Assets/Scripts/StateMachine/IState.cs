using System.Threading;
using Cysharp.Threading.Tasks;

namespace BootFlow.StateMachine
{
    public interface IState
    {
        UniTask EnterAsync(CancellationToken cancellationToken);
        UniTask ExitAsync(CancellationToken cancellationToken);
    }
}
