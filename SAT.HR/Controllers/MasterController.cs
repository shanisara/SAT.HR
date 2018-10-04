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
        #region 1. ฝ่าย - Division

        public ActionResult Division()
        {
            return View();
        }
        public ActionResult _Division()
        {
            return PartialView();
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

        #region 2. กอง - Department

        public ActionResult Department()
        {
            return View();
        }

        public ActionResult _Department()
        {
            return PartialView();
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

        #region 3. หน่วยงาน - Section

        public ActionResult Section()
        {
            return View();
        }

        public ActionResult _Section()
        {
            return PartialView();
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

        #region 4. ตำแหน่ง - Position

        public ActionResult Position()
        {
            return View();
        }

        public ActionResult _Position()
        {
            return PartialView();
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

        #region 5. ใบประกอบวิชาชีพ - Certificate

        public ActionResult Certificate()
        {
            return View();
        }

        public ActionResult _Certificate()
        {
            return PartialView();
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

        #region 6. เงินเดือน - Salary

        public ActionResult Salary()
        {
            return View();
        }

        public ActionResult _Salary()
        {
            return PartialView();
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

        #region 7. เครื่องราชอิสริยาภรณ์ - Insignia

        public ActionResult Insignia()
        {
            return View();
        }

        public ActionResult _Insignia()
        {
            return PartialView();
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

        #region 8. คำนำหน้าชื่อ - Title

        public ActionResult Title()
        {
            return View();
        }

        public ActionResult _Title()
        {
            return PartialView();
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

        #region 9. ระดับการศึกษา - Education

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult _Education()
        {
            return PartialView();
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

        #region 10. หลักสูตรปริญญา - Degree

        public ActionResult Degree()
        {
            return View();
        }

        public ActionResult _Degree()
        {
            return PartialView();
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

        #region 11. สาขาวิชา - Major

        public ActionResult Major()
        {
            return View();
        }

        public ActionResult _Major()
        {
            return PartialView();
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

        #region 12. สัญชาติ - Nationality

        public ActionResult Nationality()
        {
            return View();
        }

        public ActionResult _Nationality()
        {
            return PartialView();
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

        #region 13. ศาสนา - Religion

        public ActionResult Religion()
        {
            return View();
        }

        public ActionResult _Religion()
        {
            return PartialView();
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

        #region 14. สมรรถนะ - Capability

        public ActionResult Capability()
        {
            return View();
        }

        public ActionResult _Capability()
        {
            return PartialView();
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

        #region 15. ประเภทการลา - LeaveType

        public ActionResult LeaveType()
        {
            return View();
        }

        public ActionResult _LeaveType()
        {
            return PartialView();
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

        #region 16. วันหยุด - Holiday

        public ActionResult Holiday()
        {
            return View();
        }

        public ActionResult _Holiday()
        {
            return PartialView();
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

        #region 17. รหัสการเคลื่อนไหว - ActionType

        public ActionResult ActionType()
        {
            return View();
        }

        public ActionResult _ActionType()
        {
            return PartialView();
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

        #region 18. ค่าคงที่ระบบ - SystemConfig

        public ActionResult SystemConfig()
        {
            return View();
        }

        #endregion

        #region 19. จัดการสิทธิการเข้าใช้งาน - UserRole

        public ActionResult UserRole()
        {
            return View();
        }
        public ActionResult _User()
        {
            return PartialView();
        }
        public ActionResult _Role()
        {
            return PartialView();
        }
        public ActionResult _UserRole()
        {
            return PartialView();
        }

        #endregion

    }
}