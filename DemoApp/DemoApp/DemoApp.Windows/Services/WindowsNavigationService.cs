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
    }
}
