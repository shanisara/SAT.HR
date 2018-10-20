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
    public class EmployeeController : BaseController
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

        [HttpPost]
        public JsonResult Index(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns, int? userType, int? userStatus)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new EmployeeRepository().GetPage(search, draw, start, length, dir, column, userType, userStatus);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 1.1 Tab: User-Employee

        public ActionResult Employee(int id)
        {
            var model = new EmployeeRepository().GetByID(id);
            return PartialView("_Employee", model);
        }

        public ActionResult Detail(int id)
        {
            EmployeeViewModel model = new EmployeeRepository().GetByID(id);

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

        #region 1.2 Tab: User-Family

        public ActionResult FamilyByUser(int id)
        {
            var model = new EmployeeRepository().GetFamilyByUser(id);
            return PartialView("_Family", model);
        }

        public ActionResult FamilyDetail(int userid, int recid, int ufid)
        {
            var model = new EmployeeRepository().GetFamilyByID(userid, recid, ufid);

            ViewBag.MaritalStatus = DropDownList.GetMaritalStatus(model != null ? model.MaritalStatusID : null);
            ViewBag.Occupation = DropDownList.GetOccupation(model != null ? model.OcID : null);
            ViewBag.Position = DropDownList.GetPosition(model != null ? model.PoID : null, true);

            return PartialView("_FamilyDetail", model);
        }

        public JsonResult SaveFamily(UserFamilyViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UfID != 0)
                result = new EmployeeRepository().UpdateFamilyeByEntity(data);
            else
                result = new EmployeeRepository().AddFamilyByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFamily(int id)
        {
            var result = new EmployeeRepository().DeleteFamilyByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 1.3 Tab: User-Education

        public ActionResult EducationByUser()
        {
            return PartialView("_Education");
        }

        public ActionResult EducationDetail(int id)
        {
            var model = new EmployeeRepository().GetEducationByID(id);
            ViewBag.Education = DropDownList.GetEducation(model.EduID, true);
            ViewBag.Degree = DropDownList.GetDegree(model.DegID, true);
            ViewBag.Major = DropDownList.GetMajor(model.MajID, true);
            ViewBag.Country = DropDownList.GetCountry(model.CountryID);
            return PartialView("_EducationDetail", model);
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

        public JsonResult DeleteEducationy(int id)
        {
            var result = new EmployeeRepository().DeleteEducationByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Education(int id)
        {
            var list = new EmployeeRepository().GetEducationByUser(id);
            return Json(new { data = list.ListEducation }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 1.4 Tab: User-Position

        public ActionResult PositionByUser()
        {
            return PartialView("_Position");
        }

        public ActionResult PositionDetail(int id)
        {
            var model = new EmployeeRepository().GetPositionByID(id);
            ViewBag.ActionType = DropDownList.GetActionType(model.ActID, null);
            ViewBag.PositionType = DropDownList.GetPositionType(model.PoTID);
            ViewBag.Position = DropDownList.GetPosition(model.PoTID, true);
            ViewBag.Division = DropDownList.GetDivision(model.DivID, true);
            ViewBag.Department = DropDownList.GetDepartment(model.DivID, model.DepID, true);
            ViewBag.Section = DropDownList.GetSection(model.DivID, model.DepID, model.SecID, true);
            ViewBag.PositionAgent = DropDownList.GetPosition(model.PoAID, true);
            return PartialView("_PositionDetail", model);
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

        public JsonResult DeletePosition(int id)
        {
            var result = new EmployeeRepository().DeletePositionByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Position(int id)
        {
            var list = new EmployeeRepository().GetPositionByUser(id);
            return Json(new { data = list.ListPosition }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 1.5 Tab: User-Trainning

        public ActionResult TrainningByUser()
        {
            return PartialView("_Trainning");
        }

        public ActionResult TrainningDetail(int id)
        {
            var model = new EmployeeRepository().GetTrainningByID(id);
            ViewBag.TrainingType = DropDownList.GetTrainingType(model.TtID);
            ViewBag.Country = DropDownList.GetCountry(model.CountryID);
            return PartialView("_TrainningDetail", model);
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

        public JsonResult DeleteTrainning(int id)
        {
            var result = new EmployeeRepository().DeleteTrainingByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Trainning(int id)
        {
            var list = new EmployeeRepository().GetTrainningByUser(id);
            return Json(new { data = list.ListTrainning }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 1.6 Tab: User-Insignia

        public ActionResult InsigniaByUser()
        {
            return PartialView("_Insignia");
        }

        public ActionResult InsigniaDetail(int id)
        {
            var model = new EmployeeRepository().GetInsigniaByID(id);
            ViewBag.Insignia = DropDownList.GetInsignia(model.InsID, true);
            return PartialView("_InsigniaDetail", model);
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

        public JsonResult DeleteInsignia(int id)
        {
            var result = new EmployeeRepository().DeleteInsigniaByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insignia(int id)
        {
            var list = new EmployeeRepository().GetInsigniaByUser(id);
            return Json(new { data = list.ListInsignia }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 1.7 Tab: User-Excellent

        public ActionResult ExcellentByUser()
        {
            return PartialView("_Excellent");
        }

        public ActionResult ExcellentDetail(int id)
        {
            var model = new EmployeeRepository().GetExcellentByID(id);
            ViewBag.ExcellentType = DropDownList.GetExcellentType(model.ExTID);
            return PartialView("_ExcellentDetail", model);
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

        public JsonResult DeleteExcellent(int id)
        {
            var result = new EmployeeRepository().DeleteExcellentByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Excellent(int id)
        {
            var list = new EmployeeRepository().GetExcellentByUser(id);
            return Json(new { data = list.ListExcellent }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 1.8 Tab: User-Certificate

        public ActionResult CertificateByUser()
        {
            return PartialView("_Certificate");
        }

        public ActionResult CertificateDetail(int id)
        {
            var model = new EmployeeRepository().GetCertificateByUser(id);
            ViewBag.Certificate = DropDownList.GetCertificate(model.CerId);
            return PartialView("_CertificateDetail", model);
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

        public JsonResult DeleteCertificate(int id)
        {
            var result = new EmployeeRepository().DeleteCertificateByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Certificate(int id)
        {
            var list = new EmployeeRepository().GetCertificateByUser(id);
            return Json(new { data = list.ListCertificate }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 1.9 Tab: User-History

        public ActionResult HistoryByUser()
        {
            return PartialView("_History");
        }

        public ActionResult HistoryDetail(int id)
        {
            var model = new EmployeeRepository().GetHistoryByID(id);
            ViewBag.Title = DropDownList.GetTitle(model.SexID, model.TiID, true);
            return PartialView("_HistoryDetail", model);
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

        public JsonResult DeleteHistory(int id)
        {
            var result = new EmployeeRepository().DeleteHistoryByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult History(int id)
        {
            var list = new EmployeeRepository().GetHistoryByUser(id);
            return Json(new { data = list.ListHistory }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult EmployeeTransfer(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new EmployeeTransferRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeTransferDetail(int? id)
        {
            var model = new EmployeeTransferRepository().GetByID(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult EmployeeTransferDetailPage(int? id)
        {
            var list = new EmployeeTransferRepository().GetDetail(id);
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeTransferDetailByID(int? id)
        {
            var model = new PositionTransferRepository().GetDetailByID(id);
            ViewBag.Employee = DropDownList.GetEmployee(null, 1);
            ViewBag.SalaryLevel = DropDownList.GetSalaryLevel(null);
            ViewBag.SalaryStep = DropDownList.GetSalaryStep(null, 1);
            return PartialView("_EmployeeTransferDetail", model);
        }


        #endregion

        #region 4. โยกย้ายอัตรากำลังพล

        public ActionResult PositionTransfer()
        {
            ViewBag.UserType = DropDownList.GetUserType(1);
            return View();
        }

        [HttpPost]
        public JsonResult PositionTransfer(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns, int? usertype)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new PositionTransferRepository().GetPage(search, draw, start, length, dir, column, usertype);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PositionTransferDetail(int? id, int? type)
        {
            var model = new PositionTransferRepository().GetByID(id, type);
            ViewBag.UserTypeID = type;
            ViewBag.UserType = DropDownList.GetUserType(model != null ? model.UserTID : null);
            ViewBag.MoveType = DropDownList.GetMoveType(model != null ? model.MtID : null);
            return View(model);
        }

        [HttpPost]
        public JsonResult PositionTransferDetail(int? id)
        {
            var list = new PositionTransferRepository().GetDetail(id);
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PositionTransferDetailByID(int? id, int? type)
        {
            var model = new PositionTransferRepository().GetDetailByID(id);
            ViewBag.Employee = DropDownList.GetEmployee(null, type);
            ViewBag.PositionType = DropDownList.GetPositionType(null);
            ViewBag.Position = DropDownList.GetPosition(null, true);
            return PartialView("_PositionTransferDetail", model);
        }


        #endregion

    }
}