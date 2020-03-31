using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class SendNotificationmodel
    {
        [Required(ErrorMessage = "Please choose the required recipient class.")]
        public string ToRoleID { get; set; }
        [Required(ErrorMessage = "Please enter the required message.")]
        public string Message { get; set; }
    }
}
