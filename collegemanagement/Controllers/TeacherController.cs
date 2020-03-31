using collegemanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Model;

namespace collegemanagement.Controllers
{
    public class TeacherController : Controller
    {
        
        // GET: Teacher
        public ActionResult MarkView()
        {
            try
            {
                Marks MarkModel = new Marks();
                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Teacher" || role=="HOD")
                {
                    var student = Data.students(j);
                    foreach (var n in student)
                    {

                        MarkModel.Students.Add(new SelectListItem() { Text = n.stName, Value = n.stid.ToString() });
                    }
                    SelectListItem[] YearList = new SelectListItem[3];
                    YearList[0] = new SelectListItem() { Text = "2019", Value = "2019" };
                    YearList[1] = new SelectListItem() { Text = "2020", Value = "2020" };
                    YearList[2] = new SelectListItem() { Text = "2021", Value = "2021" };
                    ViewBag.Yearlist = YearList;
                    SelectListItem[] SemList = new SelectListItem[2];
                    SemList[0] = new SelectListItem() { Text = "First", Value = "1" };
                    SemList[1] = new SelectListItem() { Text = "Second", Value = "2" };
                    ViewBag.Semlist = SemList;
                }
                else
                {
                    return Redirect("/Teacher/Access");
                }
                return View(MarkModel);
            }
            catch
            {
                return Redirect("/Login/Login");
            }
           
        }
      
        public ActionResult Markdisplay(Marks MarkModel)
        {
            try
            {
               
                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Teacher" || role=="HOD")
                {
                    try
                    {
                        int batchid = Data.Batch(MarkModel.stdid, MarkModel.year, MarkModel.sem);
                        int stddetail = Data.stddetails(batchid, MarkModel.stdid);
                        List<Markdetail> details = Data.marks(stddetail, batchid);

                        return View(details);
                    }
                    catch
                    {
                        return Redirect("/Teacher/error");
                    }
                }
                else
                {
                    return Redirect("/Teacher/Access");
                }
               
            }
            catch
            {
                return Redirect("/Login/Login");
            }
        }
        public ActionResult MarkAddition()
        {


            Marks MarkModel = new Marks();
                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Teacher" )
                {
                    var student = Data.students(j);
                    foreach (var n in student)
                    {

                        MarkModel.Students.Add(new SelectListItem() { Text = n.stName, Value = n.stid.ToString() });
                    }
                    
                }
                else
                {
                    return Redirect("/Teacher/Access");
                }
                return View(MarkModel);
            
            
            
        }
        public PartialViewResult Batch(Marks MarkModel)
        {
           try
            {
                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Teacher")
                {
                    foreach (var n in Data.Batchs(MarkModel.stdid))
                    {
                        if (n.sem == 1)
                            MarkModel.Batchs.Add(new SelectListItem() { Text = (n.year + "/" + "FIRST").ToString(), Value = n.batchid.ToString() });
                        if (n.sem == 2)
                            MarkModel.Batchs.Add(new SelectListItem() { Text = (n.year + "/" + "SECOND").ToString(), Value = n.batchid.ToString() });

                    }
                    ViewBag.stdid = MarkModel.stdid;
                    return PartialView("Batch", MarkModel);
                }
                else
                {
                    return PartialView("/Teacher/Access");
                }
            }
            catch
            {
                return PartialView("/Login/Login");
            }
            
        }
      
        public PartialViewResult AddMark(Marks MarkModel, List<Markdetail> SubMark,int stdid)
        {
            SubMark = Data.subjects(MarkModel.batchid, stdid);
            MarkModel.ListofMark = SubMark;
            ViewBag.BatchID = MarkModel.batchid;
            ViewBag.StdID = MarkModel.stdid;
            return PartialView("AddMark", SubMark);
        }
        [HttpPost]
        public ActionResult AddMark(Marks MarkModel)
        {
            int BatchID = int.Parse(Request["hidBatch"]);
            int StdID = int.Parse(Request["hidStd"]);
            List<Markdetail> SubMark = Data.subjects(BatchID, StdID);
            List<Markdetail> markdetail = new List<Markdetail>();

            foreach (Markdetail obj in SubMark)
            {
                Markdetail mobj = new Markdetail();
                mobj.SDID = obj.SDID;
                mobj.Subid = obj.Subid;
                
                int Marks = int.Parse(Request["sub_" + obj.Subid]);

                mobj.Mark = Marks;

                markdetail.Add(mobj);
            }
            Data.Addition(markdetail);
            return Redirect("/Teacher/MarkAddition");
        }
        public ActionResult error()
        {
            return View();
        }
        public ActionResult Access()
        {
            return View();
        }
        public ActionResult four()
        {
            return View();
        }
    }
}