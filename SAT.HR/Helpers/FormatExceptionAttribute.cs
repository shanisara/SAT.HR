using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SAT.HR.Helpers
{
    public class FormatExceptionAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
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
                UtilityService.SetSessionValue("ErrorException", filterContext.Exception);
                HttpContext.Current.Session["ErrorException"] = filterContext.Exception;
                RequestContext rc = new RequestContext(filterContext.HttpContext, filterContext.RouteData);
                string url = RouteTable.Routes.GetVirtualPath(rc, new RouteValueDictionary(new { Controller = "Exception", action = "Index" })).VirtualPath;
                filterContext.HttpContext.Response.Redirect(url, true);

                base.OnException(filterContext);
            }
        }

    }
}