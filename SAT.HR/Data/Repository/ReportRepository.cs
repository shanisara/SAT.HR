using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace SAT.HR.Data.Repository
{
    
    public class ReportRepository
    {
        public List<EducationReport> ReportEducation(string UserID, string EduID)
        {
            List<EducationReport> model = new List<EducationReport>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    //var data = db.sp_Report_Education(UserID, EduID).ToList();


                    var data = db.sp_Report_Education(EduID).Select(x => new EducationReport()
                    {
                        //UserID = x.UserID == null ? "" : x.UserID.ToString(),
                        TiFullName = x.TiFullName == null ? "" : x.TiFullName.ToString(),
                        TiShortName = x.TiShortName == null ? "" : x.TiShortName.ToString(),
                        FirstNameTh = x.FirstNameTh == null ? "" : x.FirstNameTh.ToString(),
                        LastNameTh = x.LastNameTh == null ? "" : x.LastNameTh.ToString(),
                        FullNameTh = x.FullNameTh == null ? "" : x.FullNameTh.ToString(),
                        EduName = x.EduName == null ? "" : x.EduName.ToString(),
                        DegName = x.DegName == null ? "" : x.DegName.ToString(),
                        MajName = x.MajName == null ? "" : x.MajName.ToString(),
                        PoName = x.PoName == null ? "" : x.PoName.ToString(),
                        SalaryLevel = x.SalaryLevel == null ? "" : x.SalaryLevel.ToString()
                    }).ToList();

                    model = data;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return model;
        }

        public DataTable CallGenerateReport(string userID, string eduID)
        {
            DataSet dsReport = new DataSet();
            DataTable dtReport = null;
            using (SqlConnection conn = new SqlConnection(new SATEntities().Database.Connection.ConnectionString))
            {
                SqlDataAdapter daDetail = new SqlDataAdapter();
                SqlCommand cmmDetail = new SqlCommand
                {
                    CommandText = "sp_Report_Education",
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn,
                    CommandTimeout = 300
                };

                cmmDetail.Parameters.AddWithValue("@userId", userID);
                cmmDetail.Parameters.AddWithValue("@eduId", eduID);
                daDetail.SelectCommand = cmmDetail;
                daDetail.Fill(dtReport);

                daDetail.SelectCommand = cmmDetail;
                dtReport = dsReport.Tables[0];                
            }

            if (dsReport.Tables.Count > 0)
            {
                dtReport = dsReport.Tables[0];
            }

            return dtReport;
        }

        public MemoryStream ReportEducationExcel(List<EducationReport> data)
        {
            try
            {
                string[] Header = new string[] { "ที่", "ชื่อ-นามสกุล", "ตำแหน่ง", "ระดับ", "วุฒิ", "สาขา" };
                XSSFWorkbook book = new XSSFWorkbook();
                string SheetName = "Sheet1";
                XSSFSheet sheet = (XSSFSheet)book.CreateSheet(SheetName);
                XSSFPrintSetup print = (XSSFPrintSetup)sheet.PrintSetup;
                print.PaperSize = (short)NPOI.SS.UserModel.PaperSize.A4;
                print.Landscape = true;
                sheet.CreateFreezePane(1, 0);

                #region Cell Style

                XSSFFont Headerfont = (XSSFFont)book.CreateFont();
                Headerfont.FontHeightInPoints = 9;
                Headerfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

                XSSFCellStyle headerCellStyle = (XSSFCellStyle)book.CreateCellStyle();
                headerCellStyle.SetFont(Headerfont);
                headerCellStyle.FillPattern = FillPattern.SolidForeground;
                headerCellStyle.BorderBottom = BorderStyle.Thin;
                headerCellStyle.BorderLeft = BorderStyle.Thin;
                headerCellStyle.BorderRight = BorderStyle.Thin;
                headerCellStyle.BorderTop = BorderStyle.Thin;
                headerCellStyle.WrapText = true;
                headerCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                XSSFFont HeaderfontGreen = (XSSFFont)book.CreateFont();
                HeaderfontGreen.FontHeightInPoints = 9;
                HeaderfontGreen.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

                XSSFCellStyle headerGreenCellStyle = (XSSFCellStyle)book.CreateCellStyle();
                headerGreenCellStyle.SetFont(HeaderfontGreen);
                headerGreenCellStyle.FillPattern = FillPattern.SolidForeground;
                headerGreenCellStyle.BorderBottom = BorderStyle.Thin;
                headerGreenCellStyle.BorderLeft = BorderStyle.Thin;
                headerGreenCellStyle.BorderRight = BorderStyle.Thin;
                headerGreenCellStyle.BorderTop = BorderStyle.Thin;
                headerGreenCellStyle.WrapText = true;
                headerGreenCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                XSSFFont HeaderfontRed = (XSSFFont)book.CreateFont();
                HeaderfontRed.FontHeightInPoints = 9;
                HeaderfontRed.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

                XSSFCellStyle headerRedCellStyle = (XSSFCellStyle)book.CreateCellStyle();
                headerRedCellStyle.SetFont(HeaderfontRed);
                headerRedCellStyle.FillPattern = FillPattern.SolidForeground;
                headerRedCellStyle.BorderBottom = BorderStyle.Thin;
                headerRedCellStyle.BorderLeft = BorderStyle.Thin;
                headerRedCellStyle.BorderRight = BorderStyle.Thin;
                headerRedCellStyle.BorderTop = BorderStyle.Thin;
                headerRedCellStyle.WrapText = true;
                headerRedCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                XSSFFont Datafont = (XSSFFont)book.CreateFont();
                Datafont.FontHeightInPoints = 9;
                Datafont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                XSSFCellStyle dataCellStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellStyle.SetFont(Datafont);
                dataCellStyle.BorderBottom = BorderStyle.Thin;
                dataCellStyle.BorderLeft = BorderStyle.Thin;
                dataCellStyle.BorderRight = BorderStyle.Thin;
                dataCellStyle.BorderTop = BorderStyle.Thin;

                XSSFCellStyle dataCellDateStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellDateStyle.SetFont(Datafont);
                dataCellDateStyle.BorderBottom = BorderStyle.Thin;
                dataCellDateStyle.BorderLeft = BorderStyle.Thin;
                dataCellDateStyle.BorderRight = BorderStyle.Thin;
                dataCellDateStyle.BorderTop = BorderStyle.Thin;
                dataCellDateStyle.DataFormat = book.CreateDataFormat().GetFormat("dd/MM/yyyy");

                XSSFCellStyle dataCellNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellNumberStyle.SetFont(Datafont);
                dataCellNumberStyle.BorderBottom = BorderStyle.Thin;
                dataCellNumberStyle.BorderLeft = BorderStyle.Thin;
                dataCellNumberStyle.BorderRight = BorderStyle.Thin;
                dataCellNumberStyle.BorderTop = BorderStyle.Thin;

                XSSFCellStyle dataCellNumberBoldStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellNumberBoldStyle.SetFont(Headerfont);
                dataCellNumberBoldStyle.BorderBottom = BorderStyle.Thin;
                dataCellNumberBoldStyle.BorderLeft = BorderStyle.Thin;
                dataCellNumberBoldStyle.BorderRight = BorderStyle.Thin;
                dataCellNumberBoldStyle.BorderTop = BorderStyle.Thin;

                XSSFCellStyle dataCellPercentStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellPercentStyle.SetFont(Datafont);
                dataCellPercentStyle.BorderBottom = BorderStyle.Thin;
                dataCellPercentStyle.BorderLeft = BorderStyle.Thin;
                dataCellPercentStyle.BorderRight = BorderStyle.Thin;
                dataCellPercentStyle.BorderTop = BorderStyle.Thin;

                XSSFFont DataRedfont = (XSSFFont)book.CreateFont();
                DataRedfont.FontHeightInPoints = 9;
                DataRedfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                XSSFCellStyle dataCellRedNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellRedNumberStyle.SetFont(DataRedfont);
                dataCellRedNumberStyle.BorderBottom = BorderStyle.Thin;
                dataCellRedNumberStyle.BorderLeft = BorderStyle.Thin;
                dataCellRedNumberStyle.BorderRight = BorderStyle.Thin;
                dataCellRedNumberStyle.BorderTop = BorderStyle.Thin;

                XSSFFont DataGreenfont = (XSSFFont)book.CreateFont();
                DataGreenfont.FontHeightInPoints = 9;
                DataGreenfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                XSSFCellStyle dataCellGreenNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellGreenNumberStyle.SetFont(DataGreenfont);
                dataCellGreenNumberStyle.BorderBottom = BorderStyle.Thin;
                dataCellGreenNumberStyle.BorderLeft = BorderStyle.Thin;
                dataCellGreenNumberStyle.BorderRight = BorderStyle.Thin;
                dataCellGreenNumberStyle.BorderTop = BorderStyle.Thin;

                XSSFFont DataBrownfont = (XSSFFont)book.CreateFont();
                DataBrownfont.FontHeightInPoints = 9;
                DataBrownfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                XSSFCellStyle dataCellBrownNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellBrownNumberStyle.SetFont(DataBrownfont);
                dataCellBrownNumberStyle.BorderBottom = BorderStyle.Thin;
                dataCellBrownNumberStyle.BorderLeft = BorderStyle.Thin;
                dataCellBrownNumberStyle.BorderRight = BorderStyle.Thin;
                dataCellBrownNumberStyle.BorderTop = BorderStyle.Thin;

                #endregion

                #region header
                int currentRow = 0;
                sheet.CreateRow(currentRow);
                for (int j = 0; j < Header.Count(); j++)
                {
                    XSSFRow r = (XSSFRow)sheet.GetRow(currentRow);
                    r.CreateCell(j).SetCellValue(Header[j]);
                    r.GetCell(j).CellStyle = headerCellStyle;
                }
                currentRow++;
                #endregion

                #region Detail
                int SeqNo = 1;
                int col;
                sheet.CreateRow(currentRow);
                XSSFRow dr = (XSSFRow)sheet.GetRow(currentRow);

                foreach (EducationReport model in data)
                {
                    col = 0;
                    dr.CreateCell(col, CellType.Numeric).SetCellValue(SeqNo);
                    dr.GetCell(col).CellStyle = dataCellStyle;
                    SeqNo++;

                    col++;
                    dr.CreateCell(col, CellType.String).SetCellValue(model.FullNameTh.ToString());
                    dr.GetCell(col).CellStyle = dataCellStyle;

                    col++;
                    dr.CreateCell(col, CellType.String).SetCellValue(model.PoName.ToString());
                    dr.GetCell(col).CellStyle = dataCellStyle;

                    col++;
                    dr.CreateCell(col, CellType.String).SetCellValue(model.EduName.ToString());
                    dr.GetCell(col).CellStyle = dataCellStyle;

                    col++;
                    dr.CreateCell(col, CellType.String).SetCellValue(model.DegName.ToString());
                    dr.GetCell(col).CellStyle = dataCellStyle;

                    col++;
                    dr.CreateCell(col, CellType.String).SetCellValue(model.MajName.ToString());
                    dr.GetCell(col).CellStyle = dataCellStyle;

                    col++;
                }
                #endregion


                MemoryStream m = new MemoryStream();
                book.Write(m);
                return m;
            }
            catch (Exception ex)
            {
               throw new NotImplementedException();
            }
        }
    }
}