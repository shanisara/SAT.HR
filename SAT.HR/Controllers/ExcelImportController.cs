using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToExcel;
using System.Data.OleDb;
using System.Data;
using System.Data.Entity.Validation;
using SAT.HR.Data.Entities;

namespace SAT.HR.Controllers
{
    public class ExcelImportController : Controller
    {
        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }


        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase FileUpload, tb_Benefit_Cremation cremation)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);

                    #region

                    var result = from a in excelFile.Worksheet<tb_Benefit_Cremation>(sheetName) select a;
                    foreach (var a in result)
                    {
                        try
                        {
                            //if (a.UserID != "" && a.BcYear != "" && a.MID != "")
                            //{
                            //    tb_Benefit_Cremation BC = new tb_Benefit_Cremation();
                            //    BC.UserID = a.UserID;
                            //    BC.BcYear = a.BcYear;
                            //    BC.MID = a.MID;
                            //    db.tb_Benefit_Cremation.Add(BC);
                            //    db.SaveChanges();
                            //}
                            //else
                            //{
                            //    data.Add("<ul>");
                            //    if (a.UserID == "" || a.UserID == null) data.Add("<li> name is required</li>");
                            //    if (a.BcYear == "" || a.BcYear == null) data.Add("<li> Address is required</li>");
                            //    if (a.MID == "" || a.MID == null) data.Add("<li>ContactNo is required</li>");

                            //    data.Add("</ul>");
                            //    data.ToArray();
                            //    return Json(data, JsonRequestBehavior.AllowGet);
                            //}
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }

                    #endregion

                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}  