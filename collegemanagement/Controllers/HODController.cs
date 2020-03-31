using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace collegemanagement.Controllers
{
    public class HODController : Controller
    {
        // GET: HOD
        public ActionResult SendNotifications()
        {

            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "HOD")
                {
                    SelectListItem[] RoleList = new SelectListItem[3];
                    RoleList[0] = new SelectListItem() { Text = "Teachers", Value = "Teacher" };
                    RoleList[1] = new SelectListItem() { Text = "Students", Value = "Student" };
                    RoleList[2] = new SelectListItem() { Text = "All", Value = "All" };
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
            SelectListItem[] RoleList = new SelectListItem[3];
            RoleList[0] = new SelectListItem() { Text = "Teachers", Value = "Teacher" };
            RoleList[1] = new SelectListItem() { Text = "Students", Value = "Student" };
            RoleList[2] = new SelectListItem() { Text = "All", Value = "All" };
            ViewBag.ListRole = RoleList;
            if (ModelState.IsValid)
            {
                int UserLoginID = int.Parse(Session["id"].ToString());
                string RoleAssign = send.ToRoleID;
                string MessageToSend = send.Message;
                int res = Data.NotificationSend(UserLoginID, RoleAssign, MessageToSend);
                if (res == 1)
                {
                    ViewBag.Result = "Saved";
                    return Redirect("/HOD/SendNotifications");
                }
            }

            return View();
        }
        public ActionResult ViewNotifications()
        {

            try
            {

                int j = (int)Session["id"];
                var role = Data.roles(j);
                if (role == "HOD" || role=="Teacher" ||role=="Student")
                {
                    List<NotifyList> not = Data.ViewNotifs(j);
                   
                        if (not is null)
                        {
                            return RedirectToAction("NoNotifications", "Exceptions");
                        }
                        else
                        {
                            
                            return View(not);
                        }
                    
                    
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
    }
}