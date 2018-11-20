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

        public DataTable testExcel(string eduID)
        {
            DataTable data = new DataTable();
            using (SATEntities db = new SATEntities())
            {
                data = UtilityService.ToDataTable(db.tb_Degree.ToList());
            }

            return data;
        }

        public MemoryStream ReportExcel(DataTable data, string[] Header, string FileName)
        {
            try
            {
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
                Headerfont.IsBold = true;
                Headerfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

                XSSFCellStyle headerCellStyle = (XSSFCellStyle)book.CreateCellStyle();
                headerCellStyle.SetFont(Headerfont);
                //headerCellStyle.FillPattern = FillPattern.SolidForeground;
                headerCellStyle.BorderBottom = BorderStyle.Thin;
                headerCellStyle.BorderLeft = BorderStyle.Thin;
                headerCellStyle.BorderRight = BorderStyle.Thin;
                headerCellStyle.BorderTop = BorderStyle.Thin;
                headerCellStyle.WrapText = true;
                headerCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;


                XSSFFont Datafont = (XSSFFont)book.CreateFont();
                Datafont.FontHeightInPoints = 9;
                Datafont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                XSSFCellStyle dataCellStyle = (XSSFCellStyle)book.CreateCellStyle();
                dataCellStyle.SetFont(Datafont);
                dataCellStyle.BorderBottom = BorderStyle.Thin;
                dataCellStyle.BorderLeft = BorderStyle.Thin;
                dataCellStyle.BorderRight = BorderStyle.Thin;
                dataCellStyle.BorderTop = BorderStyle.Thin;

                #region styleCell
                //XSSFCellStyle dataCellDateStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellDateStyle.SetFont(Datafont);
                //dataCellDateStyle.BorderBottom = BorderStyle.Thin;
                //dataCellDateStyle.BorderLeft = BorderStyle.Thin;
                //dataCellDateStyle.BorderRight = BorderStyle.Thin;
                //dataCellDateStyle.BorderTop = BorderStyle.Thin;
                //dataCellDateStyle.DataFormat = book.CreateDataFormat().GetFormat("dd/MM/yyyy");

                //XSSFCellStyle dataCellNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellNumberStyle.SetFont(Datafont);
                //dataCellNumberStyle.BorderBottom = BorderStyle.Thin;
                //dataCellNumberStyle.BorderLeft = BorderStyle.Thin;
                //dataCellNumberStyle.BorderRight = BorderStyle.Thin;
                //dataCellNumberStyle.BorderTop = BorderStyle.Thin;

                //XSSFCellStyle dataCellNumberBoldStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellNumberBoldStyle.SetFont(Headerfont);
                //dataCellNumberBoldStyle.BorderBottom = BorderStyle.Thin;
                //dataCellNumberBoldStyle.BorderLeft = BorderStyle.Thin;
                //dataCellNumberBoldStyle.BorderRight = BorderStyle.Thin;
                //dataCellNumberBoldStyle.BorderTop = BorderStyle.Thin;

                //XSSFCellStyle dataCellPercentStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellPercentStyle.SetFont(Datafont);
                //dataCellPercentStyle.BorderBottom = BorderStyle.Thin;
                //dataCellPercentStyle.BorderLeft = BorderStyle.Thin;
                //dataCellPercentStyle.BorderRight = BorderStyle.Thin;
                //dataCellPercentStyle.BorderTop = BorderStyle.Thin;

                //XSSFFont DataRedfont = (XSSFFont)book.CreateFont();
                //DataRedfont.FontHeightInPoints = 9;
                //DataRedfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                //XSSFCellStyle dataCellRedNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellRedNumberStyle.SetFont(DataRedfont);
                //dataCellRedNumberStyle.BorderBottom = BorderStyle.Thin;
                //dataCellRedNumberStyle.BorderLeft = BorderStyle.Thin;
                //dataCellRedNumberStyle.BorderRight = BorderStyle.Thin;
                //dataCellRedNumberStyle.BorderTop = BorderStyle.Thin;

                //XSSFFont DataGreenfont = (XSSFFont)book.CreateFont();
                //DataGreenfont.FontHeightInPoints = 9;
                //DataGreenfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                //XSSFCellStyle dataCellGreenNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellGreenNumberStyle.SetFont(DataGreenfont);
                //dataCellGreenNumberStyle.BorderBottom = BorderStyle.Thin;
                //dataCellGreenNumberStyle.BorderLeft = BorderStyle.Thin;
                //dataCellGreenNumberStyle.BorderRight = BorderStyle.Thin;
                //dataCellGreenNumberStyle.BorderTop = BorderStyle.Thin;

                //XSSFFont DataBrownfont = (XSSFFont)book.CreateFont();
                //DataBrownfont.FontHeightInPoints = 9;
                //DataBrownfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

                //XSSFCellStyle dataCellBrownNumberStyle = (XSSFCellStyle)book.CreateCellStyle();
                //dataCellBrownNumberStyle.SetFont(DataBrownfont);
                //dataCellBrownNumberStyle.BorderBottom = BorderStyle.Thin;
                //dataCellBrownNumberStyle.BorderLeft = BorderStyle.Thin;
                //dataCellBrownNumberStyle.BorderRight = BorderStyle.Thin;
                //dataCellBrownNumberStyle.BorderTop = BorderStyle.Thin;

                #endregion

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
                int SeqNo = 0;
                int col = 0;
                foreach (DataRow model in data.Rows)
                {
                    sheet.CreateRow(currentRow);
                    XSSFRow dr = (XSSFRow)sheet.GetRow(currentRow);

                    col = 0;
                    SeqNo++;

                    for (col = 0; col < Header.Count(); col++)
                    {
                        dr.CreateCell(col, CellType.String).SetCellValue(col == 0 ? SeqNo.ToString() : model[col].ToString());
                        dr.GetCell(col).CellStyle = dataCellStyle;
                    }

                    currentRow++;

                }

                for (int j = 0; j < sheet.GetRow(0).LastCellNum; j++)
                {
                    sheet.AutoSizeColumn(j);
                }

                #endregion

                //using (FileStream file = new FileStream(Path.Combine(SysConfig.PathReport, FileName + ".xlsx"), FileMode.Create))
                //{
                //    book.Write(file);
                //}

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