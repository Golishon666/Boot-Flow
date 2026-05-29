using System.Threading;
using Cysharp.Threading.Tasks;

namespace BootFlow.Boot
{
    public interface IBootStateTransition
    {
        UniTask EnterStateAsync(BootStateCode stateCode, CancellationToken cancellationToken);
    }
}
