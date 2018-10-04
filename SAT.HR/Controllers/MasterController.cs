using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT.HR.Models;
using SAT.HR.Data.Repository;

namespace SAT.HR.Controllers
{
    public class MasterController : Controller
    {
        #region ฝ่าย - Division

        public ActionResult Division()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Division(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new DivisionRepository().GetDivision(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region กอง - Department

        public ActionResult Department()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Department(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new DepartmentRepository().GetDepartment(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region หน่วยงาน - Section
        public ActionResult Section()
        {
            return View();
        }
        //[HttpPost]
        //public JsonResult Section(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        //{
        //    var search = Request["search[value]"];
        //    var dir = string.Empty;     //order[0]["dir"].ToLower();
        //    var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

        //    var dataTableData = new SectionRepository().GetSection(search, draw, start, length, dir, column);

        //    return Json(dataTableData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region ตำแหน่ง - Position
        public ActionResult Position()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Position(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new PositionRepository().GetPosition(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ใบประกอบวิชาชีพ - Certificate
        public ActionResult Certificate()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Certificate(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new CertificateRepository().GetCertificate(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region เงินเดือน - Salary
        public ActionResult Salary()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Salary(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new SalaryRepository().GetSalary(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region เครื่องราชอิสริยาภรณ์ - Insignia
        public ActionResult Insignia()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Insignia(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new InsigniaRepository().GetInsignia(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region คำนำหน้าชื่อ - Title

        public ActionResult Title()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Title(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new TitleRepository().GetTitle(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ระดับการศึกษา - Education
        public ActionResult Education()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Education(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new EducationRepository().GetEducation(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region หลักสูตรปริญญา - Degree
        public ActionResult Degree()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Degree(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new DegreeRepository().GetDegree(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region สาขาวิชา - Major
        public ActionResult Major()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Major(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new MajorRepository().GetMajor(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region สัญชาติ - Nationality
        public ActionResult Nationality()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Nationality(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new NationalityRepository().GetNationality(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ศาสนา - Religion
        public ActionResult Religion()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Religion(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new ReligionRepository().GetReligion(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region สมรรถนะ - Capability
        public ActionResult Capability()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Capability(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new CapabilityRepository().GetCapability(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ประเภทการลา - LeaveType
        public ActionResult LeaveType()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LeaveType(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new LeaveTypeRepository().GetLeaveType(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region วันหยุด - Holiday
        public ActionResult Holiday()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Holiday(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new HolidayRepository().GetHoliday(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region รหัสการเคลื่อนไหว - ActionType
        public ActionResult ActionType()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ActionType(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = string.Empty;     //order[0]["dir"].ToLower();
            var column = string.Empty;  //columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new ActionTypeRepository().GetActionType(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ค่าคงที่ระบบ - SystemConfig
        public ActionResult SystemConfig()
        {
            return View();
        }

        #endregion

        #region จัดการสิทธิการเข้าใช้งาน - UserRole

        public ActionResult UserRole()
        {
            return View();
        }


        #endregion

    }
}