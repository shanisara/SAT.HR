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

        public JsonResult SalaryIncreaseConfirm(SalaryIncreaseViewModel data)
        {
            var result = new SalaryIncreaseRepository().UpdateSalaryIncrease(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadSalaryIncrease(int id)
        {
            var result = new SalaryIncreaseRepository().DownloadSalaryIncrease(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
        }

        public FileContentResult ExportSalaryIncreaseToExcel(SalaryIncreaseViewModel data)
        {
            List<SalaryIncreaseToExport> salaryinc = new SalaryIncreaseRepository().GetSalaryIncreaseToExport(data);
            string[] columns = { "ปีบัญชี", "รอบที่", "ชื่อ นามสกุล", "อัตรา", "ระดับ", "ขั้นเก่า", "ขั้นใหม่", "เงินเดือนเก่า", "เงินเดือนใหม่" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(salaryinc, "Salary Increase", true, columns);

            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);

            Response.AppendHeader("content-disposition", "attachment;filename=SalaryIncrease.txt");

            //var filresult = File(filecontent, ExcelExportHelper.ExcelContentType);
            //return filresult;

            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("content-disposition", "attachment; filename=Statement_" + "SalaryIncrease" + ".xlsx");
            //Response.Write(filecontent);
            //Response.Flush();

            return new FileContentResult(filecontent, "application/vnd.ms-excel");

        }
					
        //public FileResult ExportSalaryIncrease(SalaryIncreaseViewModel data)
        //{
        //    var result = new SalaryIncreaseRepository().ExportSalaryIncrease(data);
        //    string fileName = result.FileName;
        //    string filePath = result.FilePath;
        //    string contentType = result.ContentType;
        //    return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
        //}


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

        public JsonResult BonusCalculatorConfirm(BonusCalculatorViewModel data)
        {
            var result = new BonusCalculatorRepository().UpdateBonusCalculator(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadBonusCalculator(int id)
        {
            var result = new BonusCalculatorRepository().DownloadBonusCalculator(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
        }

        public FileContentResult ExportBonusCalculatorToExcel(BonusCalculatorViewModel data)
        {
            List<BonusCalculatorToExport> bonuscal = new BonusCalculatorRepository().GetBonusCalculatorToExport(data);
            string[] columns = { "ปีบัญชี", "ชื่อ นามสกุล", "เงินเดือน", "อัตรา", "โบนัส", "M10", "M11", "M12", "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(bonuscal, "BonusCalculator", true, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "BonusCalculator.xlsx");
        }

        //public FileResult ExportBonusCalculatore(BonusCalculatorViewModel data)
        //{
        //    var result = new BonusCalculatorRepository().ExportBonusCalculator(data);
        //    string fileName = result.FileName;
        //    string filePath = result.FilePath;
        //    string contentType = result.ContentType;
        //    return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
        //}

        #endregion
    }
}