using DemoApp.Common.Interfaces;
using DemoApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Common.Services
{
    public class MyJobsService : IMyJobsService
    {
        ILocalDataService _localData;

        public MyJobsService(ILocalDataService localData)
        {
            _localData = localData;
        }

        public void AddToMyJobs(Models.Job job)
        {
            //TODO: add to my jobs if same job id not there
            //update my jobs list in local data service
            throw new NotImplementedException();
        }

        public Models.Job[] GetMyJobs()
        {
            //TODO: get my jobs list in local data service.
            //throw new NotImplementedException();
            return new Job[0];
        }
    }
}
