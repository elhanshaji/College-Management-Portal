using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace collegemanagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login(Loginmodel userinfo)
        {
            if(ModelState.IsValid)
            {
                int userid = Data.loginfo(userinfo.username, userinfo.password);
                if (userid!= 0)
                {
                    Session["id"] = userid;
                    return Redirect("/Admin/Dashboard");
                }
                else
                {
                    ViewBag.logmsg = "Wrong Username or password";
                    return View();
                }
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect("/Login/Login");
        }
    }
}