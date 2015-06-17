using DemoApp.Common.Interfaces;
using DemoApp.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Common.Services
{
    public class MyJobsService : IMyJobsService
    {
        const string MyJobsKey = "myJobsList";

        ILocalDataService _localData;

        public MyJobsService(ILocalDataService localData)
        {
            _localData = localData;
        }

        public void AddToMyJobs(Models.Job job)
        {
            var myJobs = GetMyJobs().ToList();

            if (!myJobs.Any(a => a.JobId == job.JobId))
            {
                myJobs.Add(job);
                _localData.SetValue(MyJobsKey, JsonConvert.SerializeObject(myJobs));
            }
        }

        public Models.Job[] GetMyJobs()
        {
            var jobString = _localData.GetValue(MyJobsKey);

            if (jobString != null)
            {
                return JsonConvert.DeserializeObject<Job[]>(jobString);
            }
            
            return new Job[0];
        }
    }
}
