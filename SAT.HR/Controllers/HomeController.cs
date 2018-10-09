using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SAT.HR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult LockScreen()
        {

            return PartialView();
        }

        
        public ActionResult Login(EmployeeViewModel model)
        {
            ResponseData result = new ResponseData();

            try
            {
                var emp = new EmployeeRepository().Login(model);
                if (emp != null)
                {
                    string msg = emp.UserID + "|" + emp.UserName + "|" + emp.UserName;
                    FormsAuthentication.SetAuthCookie(msg, true);

                    var data = new PermissionRepository().MenuByUser(emp.UserID);
                    Session.Add("Permission_SAT", data);
                }
            }
            catch (Exception e)
            {
                
            }

            return View("DashBoard");

            //return Json(new { MessageCode = result.MessageCode, MessageText = result.MessageText }, JsonRequestBehavior.AllowGet);
        }


    }
}