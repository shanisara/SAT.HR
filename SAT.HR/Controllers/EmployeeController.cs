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
    [AuthorizeUser]
    public class EmployeeController : Controller
    {
        // ระบบพนักงาน/ลูกจ้าง 

        #region 1. ทะเบียนประวัติ

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new EmployeeRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model = new EmployeeRepository().GetByID(id);

            ViewBag.UserTitle = DropDownList.GetTitle(model.SexID, model.TitleID, true);
            ViewBag.Sex = DropDownList.GetSex(model.SexID);
            ViewBag.BloodType = DropDownList.GetBloodType(model.BloodID);
            ViewBag.MaritalStatus = DropDownList.GetMaritalStatus(model.MaritalStatusID);

            ViewBag.Religion = DropDownList.GetReligion(model.ReligionID, true);
            ViewBag.Ethnicity = DropDownList.GetNationality(model.EthnicityID, true);       //-- > เชื้อชาติ
            ViewBag.Nationality = DropDownList.GetNationality(model.NationalityID, true);   //--> สัญชาติ 

            ViewBag.SalaryLevel = DropDownList.GetSalaryLevel(model.SalaryLevel);
            ViewBag.SalaryStep = DropDownList.GetSalaryStep(model.SalaryStep, model.SalaryLevel);

            ViewBag.Position = DropDownList.GetPosition(model.PoID, true);
            ViewBag.Division = DropDownList.GetDivision(model.DivID, true);
            ViewBag.Department = DropDownList.GetDepartment(model.DivID, model.DepID, true);
            ViewBag.Section = DropDownList.GetSection(model.DivID, model.DepID, model.SecID, true);

            ViewBag.Empower = DropDownList.GetEmpower(model.EmpowerID);     //-- > ช่วยราชการ
            ViewBag.EmpowerPosition = DropDownList.GetEmpower(model.EmpowerID);
            ViewBag.EmpowerDivision = DropDownList.GetEmpower(model.EmpowerID);
            ViewBag.EmpowerDepartment = DropDownList.GetEmpower(model.EmpowerID);
            ViewBag.EmpowerSection = DropDownList.GetEmpower(model.EmpowerID);

            ViewBag.PositionType = DropDownList.GetPositionType(model.PoTID);//-- > รักษาการแทน
            ViewBag.AgentPosition = DropDownList.GetPositionType(model.PoTID);
            ViewBag.AgentDivision = DropDownList.GetPositionType(model.PoTID);
            ViewBag.AgentDepartment = DropDownList.GetPositionType(model.PoTID);
            ViewBag.AgentSection = DropDownList.GetPositionType(model.PoTID);

            ViewBag.HomeProvince = DropDownList.GetProvince(model.HomeProvinceID);
            ViewBag.HomeDistrict = DropDownList.GetDistrict(model.HomeDistrictID, model.HomeProvinceID);
            ViewBag.HomeSubDistrict = DropDownList.GetSubDistrict(model.HomeSubDistrictID, model.HomeProvinceID, model.HomeDistrictID);

            ViewBag.CurrProvince = DropDownList.GetProvince(model.CurrProvinceID);
            ViewBag.CurrDistrict = DropDownList.GetDistrict(model.CurrDistrictID, model.CurrProvinceID);
            ViewBag.CurrSubDistrict = DropDownList.GetSubDistrict(model.CurrSubDistrictID, model.CurrProvinceID, model.CurrDistrictID);

            ViewBag.UserStatus = DropDownList.GetUserStatus(model.StatusID);
            ViewBag.WorkingType = DropDownList.GetWorkingType(model.StatusID);

            return View(model);
        }

        //public JsonResult SaveEmployee(SectionViewModel model)
        //{
        //    ResponseData result = new Models.ResponseData();
        //    if (model.UserID != 0)
        //        result = new EmployeeRepository().UpdateEmployee(model);
        //    else
        //        result = new EmployeeRepository().AddEmployee(model);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult DeleteEmployee(int id)
        //{
        //    var result = new EmployeeRepository().DeleteEmployee(id);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        #region Partial

        public ActionResult Employee()
        {
            return PartialView("_Employee");
        }

        public ActionResult Family()
        {

            return PartialView("_Family");
        }

        public ActionResult Education()
        {

            return PartialView("_Education");
        }

        public ActionResult Position()
        {

            return PartialView("_Position");
        }

        public ActionResult Trainning()
        {

            return PartialView("_Trainning");
        }

        public ActionResult Insignia()
        {

            return PartialView("_Insignia");
        }

        public ActionResult Excellent()
        {

            return PartialView("_Excellent");
        }

        public ActionResult Certificate()
        {

            return PartialView("_Certificate");
        }

        public ActionResult History()
        {

            return PartialView("_History");
        }

        public ActionResult Remuneration()
        {

            return PartialView("_Remuneration");
        }

        #endregion

        #endregion 

        #region 2. อัตรากำลังพล

        public ActionResult PositionRate()
        {
            return View();
        }

        public ActionResult _PositionRate()
        {
            return PartialView("_PositionRate");
        }

        #endregion

        #region 3. โยกย้ายระดับ

        public ActionResult EmployeeTransfer()
        {
            return View();
        }

        public ActionResult EmployeeTransferDetail()
        {
            return View();
        }
        
        public ActionResult _EmployeeTransferDetail()
        {
            return PartialView("_EmployeeTransferDetail");
        }


        #endregion

        #region 4. โยกย้ายอัตรากำลังพล

        public ActionResult PositionTransfer()
        {
            return View();
        }

        public ActionResult _PositionTransferDetail()
        {
            return PartialView("_PositionTransferDetail");
        }

        #endregion

    }
}