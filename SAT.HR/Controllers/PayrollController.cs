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

        public ActionResult SalaryIncreaseEdit(int userid, int level, decimal step, decimal salary, int year, string fullname)
        {
            SalaryIncreaseSep2ViewModel model = new SalaryIncreaseSep2ViewModel();
            model.UserID = userid;
            model.Old_Level = level;
            model.New_Step = step;
            model.New_Salary = salary;
            model.Year = year;
            model.FullNameTh = fullname;
            ViewBag.SalaryStep = DropDownList.GetSalaryStep(step, level);
            return PartialView("_SalaryIncreaseEdit", model);
        }

        public JsonResult SalaryIncreaseConfirm(SalaryIncreaseSep1ViewModel step1, List<SalaryIncreaseSep2ViewModel> step2, SalaryIncreaseSep3ViewModel step3)
        {
            var result = new SalaryIncreaseRepository().SalaryIncreaseConfirm(step1, step2, step3);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public ActionResult BonusCalculatorEdit(int userid, decimal step, decimal bonus, int year, string fullname)
        {
            BonusCalculatorStep2ViewModel model = new BonusCalculatorStep2ViewModel();
            model.UserID = userid;
            model.UpStep = step;
            model.Bonus = bonus;
            model.Year = year;
            model.FullNameTh = fullname;
            return PartialView("_BonusCalculatorEdit", model);
        }

        public JsonResult BonusCalculatorConfirm(BonusCalculatorStep1ViewModel step1, List<BonusCalculatorStep2ViewModel> step2, BonusCalculatorStep3ViewModel step3)
        {
            var result = new BonusCalculatorRepository().BonusCalculatorConfirm(step1, step2, step3);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}