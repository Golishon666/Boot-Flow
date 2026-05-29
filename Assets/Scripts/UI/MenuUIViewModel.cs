using R3;

namespace BootFlow.UI
{
    public sealed class MenuUIViewModel : IUIViewModel
    {
        private readonly Subject<Unit> _restartRequested = new Subject<Unit>();

        public Observable<Unit> RestartRequested => _restartRequested;

        public void RequestRestart()
        {
            _restartRequested.OnNext(Unit.Default);
        }
    }
}
