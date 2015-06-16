using Caliburn.Micro;
using DemoApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoApp.Common.ViewModels
{
    public class JobDetailViewModel : Screen
    {
        INavigationService _navigationService;

        public ICommand SaveJob { get; set; }

        public ICommand GoBack { get; set; }

        public Job Job { get; set; }

        public Job Parameter 
        { 
            set
            {
                Job = value;
            }
        }

        public JobDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBack = new DelegateCommand(a => _navigationService.GoBack());
        }
    }
}
