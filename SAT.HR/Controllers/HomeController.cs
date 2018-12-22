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
    public class HomeController : BaseController
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

        public ActionResult Login(string username, string password)
        {
            ResponseData result = new ResponseData();

            try
            {
                var emp = new EmployeeRepository().Login(username, password);
                if (emp != null)
                {
                    bool terminate = emp.IsTerminate.HasValue ? (bool)emp.IsTerminate : false;
                    bool inrole = emp.RoleID.HasValue ? true : false; ;

                    if (terminate)
                    {
                        result.MessageCode = "002";
                        result.MessageText = "รหัสผู้ใช้ " + username + " ถูกระงับการใช้งาน <br/> กรุณาติดต่อผู้ดูแลระบบ!";
                    }
                    else if (!inrole)
                    {
                        result.MessageCode = "002";
                        result.MessageText = "รหัสผู้ใช้ " + username + " ไม่มีกลุ่มผู้ใช้งาน  <br/> กรุณาติดต่อผู้ดูแลระบบ!";
                    }
                    else
                    {
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                                      1,                        // version
                                                                      emp.UserID.ToString(),    // userud
                                                                      DateTime.Now,             // created
                                                                      DateTime.Now.AddDays(/*FormsAuthentication.Timeout*/1.0),   // expires
                                                                      true,                     // rememberMe?
                                                                      emp.RoleID.ToString()     // can be used to store roles
                                                                      );
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        string[] role = authTicket.UserData.Split(new Char[] { '|' });
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(authCookie);
                        System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), role);
                        UtilityService.User = emp;
                        //UtilityService.MenuInfo = new PermissionModels().DoloadMenu(DebtCollection.Common.AppUtils.UserInfo.UserRoleId/*, DebtCollection.Common.AppUtils.UserInfo.Userid*/);
                    }
                }
                else
                {
                    result.MessageCode = "004";
                    result.MessageText = "รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง.";
                }
            }
            catch (Exception e)
            {
                result.MessageCode = "005";
                result.MessageText = e.Message;
            }

            return Json(new { MessageCode = result.MessageCode, MessageText = result.MessageText }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            var raisedException = (Exception)Session["ErrorException"];
            ViewBag.Message = string.Empty;
            ViewBag.StackTrace = string.Empty;
            if (raisedException != null)
            {
                ViewBag.Message = raisedException.Message;
                int i = raisedException.StackTrace.LastIndexOf("lambda_method");
                if (i != -1)
                {
                    ViewBag.StackTrace = raisedException.StackTrace.Substring(0, i - 3).Replace("\r\n", "<br/>");
                    ViewBag.StackTrace += "<br/>" + raisedException.TargetSite.ToString();
                }
                else
                {
                    ViewBag.StackTrace = raisedException.StackTrace;
                }
            }
            return View();
        }
    }
}