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

        public JsonResult SalaryIncreaseProcess(int year, int level, decimal step)
        {
            var result = new SalaryIncreaseRepository().SalaryIncreaseProcess(year, level, step);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public JsonResult BonusCalculatorProcess(int year, int level, decimal step)
        {
            var result = new BonusCalculatorRepository().BonusCalculatorProcess(year, level, step);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BonusCalculatorConfirm(BonusCalculatorProcessViewModel data)
        {
            var result = new BonusCalculatorRepository().BonusCalculatorConfirm(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}