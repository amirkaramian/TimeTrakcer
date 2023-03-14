using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{
    public class ApplicationList
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("operatingSystem")]
        public string OperatingSystem { get; set; }
    }
}
