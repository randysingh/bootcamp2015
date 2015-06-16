using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using DemoApp.Common.Models;

namespace DemoApp.Common.Services
{
    /// <summary>
    /// TODO: put these code snippets into actual services!
    /// </summary>
    public class ServiceCodeSnippets
    {

        public Task<Job[]> GetJobsForQuery(string queryString)
        {
            return "http://api.usa.gov/jobs/search.json"
                .SetQueryParams(new { query = queryString })
                .GetJsonAsync<Job[]>();
        }

        public IEnumerable<Job> FilterJobsWithSalary(IEnumerable<Job> input, int minSalary, int maxSalary)
        {
            return input.Where(j => j.MaxSalary <= maxSalary && j.MinSalary >= minSalary);
        }
    }
}
