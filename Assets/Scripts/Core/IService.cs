using System.Threading;
using Cysharp.Threading.Tasks;

namespace BootFlow.Core
{
    public interface IService
    {
        UniTask InitializeAsync(CancellationToken cancellationToken);
        UniTask ReleaseAsync(CancellationToken cancellationToken);
    }
}
