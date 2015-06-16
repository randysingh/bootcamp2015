namespace DemoApp.Common
{
    public interface INavigationService
    {
        void NavigateToViewModel<T>();

        void NavigateToViewModel<T>(object parameter);

        void GoBack();
    }
}
