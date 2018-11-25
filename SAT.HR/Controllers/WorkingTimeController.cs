using SAT.HR.Data;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
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

        public ActionResult LeaveBalanceDetail(int id, int? year)
        {
            var model = new LeaveBalanceRepository().GetByUser(id, year);
            ViewBag.YearLeaveBalance = DropDownList.GetYearLeave(year);
            return View(model);
        }

        public JsonResult SaveLeaveBalance(LeaveBalanceViewModel data)
        {
            ResponseData result = new LeaveBalanceRepository().SaveByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
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

            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMount = firstDayOfMonth.AddMonths(1).AddDays(-1);

            string dateFrom = firstDayOfMonth.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));
            string dateTo = lastDayOfMount.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));

            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            return View(model);
        }

        public ActionResult WorkingShiftByID(int? id, int userid)
        {
            var model = new WorkingShiftRepository().GetWorkingShiftByID(id, userid);
            ViewBag.Month = DropDownList.GetMonth(model.WsMonth);
            return PartialView("_WorkingShiftDetail", model);
        }

        public JsonResult SaveWorkingShift(WorkingShiftViewModel data)
        {
            ResponseData result = new ResponseData();
            if (data.WsID != 0)
                result = new WorkingShiftRepository().UpdateByEntity(data);
            else
                result = new WorkingShiftRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteWorkingShift(int id)
        {
            var result = new WorkingShiftRepository().DeleteByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult WorkingShiftByUser(int userid, string datefrom, string dateto)
        {
            var list = new WorkingShiftRepository().GetWorkingShiftByUser(userid, datefrom, dateto);
            return Json(new { data = list.ListWorkingShift }, JsonRequestBehavior.AllowGet);
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

        public JsonResult SaveTimeAttendance(TimeAttendanceViewModel data)
        {
            ResponseData result = new ResponseData();
            if (data.TaID != 0)
                result = new TimeAttendanceRepository().UpdateByEntity(data);
            else
                result = new TimeAttendanceRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
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