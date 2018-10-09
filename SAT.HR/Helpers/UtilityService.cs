using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Helpers
{
    public class UtilityService
    {
        public string GetEmployeeCode()
        {
            string Text = HttpContext.Current.User.Identity.Name;
            return Text.Split('|')[0];
        }

    }
}