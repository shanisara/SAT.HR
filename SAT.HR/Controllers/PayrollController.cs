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

        public FileResult DownloadFileSalaryIncrease(int id)
        {
            var result = new SalaryIncreaseRepository().DownloadSalaryIncrease(id);
            if (result != null)
            {
                string fileName = result.FileName;
                string filePath = result.FilePath;
                string contentType = result.ContentType;
                return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
            }
            return null;
        }

        public ActionResult ExportSalaryIncreaseToExcel(SalaryIncreaseViewModel data)
        {
            List<SalaryIncreaseToExport> salaryinc = new SalaryIncreaseRepository().GetSalaryIncreaseToExport(data);
            string[] columns = { "Year", "Seq", "FullNameTh", "UpStep", "Level", "Old_Step", "New_Step", "Old_Salary", "New_Salary" };
            byte[] filecontent = ExcelWorksheetExtension.ExportExcel(salaryinc, string.Empty, true, columns);

            string handleSalary = Guid.NewGuid().ToString();
            TempData[handleSalary] = filecontent;

            //return File(filecontent, ExcelExportHelper.ExcelContentType, "SalaryIncrease.xlsx");
            return new JsonResult()
            {
                Data = new { FileGuid = handleSalary, FileName = "SalaryIncrease.xlsx" }
            };
        }

        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);

                return File(data, "application/vnd.ms-excel");
            }
            else
            {
                return new EmptyResult();
            }
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

        public JsonResult BonusCalculatorConfirm(BonusCalculatorViewModel data)
        {
            var result = new BonusCalculatorRepository().UpdateBonusCalculator(data);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFileBonusCalculator(int id)
        {
            var result = new BonusCalculatorRepository().DownloadBonusCalculator(id);
            string fileName = result.FileName;
            string filePath = result.FilePath;
            string contentType = result.ContentType;
            return new FilePathResult(System.IO.Path.Combine(filePath, fileName), contentType);
        }

        public ActionResult ExportBonusCalculatorToExcel(BonusCalculatorViewModel data)
        {
            List<BonusCalculatorToExport> bonuscal = new BonusCalculatorRepository().GetBonusCalculatorToExport(data);
            string[] columns = { "Year", "Seq", "FullNameTh", "Salary", "UpStep", "Bonus", "M10", "M11", "M12", "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9" };
            byte[] filecontent = ExcelWorksheetExtension.ExportExcel(bonuscal, string.Empty, true, columns);

            string handleBonus = Guid.NewGuid().ToString();
            TempData[handleBonus] = filecontent;

            //return File(filecontent, ExcelExportHelper.ExcelContentType, "BonusCalculator.xlsx");
            return new JsonResult()
            {
                Data = new { FileGuid = handleBonus, FileName = "BonusCalculator.xlsx" }
            };
        }

        #endregion

        #region 3. ประวัติการเลื่อนขั้นเงินเดือน

        public ActionResult SalaryIncreaseHistory()
        {
            //var model = new SalaryIncreaseRepository().SalaryIncreaseHistory();
            return View();
        }

        [HttpPost]
        public JsonResult SalaryIncreaseHistory(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new SalaryIncreaseRepository().GetSalaryIncreaseHistory(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalaryIncreaseHistoryDetail(int id)
        {
            var model = new SalaryIncreaseRepository().GetSalaryIncreaseHistoryDetail(id);
            return View(model);
        }


        #endregion

        #region 4. ประวัติการคำนวณโบนัส

        public ActionResult BonusCalculatorHistory()
        {
            //var model = new BonusCalculatorRepository().BonusCalculatorHistory();
            return View();
        }

        [HttpPost]
        public JsonResult BonusCalculatorHistory(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new BonusCalculatorRepository().GetBonusCalculatorHistory(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BonusCalculatorHistoryDetail(int id)
        {
            var model = new BonusCalculatorRepository().GetBonusCalculatorHistoryDetail(id);
            return View(model);
        }

        #endregion
    }
}