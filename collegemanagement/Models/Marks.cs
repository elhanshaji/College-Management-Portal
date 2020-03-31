using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace collegemanagement.Models
{
    public class Marks
    {
        public List<SelectListItem> Students = new List<SelectListItem>();
        public int stdid { get; set; }
        public int year { get; set; }
        public int sem { get; set; }
        public List<SelectListItem> Batchs = new List<SelectListItem>();
        public int batchid { get; set; }
        public List<Markdetail> ListofMark = new List<Markdetail>();
    }
}