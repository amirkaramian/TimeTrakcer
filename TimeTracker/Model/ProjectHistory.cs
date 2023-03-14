using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{
    public class ProjectHistory
    {
        public int Id { get; set; }
        public int projectId { get; set; }
        public int workerId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        [JsonProperty("mouseClick")] 
        public int ClickCount { get; set; }
        [JsonProperty("keyboardClick")]
        public int KeyboardCount { get; set; }
        public int UserId { get; set; }
        [JsonProperty("projectActivityActions")]
        public List<ProjectAction> ProjectActions { get; set; }
    }
}
