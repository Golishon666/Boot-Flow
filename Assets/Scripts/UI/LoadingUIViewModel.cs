using BootFlow.Boot.States;
using R3;

namespace BootFlow.UI
{
    public sealed class LoadingUIViewModel : IUIViewModel
    {
        private readonly ILoadProgressProvider _progressProvider;

        public LoadingUIViewModel(ILoadProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
        }

        public float CurrentProgress => _progressProvider.CurrentProgress;
        public Observable<float> ProgressChanged => _progressProvider.ProgressChanged;
    }
}
