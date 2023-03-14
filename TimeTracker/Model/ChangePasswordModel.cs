using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
