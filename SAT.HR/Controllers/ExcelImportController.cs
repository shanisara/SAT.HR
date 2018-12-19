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
using NPOI.SS.Formula.Functions;
using SAT.HR.Models;

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
        public JsonResult UploadExcel(HttpPostedFileBase FileUpload,string modelMapping)
        {
            ResponseData result = new ResponseData();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
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

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    //var worksheetsList = excelFile.GetWorksheetNames();

                    #region

                    string sheetName = "Sheet1";
                    var xxx = from a in excelFile.Worksheet(1) select a;

                    //var list = new List<tb_Benefit>();
                    //foreach (var line in query)
                    //{
                    //    try
                    //    {
                    //        var surname = line[0].ToString();  
                    //        var name = line[1].ToString();  
                    //        var date = line[2].ToString();  
                    //        list.Add(new Benefit(surname, name, date));
                    //    }
                    //    catch (Exception exception)
                    //    {

                    //    }
                    //}
                    //return list;

                    #endregion

                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                }
                else
                {
                    result.MessageText = "Only Excel file format is allowed";
                }
            }
            else
            {
                result.MessageText = "Please choose Excel file";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}  