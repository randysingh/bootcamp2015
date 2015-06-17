using System;
using Caliburn.Micro;

namespace DemoApp.Windows.Services
{
    public class WindowsNavigationService : Common.Interfaces.INavigationService
    {
        private readonly INavigationService _navigationService;
        public WindowsNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateToViewModel<T>()
        {
            _navigationService.NavigateToViewModel<T>();
        }

        public void GoBack()
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
            else
            {
                throw new InvalidOperationException("Navigation Service is unable to go back!");
            }
        }
    }
}
