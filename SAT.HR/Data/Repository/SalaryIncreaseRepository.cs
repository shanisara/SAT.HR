using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SAT.HR.Data
{
    public class SalaryIncreaseRepository
    {
        public SalaryIncreaseViewModel SalaryIncrease()
        {
            try
            {
                SalaryIncreaseViewModel model = new SalaryIncreaseViewModel();
                model.Year = DateTime.Now.Year + 543;
                model.UpLevel = 1;
                model.UpStep = (decimal)1.0;

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SalaryIncreaseSep1ViewModel SalaryIncreaseSep1()
        {
            try
            {
                SalaryIncreaseSep1ViewModel model = new SalaryIncreaseSep1ViewModel();
                model.Year = DateTime.Now.Year + 543;
                model.UpLevel = 1;
                model.UpStep = (decimal)1.0;

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SalaryIncreaseViewModel GetEmpSalaryIncrease(int year, int level, decimal step)
        {
            SalaryIncreaseViewModel model = new SalaryIncreaseViewModel();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    List<SalaryIncreaseSep2ViewModel> listStep2 = new List<SalaryIncreaseSep2ViewModel>();
                    var salaryincrease = db.sp_Salary_Increase_List(year, level, step).ToList();
                    foreach (var item in salaryincrease)
                    {
                        SalaryIncreaseSep2ViewModel step2 = new SalaryIncreaseSep2ViewModel();
                        step2.Year = item.Year;
                        step2.Seq = item.Seq;
                        step2.UpStep = item.UpStep;
                        step2.UserID = item.UserID;
                        step2.FullNameTh = item.FullNameTh;
                        step2.Old_Level = item.Old_Level;
                        step2.New_Level = item.New_Level;
                        step2.Old_Step = item.Old_Step;
                        step2.New_Step = item.New_Step;
                        step2.Old_Salary = item.Old_Salary;
                        step2.New_Salary = item.New_Salary;
                        listStep2.Add(step2);
                    }
                    model.Step2 = listStep2;
                }
                catch (Exception ex)
                {
                    throw;
                }
                return model;
            }
        }

        public ResponseData SalaryIncreaseConfirm(SalaryIncreaseViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new ResponseData();
                    try
                    {
                        tb_Salary_Increase_Header head = new tb_Salary_Increase_Header();

                        #region FileUpload

                        if (data.FileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = data.FileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadSalaryIncrease;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = data.PathFile + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.PathFile = newFileName;
                            }
                        }

                        #endregion

                        head.Year = data.Year;
                        head.UpLevel = data.UpLevel;
                        head.UpStep = data.UpStep;
                        head.BookCmd = data.BookCmd;
                        head.DateCmd = data.DateCmd;
                        head.DateEff = data.DateEff;
                        head.PathFile = data.PathFile;
                        head.CreateBy = UtilityService.User.UserID;
                        head.CreateDate = DateTime.Now;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.tb_Salary_Increase_Header.Add(head);
                        db.SaveChanges();

                        int headerID = head.HeaderID;
                        if (data.Step2 != null)
                        {
                            foreach (var item in data.Step2)
                            {
                                #region detail
                                if (Convert.ToBoolean(item.Selected))
                                {
                                    tb_Salary_Increase_Detail detail = new tb_Salary_Increase_Detail();
                                    detail.HeaderID = headerID;
                                    detail.Year = item.Year;
                                    detail.Seq = item.Seq;
                                    detail.UpStep = item.UpStep;
                                    detail.UserID = item.UserID;
                                    detail.Old_Level = item.Old_Level;
                                    detail.New_Level = item.New_Level;
                                    detail.Old_Step = item.Old_Step;
                                    detail.New_Step = item.New_Step;
                                    detail.Old_Salary = item.Old_Salary;
                                    detail.New_Salary = item.New_Salary;
                                    detail.CreateBy = UtilityService.User.UserID;
                                    detail.CreateDate = DateTime.Now;
                                    detail.ModifyBy = UtilityService.User.UserID;
                                    detail.ModifyDate = DateTime.Now;
                                    db.tb_Salary_Increase_Detail.Add(detail);
                                    db.SaveChanges();
                                }

                                #endregion 
                            }
                        }

                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }
                    return result;
                }
            }
        }

        public FileViewModel DownloadSalaryIncrease(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Salary_Increase_Header.Where(x => x.HeaderID == id).FirstOrDefault();
                    string filename = data.PathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadSalaryIncrease;

                    model.FileName = filename;
                    model.FilePath = filepath;
                    model.ContentType = Contenttype;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
    }
}