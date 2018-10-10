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

        
        public ActionResult Login(/*EmployeeViewModel model*/ string username, string password)
        {
            ResponseData result = new ResponseData();

            try
            {
                var emp = new EmployeeRepository().Login(model);
                if (emp != null)
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
                    UtilityService.User = obj;
                }
            }
            catch (Exception e)
            {
                
            }

            return Json(new { MessageCode = result.MessageCode, MessageText = result.MessageText }, JsonRequestBehavior.AllowGet);
        }


    }
}