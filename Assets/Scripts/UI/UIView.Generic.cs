using VContainer;

namespace BootFlow.UI
{
    public abstract class UIView<TViewModel> : UIView
        where TViewModel : IUIViewModel
    {
        protected TViewModel ViewModel { get; private set; }

        [Inject]
        public void Construct(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
