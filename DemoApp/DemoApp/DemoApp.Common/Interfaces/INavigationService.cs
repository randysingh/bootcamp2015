namespace DemoApp.Common.Interfaces
{
    public interface INavigationService
    {
        void NavigateToViewModel<T>();
        void GoBack();
    }
}
