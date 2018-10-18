using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static SAT.HR.Models.BaseViewModels;

namespace SAT.HR.Controllers
{
    [AuthorizeUser]
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                string StackTrace = string.Empty;
                int i = filterContext.Exception.StackTrace.LastIndexOf("lambda_method");
                if (i != -1)
                {
                    StackTrace = filterContext.Exception.StackTrace.Substring(0, i - 3).Replace("\r\n", "<br/>");
                    StackTrace += "<br/>" + filterContext.Exception.TargetSite.ToString(); ;
                }
                else
                    StackTrace = filterContext.Exception.StackTrace;
                filterContext.Result = new JsonResult()
                {
                    ContentType = "application/json",
                    Data = new
                    {
                        name = filterContext.Exception.GetType().Name,
                        message = filterContext.Exception.Message,
                        callstack = StackTrace.Replace("\r\n", "<br/>")// filterContext.Exception.StackTrace
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
            else
            {
                this.Session["ErrorException"] = filterContext.Exception; // Can use this exception message later on.

                filterContext.ExceptionHandled = true;

                filterContext.Result = this.RedirectToAction("Error", "Home"); // Redirect to error page.
                
                base.OnException(filterContext);
            }
        }

        //protected Actions GetPermission(int MenuId)
        //{
        //    var _m = AppUtils.MenuInfo;
        //    Actions _r = new Actions
        //    {
        //        IsActionAdd = false,
        //        IsActionDeleted = false,
        //        IsActionEdit = false,
        //        IsActionExport = false,
        //        IsView = false
        //    };
        //    var _ac = (from m in _m
        //               where m.MenuID == MenuId
        //               select m).FirstOrDefault();
        //    if (_ac != null)
        //    {
        //        _r = new Actions
        //        {
        //            IsActionAdd = _ac.IsActionAdd,
        //            IsActionDeleted = _ac.IsActionDeleted,
        //            IsActionEdit = _ac.IsActionEdit,
        //            IsActionExport = _ac.IsActionExport,
        //            IsView = _ac.IsView
        //        };
        //    }
        //    return _r;
        //}
    }
}