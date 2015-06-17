using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Common.Models
{
    public class Job
    {
        [JsonProperty("organization_name")]
        public string Organization { get; set; }
        [JsonProperty("position_title")]
        public string Title { get; set; }
        [JsonProperty("minimum")]
        public double MinSalary { get; set; }
        [JsonProperty("maximum")]
        public double MaxSalary { get; set; }

        [JsonProperty("id")]
        public string JobId { get; set; }
    }
}
