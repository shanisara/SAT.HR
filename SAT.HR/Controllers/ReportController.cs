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
            int userid = UtilityService.User.UserID;
            var data = new PermissionRepository().MenuReportByRole(userid, id);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานสวัสดิการ

        public ActionResult Benefit(int id)
        {
            int userid = UtilityService.User.UserID;
            var data = new PermissionRepository().MenuReportByRole(userid, id);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานพัฒนาบุคลากร

        public ActionResult Human(int id)
        {
            int userid = UtilityService.User.UserID;
            var data = new PermissionRepository().MenuReportByRole(userid, id);
            return View(data);
        }

        #endregion 
    }
}