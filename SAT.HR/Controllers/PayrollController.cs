using SAT.HR.Data;
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
    public class PayrollController : BaseController
    {
        // ระบบเงินเดือนและโบนัส

        #region 1. การเลื่อนขั้นเงินเดือน

        public ActionResult SalaryIncrease()
        {
            ViewBag.Year = DateTime.Now.Year + 543;
            return View();
        }

        public ActionResult GetEmpSalaryIncrease(int year, int level, decimal step)
        {
            var model = new SalaryIncreaseRepository().GetEmpSalaryIncrease(year, level, step);
            return PartialView("_SalaryIncrease", model);
        }

        public JsonResult SalaryIncreaseConfirm(SalaryIncreaseProcessViewModel data)
        {
            var result = new SalaryIncreaseRepository().SalaryIncreaseConfirm(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 2. การคำนวณโบนัส

        public ActionResult BonusCalculator()
        {
            ViewBag.Year = DateTime.Now.Year + 543;
            ViewBag.Rate = "1.00";
            return View();
        }

        public ActionResult GetEmpBonusCalculator(int year, int rate)
        {
            var model = new BonusCalculatorRepository().GetEmpBonusCalculator(year, rate);
            return PartialView("_BonusCalculator", model);
        }

        public JsonResult BonusCalculatorConfirm(BonusCalculatorProcessViewModel data)
        {
            var result = new BonusCalculatorRepository().BonusCalculatorConfirm(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}