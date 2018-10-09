using SAT.HR.Helpers;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MultilingualismAttribute());
        }
    }
}
