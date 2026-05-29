using R3;

namespace BootFlow.Boot.States
{
    public interface ILoadProgressProvider
    {
        float CurrentProgress { get; }
        Observable<float> ProgressChanged { get; }
    }
}
