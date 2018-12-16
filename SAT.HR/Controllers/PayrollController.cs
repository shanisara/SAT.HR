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
            var model = new SalaryIncreaseRepository().SalaryIncrease();
            return View(model);
        }

        public ActionResult SalaryIncreaseStep1()
        {
            return PartialView("_SalaryIncreaseStep1");
        }

        public ActionResult SalaryIncreaseStep2(int year, int level, decimal step)
        {
            var model = new SalaryIncreaseRepository().GetEmpSalaryIncrease(year, level, step);
            return PartialView("_SalaryIncreaseStep2", model);
        }

        public ActionResult SalaryIncreaseStep3()
        {
            return PartialView("_SalaryIncreaseStep3");
        }

        public JsonResult SalaryIncreaseConfirm(SalaryIncreaseProcessViewModel data)
        {
            var result = new SalaryIncreaseRepository().SalaryIncreaseConfirm(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalaryIncreaseEdit()
        {

            return PartialView("_SalaryIncreaseEdit");
        }



        #endregion

        #region 2. การคำนวณโบนัส

        public ActionResult BonusCalculator()
        {
            var model = new BonusCalculatorRepository().BonusCalculator();
            return View(model);
        }

        public ActionResult BonusCalculatorStep1()
        {
            return PartialView("_BonusCalculatorStep1");
        }

        public ActionResult BonusCalculatorStep2(int year, int step)
        {
            var model = new BonusCalculatorRepository().GetEmpBonusCalculator(year, step);
            return PartialView("_BonusCalculatorStep2", model);
        }

        public ActionResult BonusCalculatorStep3()
        {
            return PartialView("_BonusCalculatorStep3");
        }

        public JsonResult BonusCalculatorConfirm(BonusCalculatorProcessViewModel data)
        {
            var result = new BonusCalculatorRepository().BonusCalculatorConfirm(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BonusCalculatorEdit()
        {

            return PartialView("_BonusCalculatorEdit");
        }

        #endregion
    }
}