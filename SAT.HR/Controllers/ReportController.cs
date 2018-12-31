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

            return View("~/Views/Report/Resource/Report_Education.cshtml", model); ;
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

            return View("~/Views/Report/Resource/Report_Employee.cshtml", model); ;
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

        #region รายงาน : เงินตอบแทนความชอบ
        public ActionResult ReportRemuneration()
        {            
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            ViewBag.ResignType = DropDownList.GetResignType(0).Where(x => x.Text != "ถึงแก่กรรม");
            ViewBag.UserType = DropDownList.GetUserType(0);
            return View("~/Views/Report/Benefit/Report_Remuneration.cshtml");
        }
        #endregion

        #region รายงาน : กองทุน
        public ActionResult ReportProvidentFund()
        {            
            ViewBag.Employee = DropDownList.GetEmployee(0, null);
            return View("~/Views/Report/Benefit/Report_ProvidentFund.cshtml");
        }
        #endregion

        #region รายงาน : รักษาพยาบาล
        public ActionResult ReportMedicalTreatment()
        {            
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_MedicalTreatment.cshtml");
        }
        #endregion

        #region รายงาน : เงินกู้
        public ActionResult ReportLoan()
        {            
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_Loan.cshtml");
        }
        #endregion

        #region รายงาน : ค่าเช่าบ้าน
        public ActionResult ReportHomeRental()
        {           
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_HomeRental.cshtml");
        }
        #endregion

        #region รายงาน : เงินช่วยเหลือบุตร
        public ActionResult ReportChildFund()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            
            return View("~/Views/Report/Benefit/Report_ChildFund.cshtml");
        }

        public JsonResult GetChildFund(int userid)
        {
            var data = new BenefitRepository().GetChildFundForReport(userid).ListChildFund;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region รายงาน : เงินช่วยเหลือการศึกษาบุตร
        public ActionResult ReportChildEducation()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_ChildEducation.cshtml");
        }
        #endregion

        #region รายงาน : ฌาปนกิจสงเคราะห์
        public ActionResult ReportCremation()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_Cremation.cshtml");
        }
        #endregion

        #region รายงาน : เงินทดแทนกรณีเสียชีวิต
        public ActionResult ReportDeathReplacement()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_DeathReplacement.cshtml");
        }
        #endregion

        #region รายงาน : เงินช่วยเหลือพิเศษกรณีเสียชีวิต
        public ActionResult ReportDeathSubsidy()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_DeathSubsidy.cshtml");
        }
        #endregion

        #region รายงาน : สวัสดิการอื่นๆ
        public ActionResult ReportOtherWelfare()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            return View("~/Views/Report/Benefit/Report_OtherWelfare.cshtml");
        }
        #endregion

        #region รายงาน : อุบัติเหตุ
        public ActionResult ReportAccident()
        {
            ViewBag.Employee = DropDownList.GetEmployee(0, 1);
            ViewBag.UserType = DropDownList.GetUserType(0);
            return View("~/Views/Report/Benefit/Report_Accident.cshtml");
        }
        #endregion

        [HttpPost]
        public JsonResult ExportReportBenefit(string EmpID, string ExtensionFile, string ReportName, string userType, string[] userID, string ResignID)
        {
            try
            {
                //Log.LogMessageToFile("start");

                string myConnString = "Provider=SQLOLEDB;Data Source=" + SysConfig.ServerName + ";Initial Catalog=" + SysConfig.DatabaseName + ";user id=" + SysConfig.UserName + ";password=" + SysConfig.Password + ";Connect Timeout=30";
                OleDbConnection myConnection = new OleDbConnection(myConnString);
                myConnection.Open();

                ReportClass rptH = new ReportClass();
                FileName = ReportName + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ExtensionFile;
                rptH.FileName = Server.MapPath(@"~/Report/Master/"+ ReportName + ".rpt");
                rptH.Load();
                rptH.DataSourceConnections[0].SetConnection(SysConfig.ServerName, SysConfig.DatabaseName, SysConfig.UserName, SysConfig.Password);

                rptH.SetParameterValue("@empID", EmpID == "" ? null : EmpID);


                if (userID != null)
                {
                    string userid = "";
                    int index = 0;
                    foreach (string x in userID)
                    {
                        index++;
                        userid += "'" + x.ToString() + "'";
                        if (index < userID.Count())
                        {
                            userid += ",";
                        }
                    }

                    rptH.SetParameterValue("@empID", userid);
                }

                if (userType != null)
                {
                    rptH.SetParameterValue("@userTypeID", userType == "" ? null : userType);
                }
                if (ResignID != null)
                {
                    rptH.SetParameterValue("@statusID", ResignID == "" ? null : ResignID);
                }

                rptH.ExportToDisk(ExtensionFile == ".xlsx" ? ExportFormatType.ExcelWorkbook : ExportFormatType.PortableDocFormat, Path.Combine(SysConfig.PathUploadReport, FileName));

                return Json(FileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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