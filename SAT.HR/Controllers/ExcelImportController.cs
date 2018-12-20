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
using SAT.HR.Helpers;
using SAT.HR.Data.Repository;
using System.Globalization;

namespace SAT.HR.Controllers
{
    public class ExcelImportController : Controller
    {

        ImportRepository import = new ImportRepository();
        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }


        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase FileUpload, int modelMapping)
        {
            ResponseData result = new ResponseData();
            
            List<string> data = new List<string>();
            string[] LoanHeader = { "รหัสพนักงาน", "ปีบัญชี", "รหัสธนาคาร", "รหัสประเภทเงินกู้", "หมายเลขบัญชี", "วันที่เริ่มต้น", "วันที่สิ้นสุด", "วันที่ปิดบัญชี", "จำนวนงวด", "ชำระต่องวด", "ยอดเงินกู้", "หมายเหตุ" };
            string[] UserFamilyHeader = { "รหัสพนักงาน", "เลขบัตรประชาชน", "ชื่อ-นามสกุล", "วันเกิด", "วันที่หมดสิทธิ", "เงินทุนการศึกษา" };
            string[] ProvidentFundHeader = { "รหัสพนักงาน", "ปีบัญชี", "วันที่เปลี่ยนแปลงเงินสะสม", "รหัสเงินสะสม", "รหัสเงินสมทบ", "ผู้รับผลประโยชน์ลำดับที่ 1", "ผู้รับผลประโยชน์ลำดับที่ 2", "ผู้รับผลประโยชน์ลำดับที่ 3", "ผู้รับผลประโยชน์ลำดับที่ 4", "ผู้รับผลประโยชน์ลำดับที่ 5" };
            string[] HomeRentalHeader = { "รหัสพนักงานปีบัญชี", "รหัสประเภทค่าเช่า", "รหัสส่วน", "ระดับขั้น", "วันที่เริ่ม", "วันที่สิ้นสุด", "จำนวนเงิน", "หมายเหตุ" };
            string[] MedicalHeader = { "รหัสพนักงานปีบัญชีรหัสประเภทสิทธิรหัสผู้รับชื่อผู้รับเลขบัตรประชาชนวันที่เข้ารักษาค่าห้อง+ค่าอาหารค่ารักษาพยาบาลหมายเหตุ" };
            string[] CremationHeader = { "รหัสพนักงานปีบัญชี", "รหัสประเภทสมาชิก", "เลขสมาชิก", "วันสมัคร", "ผู้รับเงินสงเคราะห์", "ผู้รับผลประโยชน์ลำดับที่ 1", "ผู้รับผลประโยชน์ลำดับที่ 2", "ผู้รับผลประโยชน์ลำดับที่ 3", "ผู้รับผลประโยชน์ลำดับที่ 4", "ผู้รับผลประโยชน์ลำดับที่ 5" };
            string[] DeathSubsidyHeader = { "รหัสพนักงาน", "ปีบัญชี", "รหัสผู้รับ", "ชื่อผู้รับ", "ระยะเวลา", "ร้อยละ/เท่าจำนวนเงิน", "วันที่", "หมายเหตุ" };
            string[] DeathReplacementHeader = { "รหัสพนักงาน", "ปีบัญชี", "รหัสผู้รับ", "ชื่อผู้รับ", "ระยะเวลา", "ร้อยละ/เท่าจำนวนเงิน", "วันที่", "หมายเหตุ" };
            string[] RemunerationHeader = { "รหัสพนักงาน", "ปีบัญชี", "รหัสผู้รับ", "ชื่อผู้รับ", "จำนวนเงิน", "วันที่", "หมายเหตุ" };
            string[] UserFamily2Header = { "รหัสพนักงาน", "เลขบัตรประชาชน", "ชื่อ-นามสกุล", "วันเกิด", "วันที่หมดสิทธิ", "เงินทุนการศึกษา" };
            string[] OtherWelFareHeader = { "รหัสพนักงาน", "ปีบัญชี", "รหัสประเภทสวัสดิการ", "รหัสผู้รับผลประโยชน์", "ชื่อ-สกุล", "รหัสขอรับสวัสดิการ", "ชื่อ-สกุล", "ระยะเวลา", "ร้อยละ/เท่า", "จำนวนเงิน", "วันที่", "หมายเหตุ" };
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
                    

                    if (!ValidHeader.ValidateColumnHeader(dtable, modelMapping == 13 ? LoanHeader : 
                                                                ( modelMapping == 14 ? UserFamilyHeader : 
                                                                ( modelMapping == 15 ? ProvidentFundHeader : 
                                                                ( modelMapping == 16 ? HomeRentalHeader : 
                                                                ( modelMapping == 17 ? MedicalHeader : 
                                                                ( modelMapping == 18 ? CremationHeader :
                                                                ( modelMapping == 19 ? DeathSubsidyHeader :
                                                                ( modelMapping == 20 ? DeathReplacementHeader :
                                                                ( modelMapping == 21 ? RemunerationHeader :
                                                                ( modelMapping == 22 ? UserFamily2Header : OtherWelFareHeader)))))))))))
                    {
                        return Json(new { success = false, messegecode = "0001", messagetext = "รูปแบบไฟล์ไม่ถูกต้อง" }, JsonRequestBehavior.AllowGet);
                    }

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);

                    #region      

                    #region เงินกู้
                    if (modelMapping.Equals(13))
                    {
                        List<tb_Benefit_Loan> list = new List<tb_Benefit_Loan>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Loan l = new tb_Benefit_Loan();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BlYear = Convert.ToInt32(line[1].ToString());
                                    l.BID = Convert.ToInt32(line[2].ToString());
                                    l.LtID = Convert.ToInt32(line[3].ToString());
                                    l.BlAccountNo = Convert.ToInt32(line[4].ToString());
                                    l.BlStartDate = Convert.ToDateTime(Convert.ToDateTime(line[5].ToString()).ToString("yyyy-MM-dd"));
                                    l.BlEndDate = Convert.ToDateTime(Convert.ToDateTime(line[6].ToString()).ToString("yyyy-MM-dd"));
                                    l.BlCloseDate = Convert.ToDateTime(Convert.ToDateTime(line[7].ToString()).ToString("yyyy-MM-dd"));
                                    l.BlPeriod = Convert.ToInt32(line[8].ToString());
                                    l.BlPeriodPay = Convert.ToInt32(line[9].ToString());
                                    l.BISummaryAmout = Convert.ToInt32(line[10].ToString());
                                    l.BlRemark = line[11].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Benefit_Loan(list);
                    }

                    #endregion

                    #region เงินช่วยเหลือบุตร
                    if (modelMapping.Equals(14))
                    {
                        var list = new List<tb_User_Family>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_User_Family l = new tb_User_Family();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.UfCardID = Convert.ToInt32(line[1].ToString());
                                    l.ChildFundAmout = Convert.ToInt32(line[5].ToString());

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Update_User_Family(list,modelMapping);
                    }

                    #endregion

                    #region เงินสมทบกองทุนสำรองเลี้ยงชีพ
                    if (modelMapping.Equals(15))
                    {
                        var list = new List<tb_Benefit_Provident_Fund>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Provident_Fund l = new tb_Benefit_Provident_Fund();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BpYear = Convert.ToInt32(line[1].ToString());
                                    l.BpDateChangeFund = Convert.ToDateTime(Convert.ToDateTime(line[2].ToString()).ToString("yyyy-MM-dd"));
                                    l.BpAccumFundCuID = Convert.ToInt32(line[3].ToString());
                                    l.BpAssoFundCuID = Convert.ToInt32(line[4].ToString());
                                    l.BpBeneficiary1 = line[5].ToString();
                                    l.BpBeneficiary2 = line[6].ToString();
                                    l.BpBeneficiary3 = line[7].ToString();
                                    l.BpBeneficiary4 = line[8].ToString();
                                    l.BpBeneficiary5 = line[9].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Provident_Fund(list);
                    }

                    #endregion

                    #region ค่าเช่าบ้าน
                    if (modelMapping.Equals(16))
                    {
                        var list = new List<tb_Benefit_Home_Rental>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Home_Rental l = new tb_Benefit_Home_Rental();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BhrYear = Convert.ToInt32(line[1].ToString());
                                    l.RID = Convert.ToInt32(line[2].ToString());
                                    l.PID = Convert.ToInt32(line[3].ToString());
                                    l.BhrLevel = Convert.ToInt32(line[4].ToString());
                                    l.BhrStep = Convert.ToInt32(line[5].ToString());
                                    l.BhrStartDate = Convert.ToDateTime(Convert.ToDateTime(line[6].ToString()).ToString("yyyy-MM-dd"));
                                    l.BhrEndDate = (DateTime)line[7];
                                    l.BhrAmout = Convert.ToInt32(line[8].ToString());
                                    l.BhrRemark = line[9].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Home_Rental(list);
                    }

                    #endregion

                    #region รักษาพยาบาล
                    if (modelMapping.Equals(17))
                    {
                        var list = new List<tb_Benefit_Medical>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Medical l = new tb_Benefit_Medical();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BmYear = Convert.ToInt32(line[1].ToString());
                                    l.ClID = Convert.ToInt32(line[2].ToString());
                                    l.RecID = Convert.ToInt32(line[3].ToString());
                                    l.RecFullName = line[4].ToString();
                                    l.BmCardID = Convert.ToInt32(line[5].ToString());
                                    l.BmDate = Convert.ToDateTime(Convert.ToDateTime(line[6].ToString()).ToString("yyyy-MM-dd"));
                                    l.BmAmoutService = Convert.ToInt32(line[7].ToString());
                                    l.BmAmoutCare = Convert.ToInt32(line[8].ToString());
                                    l.BmRemark = line[9].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Medical(list);
                    }

                    #endregion

                    #region ฌาปนกิจสงเคราะห์
                    if (modelMapping.Equals(18))
                    {
                        var list = new List<tb_Benefit_Cremation>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Cremation l = new tb_Benefit_Cremation();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BcYear = Convert.ToInt32(line[1].ToString());
                                    l.MID = Convert.ToInt32(line[2].ToString());
                                    l.BcMemberNo = line[3].ToString();
                                    l.BcDate = Convert.ToDateTime(Convert.ToDateTime(line[4].ToString()).ToString("yyyy-MM-dd"));
                                    l.BcBeneficiary1 = line[5].ToString();
                                    l.BcBeneficiary2 = line[6].ToString();
                                    l.BcBeneficiary3 = line[7].ToString();
                                    l.BcBeneficiary4 = line[8].ToString();
                                    l.BcBeneficiary5 = line[9].ToString();
                                    l.BcRecFullName = line[10].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Cremation(list);
                    }

                    #endregion

                    #region เงินช่วยเหลือพิเศษกรณีเสียชีวิต
                    if (modelMapping.Equals(19))
                    {
                        var list = new List<tb_Benefit_Death_Subsidy>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Death_Subsidy l = new tb_Benefit_Death_Subsidy();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BdYear = Convert.ToInt32(line[1].ToString());
                                    l.RecID = Convert.ToInt32(line[2].ToString());
                                    l.BdFullName = line[3].ToString();
                                    l.BdTime = line[4].ToString();
                                    l.BdPer = Convert.ToInt32(line[5].ToString());
                                    l.BdAmout = Convert.ToInt32(line[6].ToString());
                                    l.BdDate = Convert.ToDateTime(Convert.ToDateTime(line[7].ToString()).ToString("yyyy-MM-dd"));
                                    l.BdRemark = line[8].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Death_Subsidy(list);
                    }

                    #endregion

                    #region เงินทดแทนกรณีเสียชีวิต
                    if (modelMapping.Equals(20))
                    {
                        var list = new List<tb_Benefit_Death_Replacement>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Death_Replacement l = new tb_Benefit_Death_Replacement();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BdYear = Convert.ToInt32(line[1].ToString());
                                    l.RecID = Convert.ToInt32(line[2].ToString());
                                    l.BdFullName = line[3].ToString();
                                    l.BdTime = line[4].ToString();
                                    l.BdPer = Convert.ToInt32(line[5].ToString());
                                    l.BdAmout = Convert.ToInt32(line[6].ToString());
                                    l.BdDate = Convert.ToDateTime(Convert.ToDateTime(line[7].ToString()).ToString("yyyy-MM-dd"));
                                    l.BdRemark = line[8].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Death_Replacement(list);
                    }

                    #endregion

                    #region เงินตอบแทนความชอบ
                    if (modelMapping.Equals(21))
                    {
                        var list = new List<tb_Benefit_Remuneration>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Remuneration l = new tb_Benefit_Remuneration();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BrYear = Convert.ToInt32(line[1].ToString());
                                    l.RecID = Convert.ToInt32(line[2].ToString());
                                    l.RecFullName = line[3].ToString();
                                    l.BrAmout = Convert.ToInt32(line[4].ToString());
                                    l.BrDate = Convert.ToDateTime(Convert.ToDateTime(line[5].ToString()).ToString("yyyy-MM-dd"));
                                    l.BrRemark = line[6].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Remuneration(list);
                    }

                    #endregion

                    #region เงินช่วยเหลือการศึกษาบุตร
                    if (modelMapping.Equals(21))
                    {
                        var list = new List<tb_User_Family>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_User_Family l = new tb_User_Family();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.UfCardID = Convert.ToInt32(line[1].ToString());
                                    l.ChildEducationAmout = Convert.ToInt32(line[5].ToString());

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Update_User_Family(list, modelMapping);
                    }

                    #endregion

                    #region สวัสดิการอื่นๆ
                    if (modelMapping.Equals(18))
                    {
                        var list = new List<tb_Benefit_Other_Welfare>();
                        foreach (DataRow line in dtable.Rows)
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(line[0].ToString()))
                                {
                                    tb_Benefit_Other_Welfare l = new tb_Benefit_Other_Welfare();

                                    l.UserID = Convert.ToInt32(line[0].ToString());
                                    l.BoYear = Convert.ToInt32(line[1].ToString());
                                    l.BenTID = Convert.ToInt32(line[2].ToString());
                                    l.BoRecID = Convert.ToInt32(line[3].ToString());
                                    l.BoRecFullName = line[4].ToString();
                                    l.BoOptRecID = Convert.ToInt32(line[5].ToString());
                                    l.BoOptFullName = line[6].ToString();
                                    l.BoTime = line[7].ToString();
                                    l.BoPer = Convert.ToInt32(line[8].ToString());
                                    l.BoAmout = Convert.ToInt32(line[9].ToString());
                                    l.BoDate = Convert.ToDateTime(Convert.ToDateTime(line[10].ToString()).ToString("yyyy-MM-dd"));
                                    l.BoRemark = line[11].ToString();

                                    list.Add(l);
                                }
                            }
                            catch (Exception exception)
                            {

                            }
                        }

                        import.Add_Other_Welfare(list);
                    }

                    #endregion

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