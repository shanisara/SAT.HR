using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT.HR.Models;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;

namespace SAT.HR.Controllers
{
    public class MasterController : BaseController
    {
        #region 1. ฝ่าย - Division

        public ActionResult Division()
        {
            return View();
        }

        public ActionResult DivisionDetail(int? id)
        {
            DivisionViewModel model = new DivisionViewModel();
            if (id.HasValue)
            {
                model = new DivisionRepository().GetByID((int)id);
            }
            return PartialView("_Division", model);
        }

        [HttpPost]
        public JsonResult Division(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new DivisionRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SaveDivision(DivisionViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if(model.DivID != 0)
                result = new DivisionRepository().UpdateByEntity(model);
            else
                result = new DivisionRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDivision(int id)
        {
            var result = new DivisionRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 2. ฝ่าย - Department

        public ActionResult Department()
        {
            return View();
        }

        public ActionResult DepartmentDetail(int? id)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            if (id.HasValue)
            {
                model = new DepartmentRepository().GetByID((int)id);
            }
            ViewBag.Division = DropDownList.GetDivision(model.DivID, false);
            return PartialView("_Department", model);
        }

        [HttpPost]
        public JsonResult Department(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new DepartmentRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDepartment(DepartmentViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.DepID != 0)
                result = new DepartmentRepository().UpdateByEntity(model);
            else
                result = new DepartmentRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDepartment(int id)
        {
            var result = new DepartmentRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DepartmentByDiv(int divid)
        {
            var result = DropDownList.GetDepartment(divid, null, false);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 3. หน่วยงาน - Section

        public ActionResult Section()
        {
            return View();
        }

        public ActionResult SectionDetail(int? id)
        {
            SectionViewModel model = new SectionViewModel();
            if (id.HasValue)
            {
                model = new SectionRepository().GetByID((int)id);
            }
            ViewBag.Division = DropDownList.GetDivision(model.DivID, false);
            ViewBag.Department = DropDownList.GetDepartment(model.DivID, model.DepID, false);
            return PartialView("_Section", model);
        }

        [HttpPost]
        public JsonResult Section(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new SectionRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSection(SectionViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.SecID != 0)
                result = new SectionRepository().UpdateByEntity(model);
            else
                result = new SectionRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteSection(int id)
        {
            var result = new SectionRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SectionByDep(int divid, int depid)
        {
            var result = DropDownList.GetSection(divid, depid, null, true);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 4. สายงาน - Discipline

        public ActionResult Discipline()
        {
            return View();
        }

        public ActionResult DisciplineDetail(int? id)
        {
            DisciplineViewModel model = new DisciplineViewModel();
            if (id.HasValue)
            {
                model = new DisciplineRepository().GetByID((int)id);
            }
            return PartialView("_Discipline", model);
        }

        [HttpPost]
        public JsonResult Discipline(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new DisciplineRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDiscipline(DisciplineViewModel model)
        {
            ResponseData result = new ResponseData();
            if (model.DisID != 0)
                result = new DisciplineRepository().UpdateByEntity(model);
            else
                result = new DisciplineRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDiscipline(int id)
        {
            var result = new DisciplineRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 5. ตำแหน่ง - Position

        public ActionResult Position()
        {
            return View();
        }

        public ActionResult PositionDetail(int? id)
        {
            PositionViewModel model = new PositionViewModel();
            if (id.HasValue)
            {
                model = new PositionRepository().GetByID((int)id);
            }
            return PartialView("_Position", model);
        }

        [HttpPost]
        public JsonResult Position(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new PositionRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePosition(PositionViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.PoID != 0)
                result = new PositionRepository().UpdateByEntity(model);
            else
                result = new PositionRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePosition(int id)
        {
            var result = new PositionRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult PositionBySec(int divid, int depid, int secid)
        //{
        //    var result = DropDownList.GetPosition(divid, depid, secid, null, true);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        

        #endregion

        #region 6. เงินเดือน - Salary

        public ActionResult Salary()
        {
            return View();
        }

        public ActionResult SalaryDetail(int? id)
        {
            SalaryViewModel model = new SalaryViewModel();
            if (id.HasValue)
            {
                model = new SalaryRepository().GetByID((int)id);
            }
            return PartialView("_Salary", model);
        }

        [HttpPost]
        public JsonResult Salary(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new SalaryRepository().GetPage(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSalary(SalaryViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.SaID != 0)
                result = new SalaryRepository().UpdateByEntity(model);
            else
                result = new SalaryRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteSalary(int id)
        {
            var result = new SalaryRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 7. ใบประกอบวิชาชีพ - Certificate

        public ActionResult Certificate()
        {
            return View();
        }

        public ActionResult CertificateDetail(int? id)
        {
            CertificateViewModel model = new CertificateViewModel();
            if (id.HasValue)
            {
                model = new CertificateRepository().GetByID((int)id);
            }
            return PartialView("_Certificate", model);
        }

        [HttpPost]
        public JsonResult Certificate(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new CertificateRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveCertificate(CertificateViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.CerID != 0)
                result = new CertificateRepository().UpdateByEntity(model);
            else
                result = new CertificateRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCertificate(int id)
        {
            var result = new CertificateRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 8. เครื่องราชอิสริยาภรณ์ - Insignia

        public ActionResult Insignia()
        {
            return View();
        }

        public ActionResult InsigniaDetail(int? id)
        {
            InsigniaViewModel model = new InsigniaViewModel();
            if (id.HasValue)
            {
                model = new InsigniaRepository().GetByID((int)id);
            }
            return PartialView("_Insignia", model);
        }

        [HttpPost]
        public JsonResult Insignia(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new InsigniaRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveInsignia(InsigniaViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.InsID != 0)
                result = new InsigniaRepository().UpdateByEntity(model);
            else
                result = new InsigniaRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteInsignia(int id)
        {
            var result = new InsigniaRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 9. คำนำหน้าชื่อ - Title

        public ActionResult Title()
        {
            return View();
        }

        public ActionResult TitleDetail(int? id)
        {
            TitleViewModel model = new TitleViewModel();
            if (id.HasValue)
            {
                model = new TitleRepository().GetByID((int)id);
            }
            ViewBag.Sex = DropDownList.GetTitle(model.SexID, id, true);
            return PartialView("_Title", model);
        }

        [HttpPost]
        public JsonResult Title(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];

            var dataTableData = new TitleRepository().GetPage(search, draw, start, length, dir, column);

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveTitle(TitleViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.TiID != 0)
                result = new TitleRepository().UpdateByEntity(model);
            else
                result = new TitleRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTitle(int id)
        {
            var result = new TitleRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SexByTitle(int titleid)
        {
            var result = new TitleRepository().GetSexByTitle(titleid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 10. ระดับการศึกษา - Education

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult EducationDetail(int? id)
        {
            EducationViewModel model = new EducationViewModel();
            if (id.HasValue)
            {
                model = new EducationRepository().GetByID((int)id);
            }
            return PartialView("_Education", model);
        }

        [HttpPost]
        public JsonResult Education(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new EducationRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEducation(EducationViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.EduID != 0)
                result = new EducationRepository().UpdateByEntity(model);
            else
                result = new EducationRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEducation(int id)
        {
            var result = new EducationRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 11. หลักสูตรปริญญา - Degree

        public ActionResult Degree()
        {
            return View();
        }

        public ActionResult DegreeDetail(int? id)
        {
            DegreeViewModel model = new DegreeViewModel();
            if (id.HasValue)
            {
                model = new DegreeRepository().GetByID((int)id);
            }
            return PartialView("_Degree", model);
        }

        [HttpPost]
        public JsonResult Degree(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new DegreeRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDegree(DegreeViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.DegID != 0)
                result = new DegreeRepository().UpdateByEntity(model);
            else
                result = new DegreeRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDegree(int id)
        {
            var result = new DegreeRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 12. สาขาวิชา - Major

        public ActionResult Major()
        {
            return View();
        }

        public ActionResult MajorDetail(int? id)
        {
            MajorViewModel model = new MajorViewModel();
            if (id.HasValue)
            {
                model = new MajorRepository().GetByID((int)id);
            }
            return PartialView("_Major", model);
        }

        [HttpPost]
        public JsonResult Major(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new MajorRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveMajor(MajorViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.MajID != 0)
                result = new MajorRepository().UpdateByEntity(model);
            else
                result = new MajorRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMajor(int id)
        {
            var result = new MajorRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 13. สัญชาติ - Nationality

        public ActionResult Nationality()
        {
            return View();
        }

        public ActionResult NationalityDetail(int? id)
        {
            NationalityViewModel model = new NationalityViewModel();
            if (id.HasValue)
            {
                model = new NationalityRepository().GetByID((int)id);
            }
            return PartialView("_Nationality", model);
        }

        [HttpPost]
        public JsonResult Nationality(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new NationalityRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNationality(NationalityViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.NatID != 0)
                result = new NationalityRepository().UpdateByEntity(model);
            else
                result = new NationalityRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteNationality(int id)
        {
            var result = new NationalityRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 14. ศาสนา - Religion

        public ActionResult Religion()
        {
            return View();
        }

        public ActionResult ReligionDetail(int? id)
        {
            ReligionViewModel model = new ReligionViewModel();
            if (id.HasValue)
            {
                model = new ReligionRepository().GetByID((int)id);
            }
            return PartialView("_Religion", model);
        }

        [HttpPost]
        public JsonResult Religion(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new ReligionRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveReligion(ReligionViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.RelD != 0)
                result = new ReligionRepository().UpdateByEntity(model);
            else
                result = new ReligionRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteReligion(int id)
        {
            var result = new ReligionRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 15. สมรรถนะ - Capability

        public ActionResult Capability()
        {
            return View();
        }

        public ActionResult CapabilityHeader(int? id)
        {
            CapabilityViewModel model = new CapabilityViewModel();
            if (id.HasValue)
            {
                model = new CapabilityRepository().GetByID((int)id);
            }
            ViewBag.CapabilityType = DropDownList.GetCapabilityType(model.CapTID);
            ViewBag.CapabilityGroup = DropDownList.GetCapabilityGroup(model.CapGID);
            ViewBag.CapabilityGroupType = DropDownList.GetCapabilityGroupType(model.CapGID, model.CapGTID);
            return PartialView("_Capability", model);
        }

        public JsonResult CapabilityGroupType(int capgid)
        {
            var result = DropDownList.GetCapabilityGroupType(capgid, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Capability(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new CapabilityRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveCapability(CapabilityViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.CapID != 0)
                result = new CapabilityRepository().UpdateByEntity(model);
            else
                result = new CapabilityRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCapability(int id)
        {
            var result = new CapabilityRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CapabilityDetail(int? id)
        {
            List<CapabilityDetailViewModel> model = new List<CapabilityDetailViewModel>();
            if (id.HasValue)
            {
                model = new CapabilityDetailRepository().GetByCap((int)id);
            }

            return PartialView("_CapabilityDetail", model);
        }

        public JsonResult SaveCapabilityDetail(CapabilityDetailViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.CapID != 0)
                result = new CapabilityDetailRepository().UpdateByEntity(model);
            else
                result = new CapabilityDetailRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubmitCapabilityDetail(List<CapabilityDetailViewModel> model)
        {
            ResponseData result = new Models.ResponseData();
            result = new CapabilityDetailRepository().SubmitByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteCapabilityDetail(int id)
        {
            var result = new CapabilityDetailRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 16. ประเภทการลา - LeaveType

        public ActionResult LeaveType()
        {
            return View();
        }

        public ActionResult LeaveTypeDetail(int? id)
        {
            LeaveTypeViewModel model = new LeaveTypeViewModel();
            if (id.HasValue)
            {
                model = new LeaveTypeRepository().GetByID((int)id);
            }
            ViewBag.Sex = DropDownList.GetSex(model.SexID);
            return PartialView("_LeaveType", model);
        }

        [HttpPost]
        public JsonResult LeaveType(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new LeaveTypeRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveLeaveType(LeaveTypeViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.LevID != 0)
                result = new LeaveTypeRepository().UpdateByEntity(model);
            else
                result = new LeaveTypeRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLeaveType(int id)
        {
            var result = new LeaveTypeRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 17. วันหยุด - Holiday

        public ActionResult Holiday()
        {
            return View();
        }

        public ActionResult HolidayDetail(int? id)
        {
            HolidayViewModel model = new HolidayViewModel();
            if (id.HasValue)
            {
                model = new HolidayRepository().GetByID((int)id);
            }
            return PartialView("_Holiday", model);
        }

        [HttpPost]
        public JsonResult Holiday(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new HolidayRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveHoliday(HolidayViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.HolID != 0)
                result = new HolidayRepository().UpdateByEntity(model);
            else
                result = new HolidayRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteHoliday(int id)
        {
            var result = new HolidayRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 18. รหัสการเคลื่อนไหว - ActionType

        public ActionResult ActionType()
        {
            return View();
        }

        public ActionResult ActionTypeDetail(int? id)
        {
            ActionTypeViewModel model = new ActionTypeViewModel();
            if (id.HasValue)
            {
                model = new ActionTypeRepository().GetByID((int)id);
            }
            return PartialView("_ActionType", model);
        }

        [HttpPost]
        public JsonResult ActionType(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new ActionTypeRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveActionType(ActionTypeViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.ActID != 0)
                result = new ActionTypeRepository().UpdateByEntity(model);
            else
                result = new ActionTypeRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteActionType(int id)
        {
            var result = new ActionTypeRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 19. ค่าคงที่ระบบ - SystemConfig

        public ActionResult SysConfig()
        {
            var data = new SysConfigRepository().GetAll();
            return View(data);
        }

        public JsonResult SaveSysConfig(List<SysConfigViewModel> model)
        {
            ResponseData result = new Models.ResponseData();
            result = new SysConfigRepository().UpdateByEntity(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 20. จัดการสิทธิการเข้าใช้งาน - UserRole

        public ActionResult Permission()
        {
            var data = new PermissionRepository().GetRoleAll();
            return View(data);
        }

        #region Role
        public ActionResult Role()
        {
            return PartialView("_Role");
        }

        public ActionResult RoleDetail(int? id)
        {
            RoleViewModel model = new RoleViewModel();
            if (id.HasValue)
            {
                model = new PermissionRepository().GetRoleByID((int)id);
            }
            return PartialView("_Role", model);
        }

        public JsonResult SaveRole(RoleViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.RoleID != 0)
                result = new PermissionRepository().UpdateRoleByEntity(model);
            else
                result = new PermissionRepository().AddRoleByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRole(int id)
        {
            var result = new PermissionRepository().RemoveRoleByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region User

        [HttpPost]
        public JsonResult Employee(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new EmployeeRepository().GetUserNotInRole(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Role User

        public ActionResult RoleUser(int id)
        {
            var data = new PermissionRepository().RoleUser(id);
            return View(data);
        }

        public JsonResult SaveRoleUser(int roleid, string users)
        {
            var result = new PermissionRepository().SaveRoleUser(roleid, users);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRoleUser(int roleid, int userid)
        {
            var result = new PermissionRepository().RemoveRoleUser(roleid, userid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Role Menu

        public ActionResult RoleMenu(int id)
        {
            var data = new PermissionRepository().MenuRole(id);
            return View(data);
        }

        public JsonResult SaveRoleMenu(List<RoleMenuViewModel> model)
        {
            var result = new PermissionRepository().SaveRoleMenu(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #endregion

        #region Province / District / SubDistrict

        public JsonResult Province(int? defaultValue)
        {
            var result = DropDownList.GetProvince(defaultValue);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult District(int? defaultValue, int? proid)
        {
            var result = DropDownList.GetDistrict(defaultValue, proid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubDistrict(int? defaultValue, int? proid, int? disid)
        {
            var result = DropDownList.GetSubDistrict(defaultValue, proid, disid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult DistrictByPro(int proid)
        //{
        //    var result = DropDownList.GetDistrict(proid, null);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SubDistrictByDis(int proid, int disid)
        //{
        //    var result = DropDownList.GetSubDistrict(proid, disid, null);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region  Salary 

        public JsonResult SalaryStepBylevel(int level)
        {
            var result = DropDownList.GetSalaryStep(null, level);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalaryByStep(int level, decimal step)
        {
            var result = new SalaryRepository().GetSalary(level, step);
            return Json( new { Salary = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  Detail

        public JsonResult GetDetailByUser(int userid)
        {
            var result = new MasterRepository().GetDetailByUser(userid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailByMp(int mpid)
        {
            var result = new MasterRepository().GetDetailByMp(mpid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public JsonResult GeDepartmentManPower(int divid, int type)
        {
            var result = DropDownList.GetDepartmentManPower(type, divid, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSectionManPower(int divid, int depid, int type)
        {
            var result = DropDownList.GetSectionManPower(type, divid, depid, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPositionManPower(int divid, int depid, int secid, int type)
        {
            var result = DropDownList.GetPositionManPower(type, divid, depid, secid, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}