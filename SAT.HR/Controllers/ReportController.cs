using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
        public MemoryStream ReportFile;

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

        public ActionResult ExportReportEducation(string UserID, string EduID, string ExtensionFile)
        {
            try
            {
                ExtensionFile = ".pdf";
                
                ReportClass rptH = new ReportClass();
                var file = Path.Combine(SysConfig.PathReport,"Report_Education_" + DateTime.Now.ToString("yyyyMMddHHmm") + ExtensionFile);         
                rptH.FileName = Server.MapPath(@"~/Report/Master/Report_Education.rpt");
                rptH.Load();                
                rptH.SetDatabaseLogon(SysConfig.UserName, SysConfig.Password, SysConfig.ServerName, SysConfig.DatabaseName);
                rptH.SetParameterValue("@eduID", "004");
                rptH.ExportToDisk(ExportFormatType.PortableDocFormat, file);

                var filePath = System.IO.File.ReadAllBytes(file);
                MemoryStream stream = new MemoryStream(filePath, false);

                return File(stream, "application/pdf","xxx.pdf");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}