using collegemanagement.Models;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace collegemanagement.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ViewMark()
        {
           
            try
            {
                Marks MarkModel = new Marks();
                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Student")
                {

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
                if (role == "Student")
                {
                    try
                    {
                        int batchid = Data.Batch(j, MarkModel.year, MarkModel.sem);
                        int stddetail = Data.stddetails(batchid, j);
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
        public ActionResult payfees()
        {
            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Student")
                {
                    int id = int.Parse(Session["id"].ToString());
                    List<Feesmodel> fm = Data.amount(id);
                    return View(fm);
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


        public ActionResult Feespayment(int SDID)
        {
            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Student")
                {
                    Data.pay(SDID);
                    return Redirect("/Student/payfees");
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
    }
}