using Caliburn.Micro;
using DemoApp.Common.Interfaces;
using DemoApp.Common.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DemoApp.Common.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IMyJobsService _myJobs;

        private ObservableCollection<JobViewModel> _myJobsList = new ObservableCollection<JobViewModel>();

        public ICommand NavigationDemo { get; set; }

        public MainViewModel(INavigationService navigationService,
            IMyJobsService myJobs
            )
        {
            _navigationService = navigationService;
            _myJobs = myJobs;
            DisplayName = "Job Search";

            var demoJob = new Job() { JobId = "foo"};
            NavigationDemo = new DelegateCommand(o => GoToJobDetail(demoJob));
        }

        protected void GoToJobDetail(Job job)
        {
            _navigationService.NavigateToViewModel<JobDetailViewModel>(job);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var jobsList = _myJobs.GetMyJobs();

            foreach (var job in jobsList)
            {
                var vm = new JobViewModel();
                vm.Job = job;

                _myJobsList.Add(vm);
            }
        }
    }
}
