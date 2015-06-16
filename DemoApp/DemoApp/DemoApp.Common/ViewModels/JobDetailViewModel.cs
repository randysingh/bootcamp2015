using Caliburn.Micro;
using DemoApp.Common.Interfaces;
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
        IMyJobsService _myJobsService;

        public ICommand SaveJob { get; set; }

        public ICommand GoBack { get; set; }

        public Job Job { get; set; }

        /// <summary>
        /// This is only here so that caliburn micro will set it.
        /// </summary>
        public Job Parameter 
        { 
            set
            {
                Job = value;
            }
        }

        public JobDetailViewModel(INavigationService navigationService,
            IMyJobsService myJobsService
            )
        {
            _navigationService = navigationService;
            _myJobsService = myJobsService;

            GoBack = new DelegateCommand(a => _navigationService.GoBack());
            SaveJob = new DelegateCommand(a => OnSaveJob());
        }

        private void OnSaveJob()
        {
            _myJobsService.AddToMyJobs(Job);
            //TODO Disable the save job button
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //TODO detect if this job is part of my jobs, enable / disable button

        }
    }
}
