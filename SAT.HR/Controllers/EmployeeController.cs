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

        public ActionResult Detail()
        {
            return View();
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

        public ActionResult Employee(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model = new EmployeeRepository().GetByID(id);

            //ViewBag.Title = DropDownList.GetTitle(model.TitleID);
            //ViewBag.UserStatus = DropDownList.UserStatus(model.UserStatusID);
            ViewBag.Sex = DropDownList.GetSex(model.SexID);
            //ViewBag.Religion = DropDownList.GetReligion(model.Religion, true);
            //ViewBag.Ethnicity = DropDownList.GetNationality(model.Ethnicity, true)      //-- > เชื้อชาติ
            //ViewBag.Nationality = DropDownList.GetNationality(model.Nationalit, true)   //--> สัญชาติ 
            //ViewBag.BloodType = DropDownList
            //ViewBag.MaritalStatus = DropDownList
            ViewBag.Position = DropDownList.GetPosition(model.PoID, true);
            ViewBag.Division = DropDownList.GetDivision(model.DivID, true);
            ViewBag.Department = DropDownList.GetDepartment(model.DivID, model.DepID, true);
            ViewBag.Section = DropDownList.GetSection(model.DivID, model.DepID, model.SecID, true);
            //ViewBag.Level = DropDownList.GetLevel(model.Level);
            //ViewBag.Step = DropDownList.GetStep(model.Step);
            //ViewBag. = DropDownList --> ช่วยราชการ
            //ViewBag. = DropDownList --> รักษาการแทน
            //ViewBag.Province = DropDownList.GetProvince(model.ProvinceID);
            //ViewBag.District = DropDownList.GetDistrict(model.DistrictID);
            //ViewBag.SubDistrict = DropDownList.GetSubDistrict(model.SubDistrictID);

            return PartialView("_Employee", model);
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

        public ActionResult Outstanding()
        {

            return PartialView("_Outstanding");
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