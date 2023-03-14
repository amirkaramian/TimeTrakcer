using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{
    public class UserModel
    {
        [JsonProperty("firstName")]
        public string Name { get; set; }
        [JsonProperty("lastName")]
        public string Family { get; set; }
        [JsonProperty("emailId")]
        public string Email { get; set; }
        [JsonProperty("mobileNo")]
        public string MobileNumber { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonIgnore]
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
    }
}
