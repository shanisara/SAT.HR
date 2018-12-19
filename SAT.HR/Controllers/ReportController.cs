using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;


namespace SAT.HR.Controllers
{
    public class ReportController : BaseController
    {
        string FileName = string.Empty;

        public ActionResult Resource(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        public ActionResult Benefit(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }

        public ActionResult Human(int id)
        {
            var data = new MenuRepository().MenuReportByRole(id);
            return View(data);
        }


        #region // รายงาน:ส่วนงานทรัพยากรบุคคล


        public ActionResult ReportEducation()
        {
            var model = new EducationRepository().GetAll();
            ViewBag.Education = DropDownList.GetEducation(0, true);

            return View("Report_Education", model); ;
        }


        public ActionResult ReportEmployee()
        {
            var model = new EducationRepository().GetAll();
            ViewBag.Division = DropDownList.GetDivision(null);
            ViewBag.Department = DropDownList.GetDepartment(null, null);
            ViewBag.Section = DropDownList.GetSection(null, null, null);
            ViewBag.Education = DropDownList.GetEducation(0, true);
            ViewBag.Religion = DropDownList.GetReligion(0, true);
            ViewBag.UserType = DropDownList.GetUserType(0);

            return View("Report_Employee", model); ;
        }

        [HttpPost]
        public JsonResult ExportReportEducation(string EduID, string ExtensionFile)
        {
            try
            {
                //Log.LogMessageToFile("start");

                string myConnString = "Provider=SQLOLEDB;Data Source=" + SysConfig.ServerName + ";Initial Catalog=" + SysConfig.DatabaseName + ";user id=" + SysConfig.UserName + ";password=" + SysConfig.Password + ";Connect Timeout=30";
                OleDbConnection myConnection = new OleDbConnection(myConnString);
                myConnection.Open();                

                ReportClass rptH = new ReportClass();
                FileName = "Report_Education" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ExtensionFile;
                rptH.FileName = Server.MapPath(@"~/Report/Master/Report_Education.rpt");
                rptH.Load();
                rptH.DataSourceConnections[0].SetConnection(SysConfig.ServerName, SysConfig.DatabaseName, SysConfig.UserName, SysConfig.Password);

                rptH.SetParameterValue("@eduID", EduID == "" ? null : EduID);
                rptH.ExportToDisk(ExtensionFile == ".xlsx" ? ExportFormatType.ExcelWorkbook : ExportFormatType.PortableDocFormat, Path.Combine(SysConfig.PathUploadReport, FileName));

                return Json(FileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult ExportReportEmployee(string DivID, string DepID, string SecID, string EduID, string InsName, string RelId, string UserTypeID, string ExtensionFile)
        {
            try
            {
                //Log.LogMessageToFile("start");
                string myConnString = "Provider=SQLOLEDB;Data Source=" + SysConfig.ServerName + ";Initial Catalog=" + SysConfig.DatabaseName + ";user id=" + SysConfig.UserName + ";password=" + SysConfig.Password + ";Connect Timeout=30";
                OleDbConnection myConnection = new OleDbConnection(myConnString);
                myConnection.Open();

                ReportClass rptH = new ReportClass();
                FileName = "Report_Employee" + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ExtensionFile;
                rptH.FileName = Server.MapPath(@"~/Report/Master/Report_Employee.rpt");
                rptH.Load();
                rptH.DataSourceConnections[0].SetConnection(SysConfig.ServerName, SysConfig.DatabaseName, SysConfig.UserName, SysConfig.Password);

                rptH.SetParameterValue("@divID", DivID == "" ? null : DivID);
                rptH.SetParameterValue("@depID", DepID == "" ? null : DepID);
                rptH.SetParameterValue("@secID", SecID == "" ? null : SecID);
                rptH.SetParameterValue("@eduID", EduID == "" ? null : EduID);
                rptH.SetParameterValue("@insName", InsName == "" ? null : InsName);
                rptH.SetParameterValue("@relID", RelId == "" ? null : RelId);
                rptH.SetParameterValue("@utID", UserTypeID == "" ? null : UserTypeID);
                rptH.ExportToDisk(ExtensionFile == ".xlsx" ? ExportFormatType.ExcelWorkbook : ExportFormatType.PortableDocFormat, Path.Combine(SysConfig.PathUploadReport, FileName));

                //Log.LogMessageToFile("GenFile");
                return Json(FileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region // รายงาน:ส่วนงานสวัสดิการ



        #endregion


        #region // รายงาน:ส่วนงานพัฒนาบุคลากร


        #endregion


        [HttpGet]
        public FileResult DownloadFile(string fileName)
        {
            try
            {
                //Log.LogMessageToFile("DownloadFile");


                var filePath = System.IO.File.ReadAllBytes(Path.Combine(SysConfig.PathDownloadReport, fileName));
                return File(filePath, "application", fileName);


                //string path = SysConfig.PathUploadReport + "/" + fileName; //Server.MapPath("~"+ SysConfig.PathDownloadReport+ "/" + fileName);

                //Log.LogMessageToFile(filePath.ToString());


                //return new FilePathResult(path, "application/pdf");


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}