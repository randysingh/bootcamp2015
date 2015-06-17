using System.Windows.Input;
using Caliburn.Micro;
using DemoApp.Common.Common;
using DemoApp.Common.Interfaces;

namespace DemoApp.Common.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        public string Name { get; set; }

        public ICommand GoToPictureViewCommand
        {
            get { return new DelegateCommand(() => _navigationService.NavigateToViewModel<PictureViewModel>()) ; }
        }

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
