using SAT.HR.Helpers;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SAT.HR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            //var binder = new DateTimeModelBinder();
            //ModelBinders.Binders.Add(typeof(DateTime), binder);
            //ModelBinders.Binders.Add(typeof(DateTime?), binder);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            if (exception != null)
            {
                ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                log.Error(exception);
            }

        }
    }
}
