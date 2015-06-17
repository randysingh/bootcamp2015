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
        private readonly IJobSearchService _jobSearchService;

        private ObservableCollection<JobViewModel> _myJobsList = new ObservableCollection<JobViewModel>();
        private ObservableCollection<JobViewModel> _searchResults = new ObservableCollection<JobViewModel>();
        private bool _isLoading;
        private string _queryString = string.Empty;
        private int _minSalary, _maxSalary;

        #region Bindable Properties

        /// <summary>
        /// TODO: Remove me - this is just an example of how to navigate to the jobs detail page.
        /// </summary>
        public ICommand NavigationDemo { get; set; }

        public ICommand StartJobsQuery { get; set; }

        public ObservableCollection<JobViewModel> SearchResults
        {
            get { return _searchResults; }
        }

        public ObservableCollection<JobViewModel> MyJobs
        {
            get { return _myJobsList; }
        }

        /// <summary>
        /// Set me to true while fetching results. set me to false when fetching done.
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; NotifyOfPropertyChange(() => IsLoading); }
        }

        public int MinSalary
        {
            get { return _minSalary; }
            set { _minSalary = value; NotifyOfPropertyChange(() => MinSalary); }
        }

        public int MaxSalary
        {
            get { return _maxSalary; }
            set { _maxSalary = value; NotifyOfPropertyChange(() => MaxSalary); }
        }

        public string QueryString
        {
            get { return _queryString; }
            set { _queryString = value; NotifyOfPropertyChange(() => QueryString); }
        }

        #endregion

        public MainViewModel(INavigationService navigationService,
            IMyJobsService myJobs,
            IJobSearchService jobSearchService
            )
        {
            _navigationService = navigationService;
            _myJobs = myJobs;
            _jobSearchService = jobSearchService;
            DisplayName = "Job Search";

            var demoJob = new Job() { JobId = "foo"};
            NavigationDemo = new DelegateCommand(o => GoToJobDetail(demoJob));

            StartJobsQuery = new DelegateCommand(o => OnStartJobsQuery());
        }

        public async Task OnStartJobsQuery()
        {
            IsLoading = true;

            var jobs = await _jobSearchService.QueryJobs(_queryString, _minSalary, _maxSalary);

            IsLoading = false;

            UpdateSearchResults(jobs);
        }

        private void UpdateSearchResults(Job[] jobs)
        {
            _searchResults.Clear();

            foreach (var job in jobs)
            {
                var vm = new JobViewModel() { Job = job };
                vm.Select = new DelegateCommand(a => GoToJobDetail(vm.Job));

                _searchResults.Add(vm);
            }
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
                vm.Select = new DelegateCommand(a => GoToJobDetail(vm.Job));

                _myJobsList.Add(vm);
            }
        }
    }
}
