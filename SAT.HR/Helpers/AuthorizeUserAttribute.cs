using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;

namespace SAT.HR.Helpers
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session == null)
            {
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (UtilityService.User == null && !filterContext.IsChildAction) 
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }
}