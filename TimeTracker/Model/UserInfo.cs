using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public int ScreenshotTimeinterval { get; set; }
        public int WorkerId { get; set; }
        public int AgencyId { get; set; }
        public string RoleName { get; set; }
    }

}
