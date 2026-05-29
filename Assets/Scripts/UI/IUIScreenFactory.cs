namespace BootFlow.UI
{
    public interface IUIScreenFactory
    {
        UIView Show(UIScreenCode screenCode);
        void HideCurrent();
    }
}
