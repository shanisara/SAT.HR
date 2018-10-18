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
        #region 1. ทะเบียนประวัติ

        public ActionResult Index()
        {
            return View();
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
            ViewBag.EmpowerPosition = DropDownList.GetPosition(model.EmpowerID, true);
            ViewBag.EmpowerDivision = DropDownList.GetDivision(model.EmpowerDivID, true);
            ViewBag.EmpowerDepartment = DropDownList.GetDepartment(model.EmpowerDivID, model.EmpowerDepID, true);
            ViewBag.EmpowerSection = DropDownList.GetSection(model.EmpowerDivID, model.EmpowerDepID, model.EmpowerSecID, true);

            ViewBag.PositionType = DropDownList.GetPositionType(model.PoTID);//-- > รักษาการแทน
            ViewBag.AgentPosition = DropDownList.GetPosition(model.AgentPoID, true);
            ViewBag.AgentDivision = DropDownList.GetDivision(model.AgentDivID, true);
            ViewBag.AgentDepartment = DropDownList.GetDepartment(model.AgentDivID, model.AgentDepID, true);
            ViewBag.AgentSection = DropDownList.GetSection(model.AgentDivID, model.AgentDepID, model.AgentSecID, true);

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

        [HttpPost]
        public JsonResult Index(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new EmployeeRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tab: User-Employee

        public ActionResult Employee(int id)
        {
            var model = new EmployeeRepository().GetByID(id);
            return PartialView("_Employee", model);
        }

        public JsonResult SaveEmployee(EmployeeViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateUserByEntity(data);
            else
                result = new EmployeeRepository().AddUserByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEmployee(int id)
        {
            var result = new EmployeeRepository().DeleteUserByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tab: User-Family

        public ActionResult Family(int id)
        {
            var model = new EmployeeRepository().GetUserFamily(id);
            return PartialView("_Family", model);
        }

        public JsonResult SaveFamily(UserFamilyViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateFamilyeByEntity(data);
            else
                result = new EmployeeRepository().AddFamilyByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFamily(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteFamilyByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tab: User-Education

        public ActionResult Education(int id)
        {
            var model = new EmployeeRepository().GetUserEducation(id);
            return PartialView("_Education", model);
        }

        public JsonResult Save(UserEducationViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateEducationByEntity(data);
            else
                result = new EmployeeRepository().AddEducationByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEducationy(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteEducationByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tab: User-Position

        public ActionResult Position(int id)
        {
            var model = new EmployeeRepository().GetUserPosition(id);
            return PartialView("_Position", model);
        }

        public JsonResult SavePosition(UserPositionViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdatePositionByEntity(data);
            else
                result = new EmployeeRepository().AddPositionByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePosition(int userid, int id)
        {
            var result = new EmployeeRepository().DeletePositionByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Tab: User-Trainning

        public ActionResult Trainning(int id)
        {
            var model = new EmployeeRepository().GetUserTrainning(id);
            return PartialView("_Trainning", model);
        }

        public JsonResult SaveTraining(UserTrainningViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateTrainingByEntity(data);
            else
                result = new EmployeeRepository().AddTrainingByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTrainning(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteTrainingByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Tab: User-Insignia

        public ActionResult Insignia(int id)
        {
            var model = new EmployeeRepository().GetUserInsignia(id);
            return PartialView("_Insignia", model);
        }

        public JsonResult SaveInsignia(UserInsigniaViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateInsigniaByEntity(data);
            else
                result = new EmployeeRepository().AddInsigniaByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteInsignia(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteInsigniaByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Tab: User-Excellent

        public ActionResult Excellent(int id)
        {
            var model = new EmployeeRepository().GetUserExcellent(id);
            return PartialView("_Excellent", model);
        }

        public JsonResult SaveExcellent(UserExcellentViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateExcellentByEntity(data);
            else
                result = new EmployeeRepository().AddExcellentByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteExcellent(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteExcellentByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Tab: User-Certificate

        public ActionResult Certificate(int id)
        {
            var model = new EmployeeRepository().GetUserCertificate(id);
            return PartialView("_Certificate", model);
        }

        public JsonResult SaveCertificate(UserCertificateViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateCertificateByEntity(data);
            else
                result = new EmployeeRepository().AddCertificateByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCertificate(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteCertificateByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tab: User-History

        public ActionResult History(int id)
        {
            var model = new EmployeeRepository().GetUserHistory(id);
            return PartialView("_History", model);
        }

        public JsonResult SaveHistory(UserHistoryViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateHistoryByEntity(data);
            else
                result = new EmployeeRepository().AddHistoryByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteHistory(int userid, int id)
        {
            var result = new EmployeeRepository().DeleteHistoryByID(userid, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

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