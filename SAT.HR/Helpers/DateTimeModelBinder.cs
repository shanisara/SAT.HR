using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Helpers
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            string[] formatList = { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "dd/MM/yyyy HH:mm:ss", "MMM/yyyy", "dd/MM/yyyy HH:mm" };

            DateTime outTime;

            var isSuccess = DateTime.TryParseExact(value.AttemptedValue, formatList, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out outTime);

            return isSuccess == false ? (DateTime?)null : outTime;
        }
    }

}