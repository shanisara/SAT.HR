using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    [AuthorizeUser]
    public class BenefitController : Controller
    {
        // ระบบสวัสดิการ
        public ActionResult Index()
        {
            return View();
        }

        #region  1. เงินตอบแทนความชอบ

        public ActionResult Remuneration()
        {
            return PartialView("_Remuneration");
        }

        #endregion

        #region  2. กองทุน

        public ActionResult ProvidentFund()
        {
            return PartialView("_ProvidentFund");
        }

        #endregion

        #region  3. รักษาพยาบาล

        public ActionResult MedicalTreatment()
        {
            return PartialView("_MedicalTreatment");
        }

        #endregion

        #region  4. เงินกู้

        public ActionResult Loan()
        {
            return PartialView("_Loan");
        }

        #endregion

        #region  5. ค่าเช่าบ้าน

        public ActionResult HomeRental()
        {
            return PartialView("_HomeRental");
        }

        #endregion

        #region  6. เงินช่วยเหลือบุตร

        public ActionResult ChildcareGrant()
        {
            return PartialView("_ChildcareGrant");
        }

        #endregion

        #region  7. เงินช่วยเหลือการศึกษาบุตร

        public ActionResult EducationalGrant()
        {
            return PartialView("_EducationalGrant");
        }

        #endregion

        #region  8. ฌาปนกิจสงเคราะห์

        public ActionResult ChaponkitRelief()
        {
            return PartialView("_ChaponkitRelief");
        }

        #endregion

        #region  9. เงินทดแทนกรณีเสียชีวิต

        public ActionResult CompensationDeath()
        {
            return PartialView("_OtherWelfare");
        }

        #endregion

        #region  10.เงินช่วยเหลือพิเศษกรณีเสียชีวิต

        public ActionResult SpecialGrantDeath()
        {
            return PartialView("_SpecialGrantDeath");
        }

        #endregion

        #region 11.สวัสดิการอื่นๆ

        public ActionResult OtherWelfare()
        {
            return PartialView("_OtherWelfare");
        }

        #endregion

    }
}