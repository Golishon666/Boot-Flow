using System;
using R3;

namespace BootFlow.UI
{
    public sealed class MenuUIViewModel : IUIViewModel, IDisposable
    {
        private readonly Subject<Unit> _restartRequested = new Subject<Unit>();

        public Observable<Unit> RestartRequested => _restartRequested;

        public void RequestRestart()
        {
            _restartRequested.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _restartRequested.Dispose();
        }
    }
}
