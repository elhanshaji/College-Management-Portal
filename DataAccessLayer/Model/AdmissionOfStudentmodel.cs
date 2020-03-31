using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
     public class AdmissionOfStudentmodel
    {
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        public string password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }
        [Required(ErrorMessage = "Please enter studentname")]
        public string studentname { get; set; }
        [Required(ErrorMessage = "Please enter gender")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Please enter age")]
        public int age { get; set; }
        [Required(ErrorMessage = "Please enter depname")]
        public string depname { get; set; }
    }
}
