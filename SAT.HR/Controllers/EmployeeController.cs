using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
            ViewBag.Ethnicity = DropDownList.GetNationality(null, true);     //-- > เชื้อชาติ
            ViewBag.Nationality = DropDownList.GetNationality(null, true);   //--> สัญชาติ 

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

            ViewBag.Division = DropDownList.GetDivisionManPower(model.UserType, model.DivID);
            ViewBag.Department = DropDownList.GetDepartmentManPower(model.UserType, model.DivID, model.DepID);
            ViewBag.Section = DropDownList.GetSectionManPower(model.UserType, model.DivID, model.DepID, model.SecID);
            ViewBag.Position = DropDownList.GetPositionManPowerValuePo(model.UserType, model.DivID, model.DepID, model.SecID, model.PoID);

            ViewBag.Empower = DropDownList.GetEmpower(model.EmpowerID);     //-- > ช่วยราชการ
            ViewBag.EmpowerDivision = DropDownList.GetDivisionManPower(model.UserType, model.EmpowerDivID);
            ViewBag.EmpowerDepartment = DropDownList.GetDepartmentManPower(model.UserType, model.EmpowerDivID, model.EmpowerDepID);
            ViewBag.EmpowerSection = DropDownList.GetSectionManPower(model.UserType, model.EmpowerDivID, model.EmpowerDepID, model.EmpowerSecID);
            ViewBag.EmpowerPosition = DropDownList.GetPositionManPowerValuePo(model.UserType, model.EmpowerDivID, model.EmpowerDepID, model.EmpowerSecID, model.EmpowerID);

            ViewBag.PositionType = DropDownList.GetPositionType(model.AgentPoAID);//-- > รักษาการแทน
            ViewBag.AgentDivision = DropDownList.GetDivisionManPower(model.UserType, model.AgentDivID);
            ViewBag.AgentDepartment = DropDownList.GetDepartmentManPower(model.UserType, model.AgentDivID, model.AgentDepID);
            ViewBag.AgentSection = DropDownList.GetSectionManPower(model.UserType, model.AgentDivID, model.AgentDepID, model.AgentSecID);
            ViewBag.AgentPosition = DropDownList.GetPositionManPowerValuePo(model.UserType, model.AgentDivID, model.AgentDepID, model.AgentSecID, model.AgentPoID);

            ViewBag.HomeProvince = DropDownList.GetProvince(model.HomeProvinceID);
            ViewBag.HomeDistrict = DropDownList.GetDistrict(model.HomeDistrictID, model.HomeProvinceID);
            ViewBag.HomeSubDistrict = DropDownList.GetSubDistrict(model.HomeSubDistrictID, model.HomeProvinceID, model.HomeDistrictID);

            ViewBag.CurrProvince = DropDownList.GetProvince(model.CurrProvinceID);
            ViewBag.CurrDistrict = DropDownList.GetDistrict(model.CurrDistrictID, model.CurrProvinceID);
            ViewBag.CurrSubDistrict = DropDownList.GetSubDistrict(model.CurrSubDistrictID, model.CurrProvinceID, model.CurrDistrictID);

            ViewBag.UserStatus = DropDownList.GetUserStatus(model.StatusID);
            ViewBag.WorkingType = DropDownList.GetWorkingType(model.StatusID);

            ViewBag.UserTypeID = model.UserType;
            return PartialView("_Employee", model);
        }

        public ActionResult Detail(int id, int? type)
        {
            var model = new EmployeeRepository().GetByID(id);
            ViewBag.UserTypeID = type;
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveEmployee(EmployeeViewModel data, HttpPostedFileBase fileUpload)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UserID != 0)
                result = new EmployeeRepository().UpdateUserByEntity(data, fileUpload);
            else
                result = new EmployeeRepository().AddUserByEntity(data, fileUpload);
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

        public ActionResult FamilyDetail(int userid, int recid, int ufid, int? usertype)
        {
            var model = new EmployeeRepository().GetFamilyByID(userid, recid, ufid);

            ViewBag.MaritalStatus = DropDownList.GetMaritalStatus(model != null ? model.MaritalStatusID : null);
            ViewBag.Occupation = DropDownList.GetOccupation(model != null ? model.OcID : null);
            ViewBag.Position = DropDownList.GetPosition(model != null ? model.PoID : null, usertype, true);

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

        public JsonResult SaveEducation(UserEducationViewModel data, HttpPostedFileBase fileUpload)
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

        public ActionResult PositionDetail(int userid, int id, int? usertype)
        {
            var model = new EmployeeRepository().GetPositionByID(userid, id);
            ViewBag.ActionType = DropDownList.GetActionType(model.ActID, null);
            ViewBag.PositionType = DropDownList.GetPositionType(model.PoTID);
            ViewBag.PositionAgent = DropDownList.PositionAgent(model.PoAID);
            ViewBag.Division = DropDownList.GetDivisionManPower(usertype, model.DivID);
            ViewBag.Department = DropDownList.GetDepartmentManPower(usertype, model.DivID, model.DepID);
            ViewBag.Section = DropDownList.GetSectionManPower(usertype, model.DivID, model.DepID, model.SecID);
            ViewBag.Position = DropDownList.GetPositionManPowerValuePo(usertype, model.DivID, model.DepID, model.SecID, model.PoID);
            return PartialView("_PositionDetail", model);
        }

        public JsonResult SavePosition(UserPositionViewModel data, HttpPostedFileBase fileUpload)
        {
            ResponseData result = new Models.ResponseData();
            if (data.UpID != 0)
                result = new EmployeeRepository().UpdatePositionByEntity(data, fileUpload);
            else
                result = new EmployeeRepository().AddPositionByEntity(data, fileUpload);
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

        public FileResult DownloadPosition(int id)
        {
            var result = new EmployeeRepository().DownloadPosition(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(Path.Combine(filePath, fileName), contentType);
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

        [HttpPost]
        public JsonResult SaveInsignia(UserInsigniaViewModel data, HttpPostedFileBase fileUpload)
        {
            ResponseData result = new Models.ResponseData();

            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                //if (fileUpload.ContentLength > 10240)
                //{

                //}

                //var supportedTypes = new[] { "jpg", "jpeg", "png" };
                //var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);
                //if (!supportedTypes.Contains(fileExt))
                //{

                //}
            }

            if (data.UiID != 0)
                result = new EmployeeRepository().UpdateInsigniaByEntity(data, fileUpload);
            else
                result = new EmployeeRepository().AddInsigniaByEntity(data, fileUpload);
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

        public FileResult DownloadInsignia(int id)
        {
            var result = new EmployeeRepository().DownloadInsignia(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(Path.Combine(filePath, fileName), contentType);
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
            return View();
        }

        public ActionResult PositionRateDetail(int? id, int? type)
        {
            var model = new PositionRateRepository().GetByID(id);
            ViewBag.Division = DropDownList.GetDivision(model != null ? model.DivID : null, false);
            ViewBag.Department = DropDownList.GetDepartment(model != null ? model.DivID : null, model != null ? model.DepID: null, false);
            ViewBag.Section = DropDownList.GetSection(model != null ? model.DivID : null, model != null ? model.DepID: null, model != null ? model.SecID: null, false);
            ViewBag.Position = DropDownList.GetPosition(model != null ? model.PoID : null, type, false);
            ViewBag.Education = DropDownList.GetEducation(model != null ? model.EduID : null, true);
            ViewBag.TypeID = type;
            return PartialView("_PositionRate", model);
        }

        public JsonResult GetRoot(int id)
        {
            //var items = new[]
            //{
            //    new
            //    {
            //        id = "0",
            //        text = "การกีฬาแห่งประเทศไทย",
            //        state = new { opened = true },
            //        icon = SysConfig.ApplicationRoot + "Content/assets/img/home.png",
            //        children = new PositionRateRepository().GetTree(id)
            //    }
            //}
            //.ToList();

            var items = new PositionRateRepository().GetTree(id);
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetTreeAll(int id)
        {
            var items = new PositionRateRepository().GetTreeAll(id);
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetChildren(string parenttype, int usertype, string div, string dep, string sec, string po)
        {
            List<TreeViewModel> items = new PositionRateRepository().GetTree(parenttype, usertype, div, dep, sec, po);
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

        public ActionResult LevelTransfer()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LevelTransfer(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new LevelTransferRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LevelTransferDetail(int? id)
        {
            var model = new LevelTransferRepository().GetByID(id);
            return View(model);
        }

        //[HttpPost]
        //public JsonResult LevelTransferDetailPage(int? id)
        //{
        //    var list = new LevelTransferRepository().GetDetail(id);
        //    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult LevelTransferDetailByID(int? id)
        {
            var model = new LevelTransferRepository().GetDetailByID(id);
            ViewBag.Employee = DropDownList.GetEmployee(null, 1);
            ViewBag.SalaryLevel = DropDownList.GetSalaryLevel(null);
            ViewBag.SalaryStep = DropDownList.GetSalaryStep(null, 1);
            return PartialView("_LevelTransferDetail", model);
        }

        public JsonResult SaveLevelTransfer(LevelTransferViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.MlID != 0)
                result = new LevelTransferRepository().UpdateByEntity(data);
            else
                result = new LevelTransferRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLevelTransfer(int id)
        {
            var result = new LevelTransferRepository().DeleteLevelTransfer(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFileLevelTransfer(int id)
        {
            var result = new LevelTransferRepository().DownloadFileLevelTransfer(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(Path.Combine(filePath, fileName), contentType);
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

        //[HttpPost]
        //public JsonResult PositionTransferDetail(int? id)
        //{
        //    var list = new PositionTransferRepository().GetDetail(id);
        //    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult PositionTransferDetailByID(int? id, int? type)
        {
            var model = new PositionTransferRepository().GetDetailByID(id);
            ViewBag.Employee = DropDownList.GetEmployee(null, type);
            ViewBag.PositionType = DropDownList.GetPositionType(null);
            ViewBag.Position = DropDownList.GetPositionRate(null, type);
            return PartialView("_PositionTransferDetail", model);
        }

        public JsonResult SavePositionTransfer(PositionTransferViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.MopID != 0)
                result = new PositionTransferRepository().UpdateByEntity(data);
            else
                result = new PositionTransferRepository().AddByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFilePositionTransfer(int id)
        {
            var result = new PositionTransferRepository().DownloadFilePositionTransfer(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(Path.Combine(filePath, fileName), contentType);
        }

        #endregion

    }
}