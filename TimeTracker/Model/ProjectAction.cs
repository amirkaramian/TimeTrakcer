using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{
    public class ProjectAction
    {
        [JsonProperty("mouseClick")]
        public int ClickCount { get; set; }
        [JsonProperty("keyboardClick")]
        public int KeyboardCount { get; set; }
        [JsonProperty("keyBoardStock")]
        public string keyboardStock { get; set; }
        public DateTime Time { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserId { get; set; }
    }
}
