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
    public class BonusCalculatorRepository
    {
        public BonusCalculatorViewModel BonusCalculator()
        {
            try
            {
                BonusCalculatorViewModel model = new BonusCalculatorViewModel();
                model.Year = DateTime.Now.Year + 543;
                model.Rate = (decimal)1.00;

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BonusCalculatorStep1ViewModel SalaryIncreaseSep1()
        {
            try
            {
                BonusCalculatorStep1ViewModel model = new BonusCalculatorStep1ViewModel();
                model.Year = DateTime.Now.Year + 543;
                model.Rate = (decimal)1.00;

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BonusCalculatorViewModel GetEmpBonusCalculator(int year, decimal step)
        {
            BonusCalculatorViewModel model = new BonusCalculatorViewModel();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    List<BonusCalculatorStep2ViewModel> listStep2 = new List<BonusCalculatorStep2ViewModel>();
                    var data = db.sp_Bonus_Calculator_List(year, step).ToList();

                    foreach (var item in data)
                    {
                        BonusCalculatorStep2ViewModel step2 = new BonusCalculatorStep2ViewModel();
                        step2.Year = item.Year;
                        step2.Seq = item.Seq;
                        step2.UpStep = item.UpStep;
                        step2.UserID = item.UserID;
                        step2.FullNameTh = item.FullNameTh;
                        step2.Bonus = item.Bonus;
                        step2.Salary = item.Salary;
                        step2.M1 = item.M1;
                        step2.M2 = item.M2;
                        step2.M3 = item.M3;
                        step2.M4 = item.M4;
                        step2.M5 = item.M5;
                        step2.M6 = item.M5;
                        step2.M7 = item.M5;
                        step2.M8 = item.M5;
                        step2.M9 = item.M5;
                        step2.M10 = item.M5;
                        step2.M11 = item.M5;
                        step2.M12 = item.M5;
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

        public ResponseData UpdateBonusCalculator(BonusCalculatorViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new ResponseData();
                    try
                    {
                        tb_Bonus_Calculator_Header head = new tb_Bonus_Calculator_Header();

                        #region FileUpload

                        if (data.FileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = data.FileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadBonusCalculator;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = (data.Step2.Count > 0 ? data.Step2[0].Seq : 1) + "_" + data.Year + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                data.PathFile = newFileName;
                            }
                        }

                        #endregion

                        #region tb_Bonus_Calculator_Header

                        head.Seq = data.Step2.Count > 0 ? data.Step2[0].Seq : 1;
                        head.Year = data.Year;
                        head.Rate = data.Rate;
                        head.BookCmd = data.BookCmd;
                        head.DateCmd = data.DateCmd;
                        head.PathFile = data.PathFile;
                        head.CreateBy = UtilityService.User.UserID;
                        head.CreateDate = DateTime.Now;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.tb_Bonus_Calculator_Header.Add(head);
                        db.SaveChanges();

                        #endregion 

                        int headerID = head.HeaderID;
                        if (data.Step2 != null)
                        {
                            foreach (var item in data.Step2)
                            {
                                #region tb_Bonus_Calculator_Detail

                                tb_Bonus_Calculator_Detail detail = new tb_Bonus_Calculator_Detail();
                                detail.HeaderID = headerID;
                                detail.Year = item.Year;
                                detail.UserID = item.UserID;
                                detail.FullNameTh = item.FullNameTh;
                                detail.UpStep = item.UpStep;
                                detail.Bonus = item.Bonus;
                                detail.Salary = item.Salary;
                                detail.M1 = item.M1;
                                detail.M2 = item.M2;
                                detail.M3 = item.M3;
                                detail.M4 = item.M4;
                                detail.M5 = item.M5;
                                detail.M6 = item.M6;
                                detail.M7 = item.M7;
                                detail.M8 = item.M8;
                                detail.M9 = item.M9;
                                detail.M10 = item.M10;
                                detail.M11 = item.M11;
                                detail.M12 = item.M12;
                                detail.CreateBy = UtilityService.User.UserID;
                                detail.CreateDate = DateTime.Now;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Bonus_Calculator_Detail.Add(detail);
                                db.SaveChanges();

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

        public FileViewModel DownloadBonusCalculator(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Bonus_Calculator_Header.Where(x => x.HeaderID == id).FirstOrDefault();
                    string filename = data.PathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadBonusCalculator;

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

        public List<BonusCalculatorToExport> GetBonusCalculatorToExport(BonusCalculatorViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                var model = new List<BonusCalculatorToExport>();
                foreach (var item in data.Step2)
                {
                    BonusCalculatorToExport obj = new Models.BonusCalculatorToExport();
                    obj.Year = item.Year;
                    obj.Seq = item.Seq;
                    obj.FullNameTh = item.FullNameTh;
                    obj.Salary = item.Salary;
                    obj.UpStep = item.UpStep;
                    obj.Bonus = item.Bonus;
                    obj.M10 = item.M10;
                    obj.M11 = item.M11;
                    obj.M12 = item.M12;
                    obj.M1 = item.M1;
                    obj.M2 = item.M2;
                    obj.M3 = item.M3;
                    obj.M4 = item.M4;
                    obj.M5 = item.M5;
                    obj.M6 = item.M6;
                    obj.M7 = item.M7;
                    obj.M8 = item.M8;
                    obj.M9 = item.M9;
                    model.Add(obj);
                }
                return model;
            }
        }

        public BonusCalculatorHeader GetBonusCalculatorHistory(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Bonus_Calculator_Header.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.Year.ToString().Contains(filter) || x.Seq.ToString().Contains(filter) || x.Rate.ToString().Contains(filter) || x.BookCmd.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    #region

                    case "Year":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.Year).ToList() : data.OrderByDescending(x => x.Year).ToList();
                        break;
                    case "Seq":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.Seq).ToList() : data.OrderByDescending(x => x.Seq).ToList();
                        break;
                    case "Rate":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.Rate).ToList() : data.OrderByDescending(x => x.Rate).ToList();
                        break;
                    case "BookCmd":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.BookCmd).ToList() : data.OrderByDescending(x => x.BookCmd).ToList();
                        break;
                    case "DateCmdText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DateCmd).ToList() : data.OrderByDescending(x => x.DateCmd).ToList();
                        break;

                        #endregion
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new BonusCalculatorHeaderViewModel()
                {
                    RowNumber = ++i,
                    HeaderID = s.HeaderID,
                    Year = s.Year,
                    Seq = s.Seq,
                    Rate = s.Rate,
                    BookCmd = s.BookCmd,
                    DateCmd = s.DateCmd,
                    PathFile = s.PathFile,
                    DateCmdText = s.DateCmd.HasValue ? s.DateCmd.Value.ToString("dd/MM/yyy") : string.Empty,
                }).Skip(start * length).Take(length).ToList();

                BonusCalculatorHeader result = new BonusCalculatorHeader();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public BonusCalculatorViewModel GetBonusCalculatorHistoryDetail(int id)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    BonusCalculatorViewModel model = new BonusCalculatorViewModel();

                    var header = db.tb_Bonus_Calculator_Header.Where(m => m.HeaderID == id).FirstOrDefault();
                    model.HeaderID = header.HeaderID;
                    model.Year = header.Year;
                    model.Rate = header.Rate;
                    model.BookCmd = header.BookCmd;
                    model.DateCmd = header.DateCmd;
                    model.PathFile = header.PathFile;
                    model.CreateBy = header.CreateBy;
                    model.CreateDate = header.CreateDate;

                    var list = new List<BonusCalculatorStep2ViewModel>();
                    var detail = db.tb_Bonus_Calculator_Detail.Where(m => m.HeaderID == id).ToList();
                    foreach (var item in detail)
                    {
                        BonusCalculatorStep2ViewModel obj = new BonusCalculatorStep2ViewModel();
                        obj.Year = item.Year;
                        obj.FullNameTh = item.FullNameTh;
                        obj.Salary = item.Salary;
                        obj.UpStep = item.UpStep;
                        obj.Bonus = item.Bonus;
                        obj.M10 = item.M10;
                        obj.M11 = item.M11;
                        obj.M12 = item.M12;
                        obj.M1 = item.M1;
                        obj.M2 = item.M2;
                        obj.M3 = item.M3;
                        obj.M4 = item.M4;
                        obj.M5 = item.M5;
                        obj.M6 = item.M6;
                        obj.M7 = item.M7;
                        obj.M8 = item.M8;
                        obj.M9 = item.M9;
                        obj.CreateBy = item.CreateBy;
                        obj.CreateDate = item.CreateDate;
                        list.Add(obj);
                    }
                    model.Step2 = list;

                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}