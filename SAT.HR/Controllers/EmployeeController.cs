﻿using SAT.HR.Data.Repository;
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
            ViewBag.UserType = DropDownList.GetUserType(1);
            ViewBag.UserStatus = DropDownList.GetUserStatus(1);
            return View();
        }

        public ActionResult Add(string type)
        {
            ViewBag.UserTypeID = type;
            ViewBag.UserTitle = DropDownList.GetTitle(null, null, true);
            ViewBag.Sex = DropDownList.GetSex(null);
            ViewBag.BloodType = DropDownList.GetBloodType(null);
            ViewBag.MaritalStatus = DropDownList.GetMaritalStatus(null);

            ViewBag.Religion = DropDownList.GetReligion(null, true);
            ViewBag.Ethnicity = DropDownList.GetNationality(null, true);       //-- > เชื้อชาติ
            ViewBag.Nationality = DropDownList.GetNationality(null, true);   //--> สัญชาติ 

            //ViewBag.SalaryLevel = DropDownList.GetSalaryLevel(null);
            //ViewBag.SalaryStep = DropDownList.GetSalaryStep(null, null);

            //ViewBag.Position = DropDownList.GetPosition(null, true);
            //ViewBag.Division = DropDownList.GetDivision(null, true);
            //ViewBag.Department = DropDownList.GetDepartment(null, null, true);
            //ViewBag.Section = DropDownList.GetSection(null, null, null, true);

            //ViewBag.Empower = DropDownList.GetEmpower(null);     //-- > ช่วยราชการ
            //ViewBag.EmpowerPosition = DropDownList.GetPosition(null, true);
            //ViewBag.EmpowerDivision = DropDownList.GetDivision(null, true);
            //ViewBag.EmpowerDepartment = DropDownList.GetDepartment(null, null, true);
            //ViewBag.EmpowerSection = DropDownList.GetSection(null, null, null, true);

            //ViewBag.PositionType = DropDownList.GetPositionType(null);//-- > รักษาการแทน
            //ViewBag.AgentPosition = DropDownList.GetPosition(null, true);
            //ViewBag.AgentDivision = DropDownList.GetDivision(null, true);
            //ViewBag.AgentDepartment = DropDownList.GetDepartment(null, null, true);
            //ViewBag.AgentSection = DropDownList.GetSection(null, null, null, true);

            ViewBag.HomeProvince = DropDownList.GetProvince(null);
            ViewBag.HomeDistrict = DropDownList.GetDistrict(null, null);
            ViewBag.HomeSubDistrict = DropDownList.GetSubDistrict(null, null, null);

            ViewBag.CurrProvince = DropDownList.GetProvince(null);
            ViewBag.CurrDistrict = DropDownList.GetDistrict(null, null);
            ViewBag.CurrSubDistrict = DropDownList.GetSubDistrict(null, null, null);

            ViewBag.UserStatus = DropDownList.GetUserStatus(null);
            ViewBag.WorkingType = DropDownList.GetWorkingType(null);

            return View();
        }

        [HttpPost]
        public JsonResult Index(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns, string userType, string userStatus)
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

            return PartialView("_Employee", model);
        }

        public ActionResult Detail(int id, int? type)
        {
            var model = new EmployeeRepository().GetByID(id);
            ViewBag.UserTypeID = type;
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
            var model = new EmployeeRepository().GetFamily(id);
            ViewBag.CountFather = model.CountFather;
            ViewBag.CountMother = model.CountMother;
            return PartialView("_Family");
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

        [HttpPost]
        public JsonResult Family(int id, int recid)
        {
            var list = new EmployeeRepository().GetFamilyByUser(id, recid);
            return Json(new { data = list.ListFamily }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 1.3 Tab: User-Education

        public ActionResult EducationByUser()
        {
            return PartialView("_Education");
        }

        public ActionResult EducationDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetEducationByID(userid, id);
            ViewBag.Education = DropDownList.GetEducation(model.EduID, true);
            ViewBag.Degree = DropDownList.GetDegree(model.DegID, true);
            ViewBag.Major = DropDownList.GetMajor(model.MajID, true);
            ViewBag.Country = DropDownList.GetCountry(model.CountryID);
            return PartialView("_EducationDetail", model);
        }

        public JsonResult SaveEducation(UserEducationViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UeID != 0)
                result = new EmployeeRepository().UpdateEducationByEntity(data);
            else
                result = new EmployeeRepository().AddEducationByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEducation(int id)
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

        public ActionResult PositionDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetPositionByID(userid, id);
            ViewBag.ActionType = DropDownList.GetActionType(model.ActID, null);
            ViewBag.PositionType = DropDownList.GetPositionType(model.PoTID);
            ViewBag.Position = DropDownList.GetPosition(model.PoID, true);
            ViewBag.Division = DropDownList.GetDivision(model.DivID, true);
            ViewBag.Department = DropDownList.GetDepartment(model.DivID, model.DepID, true);
            ViewBag.Section = DropDownList.GetSection(model.DivID, model.DepID, model.SecID, true);
            ViewBag.PositionAgent = DropDownList.GetPosition(model.PoAID, true);
            return PartialView("_PositionDetail", model);
        }

        public JsonResult SavePosition(UserPositionViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UpID != 0)
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

        public ActionResult TrainningDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetTrainningByID(userid, id);
            ViewBag.TrainingType = DropDownList.GetTrainingType(model.TtID);
            ViewBag.Country = DropDownList.GetCountry(model.CountryID);
            return PartialView("_TrainningDetail", model);
        }

        public JsonResult SaveTraining(UserTrainningViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UtID != 0)
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

        public ActionResult InsigniaDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetInsigniaByID(userid, id);
            ViewBag.Insignia = DropDownList.GetInsignia(model.InsID, true);
            return PartialView("_InsigniaDetail", model);
        }

        public JsonResult SaveInsignia(UserInsigniaViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UiID != 0)
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

        public ActionResult ExcellentDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetExcellentByID(userid, id);
            ViewBag.ExcellentType = DropDownList.GetExcellentType(model.ExTID);
            return PartialView("_ExcellentDetail", model);
        }

        public JsonResult SaveExcellent(UserExcellentViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UeID != 0)
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

        public ActionResult CertificateDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetCertificateByID(userid, id);
            ViewBag.Certificate = DropDownList.GetCertificate(model.CerId);
            return PartialView("_CertificateDetail", model);
        }

        public JsonResult SaveCertificate(UserCertificateViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UcID != 0)
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

        public ActionResult HistoryDetail(int userid, int id)
        {
            var model = new EmployeeRepository().GetHistoryByID(userid, id);
            ViewBag.Title = DropDownList.GetTitle(model.SexID, model.NewTiID, true);
            return PartialView("_HistoryDetail", model);
        }

        public JsonResult SaveHistory(UserHistoryViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UhID != 0)
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
            ViewBag.UserType = DropDownList.GetUserType(1);
            ViewBag.Division = DropDownList.GetDivision(null, true);
            ViewBag.Department = DropDownList.GetDepartment(null, null, true);
            ViewBag.Section = DropDownList.GetSection(null, null, null, true);
            ViewBag.Position = DropDownList.GetPosition(null, true);
            ViewBag.Education = DropDownList.GetEducation(null, true);
            return View();
        }

        public ActionResult PositionRateDetail(int? id)
        {
            var model = new PositionRateRepository().GetByID(id);
            ViewBag.Division = DropDownList.GetDivision(model != null ? model.DivID : null, false);
            ViewBag.Department = DropDownList.GetDepartment(model != null ? model.DivID : null, model != null ? model.DepID: null, false);
            ViewBag.Section = DropDownList.GetSection(model != null ? model.DivID : null, model != null ? model.DepID: null, model != null ? model.SecID: null, false);
            ViewBag.Position = DropDownList.GetPosition(model != null ? model.PoID : null, false);
            ViewBag.Education = DropDownList.GetEducation(model != null ? model.EduID : null, true);

            return PartialView("_PositionRate", model);
        }

        public JsonResult GetRoot()
        {
            var data = new PositionRateRepository().GetTree();
            var items = new[]
            {
                new
                {
                    id = "0",
                    text = "การกีฬาแห่งประเทศไทย",
                    state = new { opened = true },
                    children = data
                }
            }
            .ToList();
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetChildren(string type, int? divid, int? depid, int? secid, int? poid)
        {
            List<TreeViewModel> items = new PositionRateRepository().GetTree(type, divid, depid, secid, poid);
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //var g1 = Guid.NewGuid().ToString();
            //var g2 = Guid.NewGuid().ToString();

            //var items = new[]
            //{
            //    new { id = "child-" + g1, text = "Child " + g1, children = true },
            //    new { id = "child-" + g2, text = "Child " + g2, children = true }
            //}
            //.ToList();
        }

        public JsonResult SavePositionRate(PositionRateViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.MpID != 0)
                result = new PositionRateRepository().UpdateByEntity(data);
            else
                result = new PositionRateRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
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
            var model = new EmployeeTransferRepository().GetDetailByID(id);
            ViewBag.Employee = DropDownList.GetEmployee(null, 1);
            ViewBag.SalaryLevel = DropDownList.GetSalaryLevel(null);
            ViewBag.SalaryStep = DropDownList.GetSalaryStep(null, 1);
            return PartialView("_EmployeeTransferDetail", model);
        }

        public JsonResult SaveEmployeeTransfer(EmployeeTransferViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.MlID != 0)
                result = new EmployeeTransferRepository().UpdateByEntity(data);
            else
                result = new EmployeeTransferRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public JsonResult SaveEmployeeTransfer(PositionTransferViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.MtID != 0)
                result = new PositionTransferRepository().UpdateByEntity(data);
            else
                result = new PositionTransferRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}