using SAT.HR.Data;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class WorkingTimeController : BaseController
    {
        // การเข้าปฏิบัติงาน
        #region ทำรายการลา

        public ActionResult LeaveRequest()
        {
            return View();
        }

        public ActionResult _LeaveRequest()
        {
            return PartialView("_LeaveRequest");
        }

        #endregion

        #region สิทธิการลาพนักงาน

        public ActionResult LeaveBalance()
        {
            ViewBag.UserType = DropDownList.GetUserType(1);
            ViewBag.UserStatus = DropDownList.GetUserStatus(1);
            return View();
        }

        public ActionResult LeaveBalanceDetail(int id)
        {
            var model = new EmployeeRepository().GetByID(id);
            ViewBag.YearLeaveBalance = DropDownList.GetYearHoliday(null);
            return View(model);
        }

        public ActionResult _LeaveBalance()
        {
            return PartialView("_LeaveBalance");
        }

        #endregion

        #region รอบการเข้าปฏิบัติงาน

        public ActionResult WorkingShift()
        {
            ViewBag.UserType = DropDownList.GetUserType(1);
            ViewBag.UserStatus = DropDownList.GetUserStatus(1);
            return View();
        }

        public ActionResult WorkingShiftDetail(int id)
        {
            var model = new EmployeeRepository().GetByID(id);
            return View(model);
        }

        public ActionResult WorkingShiftDetailByID(int? id)
        {
            //var model = new WorkingTime().WorkingShiftDetailByID(id);
            return PartialView("_WorkingShiftDetail");
        }


        #endregion

        #region ปรับปรุงเวลาเข้าปฏิบัติงาน

        public ActionResult TimeAttendance()
        {
            ViewBag.UserType = DropDownList.GetUserType(1);
            return View();
        }

        public ActionResult TimeAttendanceDetail(int id)
        {
            var model = new EmployeeRepository().GetByID(id);
            
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMount = firstDayOfMonth.AddMonths(1).AddDays(-1);

            string dateFrom = firstDayOfMonth.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));
            string dateTo = lastDayOfMount.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));

            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.AttendanceType = DropDownList.GetAttendanceType(null);

            return View(model);
        }

        public ActionResult TimeAttendanceByID(int? id, int userid)
        {
            var model = new TimeAttendanceRepository().GetTimeAttendanceByID(id, userid);
            ViewBag.AttendanceType = DropDownList.GetAttendanceType(null);
            return PartialView("_TimeAttendanceDetail", model);
        }

        public JsonResult DeleteTimeAttendance(int id)
        {
            var result = new TimeAttendanceRepository().DeleteByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TimeAttendanceByUser(int userid, string datefrom, string dateto, int? type)
        {
            var list = new TimeAttendanceRepository().GetTimeAttendanceByUser(userid, datefrom, dateto, type);
            return Json(new { data = list.ListTimeAttendance }, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}