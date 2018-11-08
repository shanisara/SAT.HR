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
    public class BenefitController : BaseController
    {
        // ระบบสวัสดิการ
        public ActionResult Index()
        {
            ViewBag.UserType = DropDownList.GetUserType(1);
            ViewBag.UserStatus = DropDownList.GetUserStatus(1);
            return View();
        }

        public ActionResult Detail(int id, int? type)
        {
            var model = new EmployeeRepository().GetByID(id);
            ViewBag.UserTypeID = type;
            return View(model);
        }


        #region  1. เงินตอบแทนความชอบ

        public ActionResult RemunerationByUser()
        {
            return PartialView("_Remuneration");
        }

        public ActionResult RemunerationDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetRemunerationByID(userid, id);
            ViewBag.RecieveType = DropDownList.GetRecieveType(model.RecID);
            return PartialView("_RemunerationDetail", model);
        }

        public JsonResult SaveRemuneration(BenefitRemunerationViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BrID != 0)
                result = new BenefitRepository().UpdateRemunerationByEntity(data);
            else
                result = new BenefitRepository().AddRemunerationByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRemuneration(int id)
        {
            var result = new BenefitRepository().DeleteRemunerationByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remuneration(int id)
        {
            var list = new BenefitRepository().GetRemunerationByUser(id);
            return Json(new { data = list.ListRemuneration }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  2. กองทุน

        
        public ActionResult ProvidentFundByUser()
        {
            return PartialView("_ProvidentFund");
        }

        public ActionResult ProvidentFundDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetProvidentFundByID(userid, id);
            ViewBag.BpAccumFundCu = DropDownList.GetAccumulativeFund(model.BpAccumFundCuID);
            ViewBag.BpAssoFundCu = DropDownList.GetAccumulativeFund(model.BpAssoFundCuID);
            return PartialView("_ProvidentFundDetail", model);
        }

        public JsonResult SaveProvidentFund(BenefitProvidentFundViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BpID != 0)
                result = new BenefitRepository().UpdateProvidentFundByEntity(data);
            else
                result = new BenefitRepository().AddProvidentFundByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProvidentFund(int id)
        {
            var result = new BenefitRepository().DeleteProvidentFundByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProvidentFund(int id)
        {
            var list = new BenefitRepository().GetProvidentFundByUser(id);
            return Json(new { data = list.ListProvidentFund }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  3. รักษาพยาบาล

        public ActionResult MedicalTreatmentByUser()
        {
            return PartialView("_MedicalTreatment");
        }

        public ActionResult MedicalTreatmentDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetMedicalTreatmentByID(userid, id);
            ViewBag.ClaimType = DropDownList.GetClaimType(model.ClID);
            ViewBag.RecieveType = DropDownList.GetRecieveType(model.RecID);
            return PartialView("_MedicalTreatmentDetail", model);
        }

        public JsonResult SaveMedicalTreatment(BenefitMedicalViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BmID != 0)
                result = new BenefitRepository().UpdateMedicalTreatmentByEntity(data);
            else
                result = new BenefitRepository().AddMedicalTreatmentByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMedicalTreatment(int id)
        {
            var result = new BenefitRepository().DeleteMedicalTreatmentByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MedicalTreatment(int id)
        {
            var list = new BenefitRepository().GetMedicalTreatmentByUser(id);
            return Json(new { data = list.ListMedical }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  4. เงินกู้

        public ActionResult LoanByUser()
        {
            return PartialView("_Loan");
        }

        public ActionResult LoanDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetLoanByID(userid, id);
            ViewBag.BankLoan = DropDownList.GetBankLoan(model.BID);
            ViewBag.LoanType = DropDownList.GetLoanType(model.LtID);
            return PartialView("_LoanDetail", model);
        }

        public JsonResult SaveLoan(BenefitLoanViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BlID != 0)
                result = new BenefitRepository().UpdateLoanByEntity(data);
            else
                result = new BenefitRepository().AddLoanByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLoan(int id)
        {
            var result = new BenefitRepository().DeleteLoanByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Loan(int id)
        {
            var list = new BenefitRepository().GetLoanByUser(id);
            return Json(new { data = list.ListLoan }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  5. ค่าเช่าบ้าน

        public ActionResult HomeRentalByUser()
        {
            return PartialView("_HomeRental");
        }

        public ActionResult HomeRentalDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetHomeRentalByID(userid, id);
            ViewBag.RentType = DropDownList.GetRentType(model.RID);
            ViewBag.PartType = DropDownList.GetPartType(model.PID);
            ViewBag.BhrLevel = DropDownList.GetSalaryLevel(null /*model.BhrLevel*/);
            ViewBag.BhrStep = DropDownList.GetSalaryLevel(null /*model.BhrStep*/);
            return PartialView("_HomeRentalDetail", model);
        }

        public JsonResult SaveHomeRental(BenefitHomeRentalViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BhrID != 0)
                result = new BenefitRepository().UpdateHomeRentalByEntity(data);
            else
                result = new BenefitRepository().AddHomeRentalByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteHomeRental(int id)
        {
            var result = new BenefitRepository().DeleteHomeRentalByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HomeRental(int id)
        {
            var list = new BenefitRepository().GetHomeRentalByUser(id);
            return Json(new { data = list.ListHomeRental }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  6. เงินช่วยเหลือบุตร

        public ActionResult ChildFundByUser()
        {
            return PartialView("_ChildFund");
        }

        public ActionResult ChildFundDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetChildFundByID(userid, id);
            return PartialView("_ChildFundDetail", model);
        }

        public JsonResult SaveChildFund(BenefitChildFundViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BcfID != 0)
                result = new BenefitRepository().UpdateChildFundByEntity(data);
            else
                result = new BenefitRepository().AddChildFundByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteChildFund(int id)
        {
            var result = new BenefitRepository().DeleteChildFundByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChildFund(int id)
        {
            var list = new BenefitRepository().GetChildFundByUser(id);
            return Json(new { data = list.ListChildFund }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  7. เงินช่วยเหลือการศึกษาบุตร

        public ActionResult ChildEducationByUser()
        {
            return PartialView("_ChildEducation");
        }

        public ActionResult ChildEducationDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetChildEducationByID(userid, id);
            return PartialView("_ChildEducationDetail", model);
        }

        public JsonResult SaveChildEducation(BenefitChildEducationViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BceID != 0)
                result = new BenefitRepository().UpdateChildEducationByEntity(data);
            else
                result = new BenefitRepository().AddChildEducationByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteChildEducation(int id)
        {
            var result = new BenefitRepository().DeleteChildEducationByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChildEducation(int id)
        {
            var list = new BenefitRepository().GetChildEducationByUser(id);
            return Json(new { data = list.ListChildEducation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  8. ฌาปนกิจสงเคราะห์

        public ActionResult CremationByUser()
        {
            return PartialView("_Cremation");
        }

        public ActionResult CremationDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetCremationByID(userid, id);
            ViewBag.MemberType = DropDownList.GetMemberType(model.MID);
            return PartialView("_CremationDetail", model);
        }

        public JsonResult SaveCremation(BenefitCremationViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BcID != 0)
                result = new BenefitRepository().UpdateCremationByEntity(data);
            else
                result = new BenefitRepository().AddCremationByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCremation(int id)
        {
            var result = new BenefitRepository().DeleteCremationByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Cremation(int id)
        {
            var list = new BenefitRepository().GetCremationByUser(id);
            return Json(new { data = list.ListCremation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  9. เงินทดแทนกรณีเสียชีวิต

        public ActionResult DeathReplacementByUser()
        {
            return PartialView("_DeathReplacement");
        }

        public ActionResult DeathReplacementDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetDeathReplacementByID(userid, id);
            ViewBag.RecieveType = DropDownList.GetRecieveType(model.RecID);
            //ViewBag.BdFullName = DropDownList.GetRecieveType(model.BdFullName);
            return PartialView("_DeathReplacementDetail", model);
        }

        public JsonResult SaveDeathReplacement(BenefitDeathReplacementViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BdID != 0)
                result = new BenefitRepository().UpdateDeathReplacementByEntity(data);
            else
                result = new BenefitRepository().AddDeathReplacementByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDeathReplacement(int id)
        {
            var result = new BenefitRepository().DeleteDeathReplacementByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeathReplacement(int id)
        {
            var list = new BenefitRepository().GetDeathReplacementByUser(id);
            return Json(new { data = list.ListDeathReplacement }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  10.เงินช่วยเหลือพิเศษกรณีเสียชีวิต

       
        public ActionResult DeathSubsidyByUser()
        {
            return PartialView("_DeathSubsidy");
        }

        public ActionResult DeathSubsidyDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetDeathSubsidyByID(userid, id);
            ViewBag.RecieveType = DropDownList.GetRecieveType(model.RecID);
            //ViewBag.BdFullName = DropDownList.GetRecieveType(model.BdFullName);
            return PartialView("_DeathSubsidyDetail", model);
        }

        public JsonResult SaveDeathSubsidy(BenefitDeathSubsidyViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BdID != 0)
                result = new BenefitRepository().UpdateDeathSubsidyByEntity(data);
            else
                result = new BenefitRepository().AddDeathSubsidyByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDeathSubsidy(int id)
        {
            var result = new BenefitRepository().DeleteDeathSubsidyByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeathSubsidy(int id)
        {
            var list = new BenefitRepository().GetDeathSubsidyByUser(id);
            return Json(new { data = list.ListDeathSubsidy }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 11.สวัสดิการอื่นๆ

        public ActionResult OtherWelfareByUser()
        {
            ViewBag.BenefitType = DropDownList.GetBenefitType(null);
            return PartialView("_OtherWelfare");
        }

        public ActionResult OtherWelfareDetail(int userid, int id)
        {
            var model = new BenefitRepository().GetOtherWelfareByID(userid, id);
            ViewBag.BenefitType = DropDownList.GetBenefitType(model.BenTID);
            ViewBag.RecieveType = DropDownList.GetRecieveType(model.BoRecID);
            ViewBag.RecieveType = DropDownList.GetRecieveType(model.BoOptRecID);
            return PartialView("_OtherWelfareDetail", model);
        }

        public JsonResult SaveOtherWelfare(BenefitOtherWelfareViewModel data)
        {
            ResponseData result = new Models.ResponseData();
            if (data.BoID != 0)
                result = new BenefitRepository().UpdateOtherWelfareByEntity(data);
            else
                result = new BenefitRepository().AddOtherWelfareByEntity(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteOtherWelfare(int id)
        {
            var result = new BenefitRepository().DeleteOtherWelfareByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OtherWelfare(int id)
        {
            var list = new BenefitRepository().GetOtherWelfareByUser(id);
            return Json(new { data = list.ListOtherWelfare }, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}