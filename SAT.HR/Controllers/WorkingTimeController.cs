using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class WorkingTimeController : Controller
    {
        // การเข้าปฏิบัติงาน

        #region 1. ปรับปรุงเวลาเข้าปฏิบัติงาน

        public ActionResult TimeAttendance()
        {
            return View();
        }

        public ActionResult _TimeAttendance()
        {
            return PartialView("_TimeAttendance");
        }

        #endregion

        #region 2. ทำรายการลา

        public ActionResult LeaveRequest()
        {
            return View();
        }
        public ActionResult _LeaveRequest()
        {
            return PartialView("_LeaveRequest");
        }

        #endregion

        #region 3. สิทธิการลาพนักงาน

        public ActionResult LeaveBalance()
        {
            return View();
        }
        public ActionResult _LeaveBalance()
        {
            return PartialView("_LeaveBalance");
        }

        #endregion

        #region 4. รอบการเข้าปฏิบัติงาน

        public ActionResult WorkingShift()
        {
            return View();
        }

        #endregion
    }
}