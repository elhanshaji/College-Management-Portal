using collegemanagement.Models;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace collegemanagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            
            try
            {
               
                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role != null)
                {
                    LogerinfoModel info = Data.info(j,role);
                    return View(info);
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
       /* _________________________________________________________________________________________________________________________________________________________
            JISHNU*/
        public ActionResult Select()
        {
            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Management")
                {
                    SelectListItem[] Selectyear = new SelectListItem[3];
                    Selectyear[0] = new SelectListItem() { Text = "2018", Value = "2018" };
                    Selectyear[1] = new SelectListItem() { Text = "2019", Value = "2019" };
                    Selectyear[2] = new SelectListItem() { Text = "2020", Value = "2020" };
                    ViewBag.Selectyear = Selectyear;
                    SelectListItem[] selectsem = new SelectListItem[2];
                    selectsem[0] = new SelectListItem() { Text = "1", Value = "1" };
                    selectsem[1] = new SelectListItem() { Text = "2", Value = "2" };

                    ViewBag.selectsem = selectsem;
                    return View();
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

        public ActionResult Checkfee(checkfeesmodel cfm)
        {
            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Management")
                {
                    List<checkfeesmodel> cf = Data.checkfees(cfm.year, cfm.sem);
                    return View(cf);
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
        /*_____________________________________________________________________________________________________________________________________________________________
            Kalyan*/
        public ActionResult AdmissionOfStudent()
        {
           
            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Management")
                {

                    SelectListItem[] deplist = new SelectListItem[2];
                    deplist[0] = new SelectListItem() { Text = "ECE", Value = "ECE" };
                    deplist[1] = new SelectListItem() { Text = "MECH", Value = "MECH" };
                    ViewBag.branches = deplist;
                   
                    return View();
                }                
                else
                {
                    return Redirect("/Teacher/Access");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpPost]
        public ActionResult AdmissionOfStudent(AdmissionOfStudentmodel Add)
        {
            SelectListItem[] deplist = new SelectListItem[2];
            deplist[0] = new SelectListItem() { Text = "ECE", Value = "ECE" };
            deplist[1] = new SelectListItem() { Text = "MECH", Value = "MECH" };
            ViewBag.branches = deplist;
            if (ModelState.IsValid)
            {
                int AdStud = Data.AddStudent(Add.studentname, Add.confirmpassword, Add.age, Add.depname, Add.gender);
                if (AdStud == 1)
                {
                    ViewBag.msg = "Saved";

                   
                    return Redirect("/Admin/AdmissionOfStudent");
                   
                }
                else
                {
                    ViewBag.msg = "Failed";
               
                    return Redirect("/Admin/AdmissionOfStudent");

                }
            }
            return View();
            
        }
        public ActionResult AdmissionOfTeacher()
        {
            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Management")
                {
                    SelectListItem[] deplist = new SelectListItem[2];
                    deplist[0] = new SelectListItem() { Text = "ECE", Value = "ECE" };
                    deplist[1] = new SelectListItem() { Text = "MECH", Value = "MECH" };
                    ViewBag.branches = deplist;
                    return View();
                }
                else
                {
                    return Redirect("/Teacher/Access");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpPost]
        public ActionResult AdmissionOfTeacher(AdmissionOfTeachermodel hire)
        {
            SelectListItem[] deplist = new SelectListItem[2];
            deplist[0] = new SelectListItem() { Text = "ECE", Value = "ECE" };
            deplist[1] = new SelectListItem() { Text = "MECH", Value = "MECH" };
            ViewBag.branches = deplist;
            if (ModelState.IsValid)
            {
                int AdTeach = Data.AddTeacher(hire.teachername, hire.confirmpassword, hire.age, hire.depname, hire.gender, hire.experience, hire.qualification);
                if (AdTeach == 1)
                {
                    ViewBag.msg = "Saved";
                    return Redirect("/Admin/AdmissionOfTeacher");
                }
                else
                {
                    ViewBag.msg = "Failed";
                    return Redirect("/Admin/AdmissionOfTeacher");
                }
            }

            return View();
        }
        /* _____________________________________________________________________________________________________________________________________________________________________
             Elhan*/
        public ActionResult SendNotifications()
        {

            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "Management")
                {
                    SelectListItem[] RoleList = new SelectListItem[4];
                    RoleList[0] = new SelectListItem() { Text = "HOD", Value = "HOD" };
                    RoleList[1] = new SelectListItem() { Text = "Teachers", Value = "Teacher" };
                    RoleList[2] = new SelectListItem() { Text = "Students", Value = "Student" };
                    RoleList[3] = new SelectListItem() { Text = "All", Value = "All" };
                    ViewBag.ListRole = RoleList;
                    return View();
                }
                else
                {
                    return Redirect("/Teacher/Access");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }

        }

        [HttpPost]
        public ActionResult SendNotifications(SendNotificationmodel send)
        {
            SelectListItem[] RoleList = new SelectListItem[4];
            RoleList[0] = new SelectListItem() { Text = "HOD", Value = "HOD" };
            RoleList[1] = new SelectListItem() { Text = "Teachers", Value = "Teacher" };
            RoleList[2] = new SelectListItem() { Text = "Students", Value = "Student" };
            RoleList[3] = new SelectListItem() { Text = "All", Value = "All" };
            ViewBag.ListRole = RoleList;
            if(ModelState.IsValid)
            {
                int UserLoginID = int.Parse(Session["id"].ToString());
                string RoleAssign = send.ToRoleID;
                string MessageToSend = send.Message;
                int res = Data.NotificationSend(UserLoginID, RoleAssign, MessageToSend);
                if (res == 1)
                {
                    ViewBag.Result = "Saved";
                    return Redirect("/Admin/SendNotifications");
                }
            }
            
            return View();
        }
    }
}