using Caliburn.Micro;
using DemoApp.Common.Interfaces;

namespace DemoApp.Common.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        public string Name { get; set; }

        public MainViewModel()
        {
            Name = "Hello from portable";
        }
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Name = "Hello from portable";
        }
    }
}
