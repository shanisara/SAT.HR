using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class ReportController : Controller
    {
        #region // รายงาน:ส่วนงานทรัพยากรบุคคล

        public ActionResult Resource(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานสวัสดิการ

        public ActionResult Benefit(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานพัฒนาบุคลากร

        public ActionResult Human(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        #endregion 
    }
}