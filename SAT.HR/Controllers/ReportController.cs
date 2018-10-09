using SAT.HR.Data.Repository;
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

        public ActionResult Resource(int menuid)
        {
            int userid = 1;
            var data = new PermissionRepository().MenuReportByRole(userid, menuid);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานสวัสดิการ

        public ActionResult Benefit(int menuid)
        {
            int userid = 1;
            var data = new PermissionRepository().MenuReportByRole(userid, menuid);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานพัฒนาบุคลากร

        public ActionResult Human(int menuid)
        {
            int userid = 1;
            var data = new PermissionRepository().MenuReportByRole(userid, menuid);
            return View(data);
        }

        #endregion 
    }
}