using SAT.HR.Data;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class WorkingTimeController : BaseController
    {
        #region ทำรายการลา

        public ActionResult LeaveRequest()
        {
            ViewBag.LeaveYear = DropDownList.GetLeaveYear(DateTime.Now.Year);
            ViewBag.LeaveStatus = DropDownList.GetLeaveStatus(null);
            return View();
        }

        public ActionResult LeaveWaiting()
        {
            return View();
        }

        public ActionResult LeaveApprove(int? id)
        {
            var model = new LeaveRequestRepository().GetDetail(id);
            return View(model);
        }

        public ActionResult LeaveRequestDetail(int? id)
        {
            var model = new LeaveRequestRepository().GetByID(id);
            ViewBag.Employee = DropDownList.GetEmployee(model.RequestID, 1);
            ViewBag.LeaveType = DropDownList.GetLeaveType(model.LeaveType, true, UtilityService.User.SexID, (int)model.RequestID);
            ViewBag.MaxFileSizeUpload = Helpers.SysConfig.MaxFileSizeUpload;
            return View(model);
        }

        public JsonResult SaveLeaveRequest(LeaveRequestViewModel data)
        {
            ResponseData result = new ResponseData();
            if (data.FormID.HasValue)
                result = new LeaveRequestRepository().UpdateByEntity(data);
            else
                result = new LeaveRequestRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApproveLeave(LeaveRequestViewModel data)
        {
            ResponseData result = new LeaveRequestRepository().Approve(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmCancelLeaveRequest(LeaveRequestViewModel data)
        {
            var result = new LeaveRequestRepository().Cancel(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelLeaveRequest(int? id)
        {
            var model = new LeaveRequestRepository().GetDetail(id);
            return PartialView("_CancelLeaveRequest", model);
        }

        public JsonResult LeaveCalculateTotalDay(int daytime, DateTime startdate, DateTime entdate)
        {
            var result = new LeaveRequestRepository().CalculateTotalDay(daytime, startdate, entdate);
            return Json(new { TotalDay = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LeaveRequest(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns, string year, string status)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new LeaveRequestRepository().GetLeaveRequest(search, draw, start, length, dir, column, year, status);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LeaveWaiting(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns, string year, string status)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new LeaveRequestRepository().GetLeaveWaiting(search, draw, start, length, dir, column, year, status);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFileAttach(int id)
        {
            var result = new LeaveRequestRepository().DownloadFileAttach(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
        }

        #endregion

        #region ปรับปรุงสิทธิการลาพนักงาน

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

        #region ปรับปรุงรอบการเข้าปฏิบัติงาน

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

            string dateFrom = firstDayOfMonth.ToString("dd/MM/yyy"); //firstDayOfMonth.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));
            string dateTo = lastDayOfMount.ToString("dd/MM/yyy"); //lastDayOfMount.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));

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

            string dateFrom = firstDayOfMonth.ToString("dd/MM/yyy"); //firstDayOfMonth.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));
            string dateTo = lastDayOfMount.ToString("dd/MM/yyy"); //lastDayOfMount.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH"));

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

        #region ตรวจสอบสิทธิการลาคงเหลือ

        public ActionResult EmpLeaveBalance()
        {
            int id = UtilityService.User.UserID;
            var model = new LeaveBalanceRepository().GetByUser(id, null);
            return View(model);
        }

        #endregion

    }
}