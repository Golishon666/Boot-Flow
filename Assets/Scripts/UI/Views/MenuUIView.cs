using R3;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BootFlow.UI.Views
{
    public sealed class MenuUIView : UIView<MenuUIViewModel>
    {
        public Button RestartButton;

        protected override void OnInitialize(CompositeDisposable bindings)
        {
            if (RestartButton == null)
            {
                return;
            }

            UnityAction restartAction = ViewModel.RequestRestart;
            RestartButton.onClick.AddListener(restartAction);
            bindings.Add(Disposable.Create((RestartButton, restartAction), static state =>
            {
                if (state.RestartButton != null)
                {
                    state.RestartButton.onClick.RemoveListener(state.restartAction);
                }
            }));
        }
    }
}
