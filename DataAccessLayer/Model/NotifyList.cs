using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
     public class NotifyList
    {
        public string FromUser { get; set; }
        public string RoleName { get; set; }
        public DateTime Date_Time { get; set; }
        public string Content { get; set; }
    }
}
