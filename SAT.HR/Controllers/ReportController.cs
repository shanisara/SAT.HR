using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SAT.HR.Controllers
{
    public class ReportController : BaseController
    {
        string FileName = string.Empty;

        #region // รายงาน:ส่วนงานทรัพยากรบุคคล

        public ActionResult Resource(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานสวัสดิการ

        public ActionResult Benefit(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        #endregion 

        #region // รายงาน:ส่วนงานพัฒนาบุคลากร

        public ActionResult Human(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        #endregion 

        public ActionResult ReportEducation()
        {
            var model = new EducationRepository().GetAll();
            ViewBag.Education = DropDownList.GetEducation(0, true);

            return View("Report_Education", model); ;
        }

        [HttpPost]
        public JsonResult ExportReportEducation(string EduID, string ExtensionFile)
        {
            try
            {
                //string[] Header = new string[] { "ลำดับ", "ที่", "ชื่อ-นามสกุล"};
                //ReportRepository Report = new ReportRepository();
                //var fileExcel = Report.ReportExcel(Report.testExcel(EduID), Header);

                ReportClass rptH = new ReportClass();
                FileName = "Report_Education" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ExtensionFile;
                rptH.FileName = Server.MapPath(@"~/Report/Master/Report_Education.rpt");
                rptH.Load();
                rptH.SetDatabaseLogon(SysConfig.UserName, SysConfig.Password, SysConfig.ServerName, SysConfig.DatabaseName);
                rptH.SetParameterValue("@eduID", EduID == "" ? null : EduID);
                rptH.ExportToDisk(ExtensionFile == ".xlsx" ? ExportFormatType.ExcelWorkbook : ExportFormatType.PortableDocFormat, Path.Combine(SysConfig.PathReport, FileName));

                return Json(FileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public FileResult DownloadFile(string fileName)
        {
            try
            {
                var filePath = System.IO.File.ReadAllBytes(Path.Combine(SysConfig.PathReport, fileName));
                return File(filePath, "application", fileName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
       
        }
        
    }
}