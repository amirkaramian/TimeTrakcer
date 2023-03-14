using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Model
{

    public class ApiResponse<T>
    {
        public List<T> dataList { get; set; }
        public object data { get; set; }
        public int id { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public object exception { get; set; }

       
    }
}
