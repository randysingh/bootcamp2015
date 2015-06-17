using DemoApp.Common.Interfaces;
using DemoApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;

namespace DemoApp.Common.Services
{
    public class JobSearchService : IJobSearchService
    {
        public async Task<Models.Job[]> QueryJobs(string query, int minSalary, int maxSalary)
        {
            var jobs = await GetJobsForQuery(query);

            return FilterJobsWithSalary(jobs, minSalary, maxSalary).ToArray();
        }

        private Task<Job[]> GetJobsForQuery(string queryString)
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
