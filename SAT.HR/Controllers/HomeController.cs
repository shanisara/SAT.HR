using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
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
    [AuthorizeUser]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Logout()
        {
            UtilityService.ClearSession();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(EmployeeViewModel model)
        {
            ResponseData result = new ResponseData();

            try
            {
                var emp = new EmployeeRepository().Login(model.UserName, model.Password);
                if (emp != null)
                {
                    bool activate = emp.IsActive.HasValue ? (bool)emp.IsActive : false;
                    if (activate)
                    {
                        UserProfile obj = new Models.UserProfile();
                        obj.UserID = emp.UserID;
                        obj.UserName = emp.UserName;
                        obj.DivID = emp.DivID;
                        obj.DivName = emp.DivName;
                        obj.DepID = emp.DepID;
                        obj.DepName = emp.DepName;
                        obj.SecID = emp.SecID;
                        obj.SecName = emp.SecName;
                        obj.PoID = emp.PoID;
                        obj.PoName = emp.PoName;
                        obj.UserTypeID = emp.UserTID;
                        obj.UserTypeName = emp.UserTName;
                        obj.FullName = emp.FirstName + " " + emp.LastName;
                        obj.Avatar = !string.IsNullOrEmpty(emp.Avatar) ? emp.Avatar : "avatar.png";
                        UtilityService.User = obj;
                    }
                    else
                    {
                        result.MessageCode = "001";
                        result.MessageText = "Your account has not activate";
                    }
                }
                else
                {
                    result.MessageCode = "002";
                    result.MessageText = "Username or Password incorrect";
                }
            }
            catch (Exception e)
            {
                
            }

            return Json(new { MessageCode = result.MessageCode, MessageText = result.MessageText }, JsonRequestBehavior.AllowGet);
        }


    }
}