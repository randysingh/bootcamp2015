using Caliburn.Micro;
using INavigationService = DemoApp.Common.INavigationService;

namespace DemoApp.Windows.Services
{
    public class WindowsNavigationService : INavigationService
    {
        private readonly Caliburn.Micro.INavigationService _navigationService;

        public WindowsNavigationService(Caliburn.Micro.INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateToViewModel<T>()
        {
            _navigationService.NavigateToViewModel<T>();
        }

        public void NavigateToViewModel<T>(object parameter)
        {
            _navigationService.NavigateToViewModel<T>(parameter);
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
