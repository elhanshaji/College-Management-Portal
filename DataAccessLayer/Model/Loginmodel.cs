using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Loginmodel
    {
        [Required]
        [StringLength(30)]
        public string username { get; set; }
        [Required]
        [StringLength(30)]
        public string password { get; set; }
    }
}
