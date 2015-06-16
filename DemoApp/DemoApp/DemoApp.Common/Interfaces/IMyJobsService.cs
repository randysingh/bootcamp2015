using DemoApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Common.Interfaces
{
    /// <summary>
    /// Manages the list of jobs
    /// </summary>
    public interface IMyJobsService
    {
        void AddToMyJobs(Job job);

        Job[] GetMyJobs();
    }
}
