using Caliburn.Micro;
using DemoApp.Common.Interfaces;
using DemoApp.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoApp.Common.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IMyJobsService _myJobs;

        private ObservableCollection<JobViewModel> _myJobsList = new ObservableCollection<JobViewModel>();
        private bool _isLoading;

        #region Bindable Properties

        /// <summary>
        /// TODO: Remove me - this is just an example of how to navigate to the jobs detail page.
        /// </summary>
        public ICommand NavigationDemo { get; set; }

        public ICommand StartJobsQuery { get; set; }

        /// <summary>
        /// Set me to true while fetching results. set me to false when fetching done.
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; NotifyOfPropertyChange(() => IsLoading); }
        }

        #endregion

        public MainViewModel(INavigationService navigationService,
            IMyJobsService myJobs
            )
        {
            _navigationService = navigationService;
            _myJobs = myJobs;
            DisplayName = "Job Search";

            var demoJob = new Job() { JobId = "foo"};
            NavigationDemo = new DelegateCommand(o => GoToJobDetail(demoJob));

            StartJobsQuery = new DelegateCommand(o => OnStartJobsQuery());
        }

        public async Task OnStartJobsQuery()
        {
            IsLoading = true;

            //TODO use some service to get the jobs results.
            //await ....

            IsLoading = false;
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
