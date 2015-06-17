using DemoApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Common.Interfaces
{
    public interface IJobSearchService
    {
        Task<Job[]> QueryJobs(string query, int minSalary, int maxSalary);
    }
}
