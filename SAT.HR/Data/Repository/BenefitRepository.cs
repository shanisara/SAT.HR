using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;

namespace SAT.HR.Data.Repository
{
    public class BenefitRepository
    {
        #region  1. เงินตอบแทนความชอบ

        public BenefitRemunerationViewModel GetRemunerationByUser(int userid)
        {
            var data = new BenefitRemunerationViewModel();
            var list = new List<BenefitRemunerationViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var remuneration = db.tb_Benefit_Remuneration.Where(x => x.UserID == userid).OrderByDescending(o => o.BrID).ToList();

                    foreach (var item in remuneration)
                    {
                        BenefitRemunerationViewModel model = new BenefitRemunerationViewModel();
                        model.RowNumber = index++;
                        model.BrID = item.BrID;
                        model.UserID = item.UserID;
                        model.BrYear = item.BrYear;
                        model.RecID = item.RecID;
                        model.RecFullName = item.RecFullName;
                        model.BrAmout = item.BrAmout;
                        model.BrRemark = item.BrRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.RecName = string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListRemuneration = list;
            return data;
        }

        public BenefitRemunerationViewModel GetRemunerationByID(int userid, int id)
        {
            BenefitRemunerationViewModel data = new BenefitRemunerationViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Remuneration.Where(x => x.BrID == id).FirstOrDefault();
                    BenefitRemunerationViewModel model = new BenefitRemunerationViewModel();
                    if (item != null)
                    {
                        model.BrID = item.BrID;
                        model.UserID = item.UserID;
                        model.BrYear = item.BrYear;
                        model.BrDate = item.BrDate;
                        model.RecID = item.RecID;
                        model.RecFullName = item.RecFullName;
                        model.BrAmout = item.BrAmout;
                        model.BrRemark = item.BrRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                    }
                    else
                    {
                        model.BrYear = DateTime.Now.Year;
                    }
                    data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddRemunerationByEntity(BenefitRemunerationViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Remuneration model = new tb_Benefit_Remuneration();
                    model.BrID = data.BrID;
                    model.UserID = data.UserID;
                    model.BrYear = data.BrYear;
                    if (Convert.ToDateTime(data.BrDate) > DateTime.MinValue)
                        model.BrDate = Convert.ToDateTime(data.BrDate);
                    model.RecID = data.RecID;
                    model.RecFullName = data.RecFullName;
                    model.BrAmout = data.BrAmout;
                    model.BrRemark = data.BrRemark;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Remuneration.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateRemunerationByEntity(BenefitRemunerationViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Remuneration.Single(x => x.UserID == newdata.UserID && x.BrID == newdata.BrID);
                    model.BrID = newdata.BrID;
                    model.UserID = newdata.UserID;
                    model.BrYear = newdata.BrYear;
                    if (Convert.ToDateTime(newdata.BrDate) > DateTime.MinValue)
                        model.BrDate = Convert.ToDateTime(newdata.BrDate);
                    model.RecID = newdata.RecID;
                    model.RecFullName = newdata.RecFullName;
                    model.BrAmout = newdata.BrAmout;
                    model.BrRemark = newdata.BrRemark;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteRemunerationByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Remuneration.SingleOrDefault(x => x.BrID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Remuneration.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  2. กองทุน

        public BenefitProvidentFundViewModel GetProvidentFundByUser(int userid)
        {
            var data = new BenefitProvidentFundViewModel();
            var list = new List<BenefitProvidentFundViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var user = db.tb_User.SingleOrDefault(c => c.UserID == userid);
                    data.ProvidentFundDate = user.ProvidentFundDate;
                    data.ProvidentFundNo = user.ProvidentFundNo;

                    int index = 1;
                    var providentFund = db.vw_Benefit_Provident_Fund.Where(x => x.UserID == userid).OrderByDescending(o => o.BpID).ToList();

                    foreach (var item in providentFund)
                    {
                        BenefitProvidentFundViewModel model = new BenefitProvidentFundViewModel();
                        model.RowNumber = index++;
                        model.BpID = item.BpID;
                        model.UserID = item.UserID;
                        model.BpYear = item.BpYear;
                        model.BpDateChangeFund = item.BpDateChangeFund;
                        model.BpAccumFundCuID = item.BpAccumFundCuID;
                        model.BpAssoFundCuID = item.BpAssoFundCuID;
                        model.BpBeneficiary1 = item.BpBeneficiary1;
                        model.BpBeneficiary2 = item.BpBeneficiary2;
                        model.BpBeneficiary3 = item.BpBeneficiary3;
                        model.BpBeneficiary4 = item.BpBeneficiary4;
                        model.BpBeneficiary5 = item.BpBeneficiary5;
                        model.BpBeneficiary1Percent = item.BpBeneficiary1Percent;
                        model.BpBeneficiary2Percent = item.BpBeneficiary2Percent;
                        model.BpBeneficiary3Percent = item.BpBeneficiary3Percent;
                        model.BpBeneficiary4Percent = item.BpBeneficiary4Percent;
                        model.BpBeneficiary5Percent = item.BpBeneficiary5Percent;
                        model.BpBeneficiary = GetAllBpBeneficiary(item.BpBeneficiary1, item.BpBeneficiary2, item.BpBeneficiary3, item.BpBeneficiary4, item.BpBeneficiary5);
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BpDateChangeFundText = (item.BpDateChangeFund.HasValue) ? item.BpDateChangeFund.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.BpAccumFundCuName = item.BpAccumFundCuName;
                        model.BpAssoFundCuName = item.BpAssoFundCuName;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListProvidentFund = list;
            return data;
        }

        public BenefitProvidentFundViewModel GetProvidentFundByID(int userid, int id)
        {
            BenefitProvidentFundViewModel data = new BenefitProvidentFundViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Provident_Fund.Where(x => x.BpID == id).FirstOrDefault();
                    BenefitProvidentFundViewModel model = new BenefitProvidentFundViewModel();
                    model.BpID = item.BpID;
                    model.UserID = item.UserID;
                    model.BpYear = item.BpYear;
                    model.BpDateChangeFund = item.BpDateChangeFund;
                    model.BpAccumFundCuID = item.BpAccumFundCuID;
                    model.BpAssoFundCuID = item.BpAssoFundCuID;
                    model.BpBeneficiary1 = item.BpBeneficiary1;
                    model.BpBeneficiary2 = item.BpBeneficiary2;
                    model.BpBeneficiary3 = item.BpBeneficiary3;
                    model.BpBeneficiary4 = item.BpBeneficiary4;
                    model.BpBeneficiary5 = item.BpBeneficiary5;
                    model.BpBeneficiary1Percent = item.BpBeneficiary1Percent;
                    model.BpBeneficiary2Percent = item.BpBeneficiary2Percent;
                    model.BpBeneficiary3Percent = item.BpBeneficiary3Percent;
                    model.BpBeneficiary4Percent = item.BpBeneficiary4Percent;
                    model.BpBeneficiary5Percent = item.BpBeneficiary5Percent;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddProvidentFundByEntity(BenefitProvidentFundViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Provident_Fund model = new tb_Benefit_Provident_Fund();
                    model.BpID = data.BpID;
                    model.UserID = data.UserID;
                    model.BpYear = data.BpYear;
                    model.BpAccumFundCuID = data.BpAccumFundCuID;
                    model.BpAssoFundCuID = data.BpAssoFundCuID;
                    model.BpBeneficiary1 = data.BpBeneficiary1;
                    model.BpBeneficiary2 = data.BpBeneficiary2;
                    model.BpBeneficiary3 = data.BpBeneficiary3;
                    model.BpBeneficiary4 = data.BpBeneficiary4;
                    model.BpBeneficiary5 = data.BpBeneficiary5;
                    model.BpBeneficiary1Percent = data.BpBeneficiary1Percent;
                    model.BpBeneficiary2Percent = data.BpBeneficiary2Percent;
                    model.BpBeneficiary3Percent = data.BpBeneficiary3Percent;
                    model.BpBeneficiary4Percent = data.BpBeneficiary4Percent;
                    model.BpBeneficiary5Percent = data.BpBeneficiary5Percent;
                    if (data.BpDateChangeFund.HasValue)
                        model.BpDateChangeFund = data.BpDateChangeFund;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Provident_Fund.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateProvidentFundByEntity(BenefitProvidentFundViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Provident_Fund.Single(x => x.UserID == newdata.UserID && x.BpID == newdata.BpID);
                    model.BpID = newdata.BpID;
                    model.UserID = newdata.UserID;
                    model.BpYear = newdata.BpYear;
                    model.BpAccumFundCuID = newdata.BpAccumFundCuID;
                    model.BpAssoFundCuID = newdata.BpAssoFundCuID;
                    model.BpBeneficiary1 = newdata.BpBeneficiary1;
                    model.BpBeneficiary2 = newdata.BpBeneficiary2;
                    model.BpBeneficiary3 = newdata.BpBeneficiary3;
                    model.BpBeneficiary4 = newdata.BpBeneficiary4;
                    model.BpBeneficiary5 = newdata.BpBeneficiary5;
                    model.BpBeneficiary1Percent = newdata.BpBeneficiary1Percent;
                    model.BpBeneficiary2Percent = newdata.BpBeneficiary2Percent;
                    model.BpBeneficiary3Percent = newdata.BpBeneficiary3Percent;
                    model.BpBeneficiary4Percent = newdata.BpBeneficiary4Percent;
                    model.BpBeneficiary5Percent = newdata.BpBeneficiary5Percent;
                    if(newdata.BpDateChangeFund.HasValue)
                        model.BpDateChangeFund = newdata.BpDateChangeFund;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }
        
        public BenefitProvidentFundViewModel GetProvidentFund(int userid)
        {
            var data = new BenefitProvidentFundViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var user = db.tb_User.SingleOrDefault(c => c.UserID == userid);
                    data.UserID = user.UserID;
                    data.ProvidentFundDate = user.ProvidentFundDate;
                    data.ProvidentFundNo = user.ProvidentFundNo;
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseData DeleteProvidentFundByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Provident_Fund.SingleOrDefault(x => x.BpID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Provident_Fund.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public string GetAllBpBeneficiary(string data1, string data2, string data3, string data4, string data5)
        {
            string Beneficiary = "<div style='list-style:decimal; text-align:left; font-size:12px; color:blue'>";

            if (!string.IsNullOrEmpty(data1))
                Beneficiary = Beneficiary + "<li>" + data1 + "</li>";

            if (!string.IsNullOrEmpty(data2))
                Beneficiary = Beneficiary + "<li>" + data2 + "</li>";

            if (!string.IsNullOrEmpty(data3))
                Beneficiary = Beneficiary + "<li>" + data3 + "</li>";

            if (!string.IsNullOrEmpty(data4))
                Beneficiary = Beneficiary + "<li>" + data4 + "</li>";

            if (!string.IsNullOrEmpty(data5))
                Beneficiary = Beneficiary + "<li>" + data5 + "</li>";

            Beneficiary = Beneficiary + "</div>";

            return Beneficiary;
        }

        #endregion

        #region  3. รักษาพยาบาล

        public BenefitMedicalViewModel GetMedicalTreatmentByUser(int userid)
        {
            var data = new BenefitMedicalViewModel();
            var list = new List<BenefitMedicalViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var medical = db.vw_Benefit_Medical.Where(x => x.UserID == userid).OrderByDescending(o => o.BmID).ToList();

                    foreach (var item in medical)
                    {
                        BenefitMedicalViewModel model = new BenefitMedicalViewModel();
                        model.RowNumber = index++;
                        model.BmID = item.BmID;
                        model.UserID = item.UserID;
                        model.BmYear = item.BmYear;
                        model.ClID = item.ClID;
                        model.ClName = item.ClName;
                        model.RecID = item.RecID;
                        model.RecFullName = item.RecFullName;
                        model.BmCardName = item.RecFullName;
                        model.BmCardID = item.BmCardID;
                        model.BmDate = item.BmDate;
                        model.BmAmoutService = item.BmAmoutService;
                        model.BmAmoutCare = item.BmAmoutCare;
                        model.BmRemark = item.BmRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BmDateText = (item.BmDate.HasValue) ? item.BmDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListMedical = list;
            return data;
        }

        public BenefitMedicalViewModel GetMedicalTreatmentByID(int userid, int id)
        {
            BenefitMedicalViewModel data = new BenefitMedicalViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Medical.Where(x => x.BmID == id).FirstOrDefault();
                    BenefitMedicalViewModel model = new BenefitMedicalViewModel();
                    model.BmID = item.BmID;
                    model.UserID = item.UserID;
                    model.BmYear = item.BmYear;
                    model.ClID = item.ClID;
                    model.RecID = item.RecID;
                    model.RecFullName = item.RecFullName;
                    model.BmCardID = item.BmCardID;
                    model.BmDate = item.BmDate;
                    model.BmAmoutService = item.BmAmoutService;
                    model.BmAmoutCare = item.BmAmoutCare;
                    model.BmRemark = item.BmRemark;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    model.BmDateText = (item.BmDate.HasValue) ? item.BmDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddMedicalTreatmentByEntity(BenefitMedicalViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Medical model = new tb_Benefit_Medical();
                    model.BmID = data.BmID;
                    model.UserID = data.UserID;
                    model.BmYear = data.BmYear;
                    model.ClID = data.ClID;
                    model.RecID = data.RecID;
                    model.RecFullName = data.RecFullName;
                    model.BmCardID = data.BmCardID;
                    model.BmDate = data.BmDate;
                    model.BmAmoutService = data.BmAmoutService;
                    model.BmAmoutCare = data.BmAmoutCare;
                    model.BmRemark = data.BmRemark;
                    if (Convert.ToDateTime(data.BmDate) > DateTime.MinValue)
                        model.BmDate = Convert.ToDateTime(data.BmDate);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Medical.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateMedicalTreatmentByEntity(BenefitMedicalViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Medical.Single(x => x.UserID == newdata.UserID && x.BmID == newdata.BmID);
                    model.BmID = newdata.BmID;
                    model.UserID = newdata.UserID;
                    model.BmYear = newdata.BmYear;
                    model.ClID = newdata.ClID;
                    model.RecID = newdata.RecID;
                    model.RecFullName = newdata.RecFullName;
                    model.BmCardID = newdata.BmCardID;
                    model.BmDate = newdata.BmDate;
                    model.BmAmoutService = newdata.BmAmoutService;
                    model.BmAmoutCare = newdata.BmAmoutCare;
                    model.BmRemark = newdata.BmRemark;
                    if (Convert.ToDateTime(newdata.BmDate) > DateTime.MinValue)
                        model.BmDate = Convert.ToDateTime(newdata.BmDate);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteMedicalTreatmentByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Medical.SingleOrDefault(x => x.BmID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Medical.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  4. เงินกู้

        public BenefitLoanViewModel GetLoanByUser(int userid)
        {
            var data = new BenefitLoanViewModel();
            var list = new List<BenefitLoanViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var loan = db.vw_Benefit_Loan.Where(x => x.UserID == userid).OrderByDescending(o => o.BlID).ToList();

                    foreach (var item in loan)
                    {
                        BenefitLoanViewModel model = new BenefitLoanViewModel();
                        model.RowNumber = index++;
                        model.BlID = item.BlID;
                        model.UserID = item.UserID;
                        model.BlYear = item.BlYear;
                        model.BID = item.BID;
                        model.LtID = item.LtID;
                        model.BlAccountNo = item.BlAccountNo;
                        model.BlStartDate = item.BlStartDate;
                        model.BlEndDate = item.BlEndDate;
                        model.BlCloseDate = item.BlCloseDate;
                        model.BlPeriod = item.BlPeriod;
                        model.BlPeriodPay = item.BlPeriodPay;
                        model.BISummaryAmout = item.BISummaryAmout;
                        model.BlRemark = item.BlRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BName = item.BName;
                        model.LtName = item.LtName;
                        model.CardID = item.CardID;
                        model.Outstanding = item.Outstanding;
                        model.BlStatus = item.BlStatus;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListLoan = list;
            return data;
        }

        public BenefitLoanViewModel GetLoanByID(int userid, int id)
        {
            BenefitLoanViewModel data = new BenefitLoanViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Loan.Where(x => x.BlID == id).FirstOrDefault();
                    BenefitLoanViewModel model = new BenefitLoanViewModel();
                    model.BlID = item.BlID;
                    model.UserID = item.UserID;
                    model.BlYear = item.BlYear;
                    model.BID = item.BID;
                    model.LtID = item.LtID;
                    model.BlAccountNo = item.BlAccountNo;
                    model.BlStartDate = item.BlStartDate;
                    model.BlEndDate = item.BlEndDate;
                    model.BlCloseDate = item.BlCloseDate;
                    model.BlPeriod = item.BlPeriod;
                    model.BlPeriodPay = item.BlPeriodPay;
                    model.BISummaryAmout = item.BISummaryAmout;
                    model.BlRemark = item.BlRemark;
                    model.CardID = item.CardID;
                    model.Outstanding = item.Outstanding;
                    model.BlStatus = item.BlStatus;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddLoanByEntity(BenefitLoanViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Loan model = new tb_Benefit_Loan();
                    model.BlID = data.BlID;
                    model.UserID = data.UserID;
                    model.BlYear = data.BlYear;
                    model.BID = data.BID;
                    model.LtID = data.LtID;
                    model.BlAccountNo = data.BlAccountNo;
                    model.BlPeriod = data.BlPeriod;
                    model.BlPeriodPay = data.BlPeriodPay;
                    model.BISummaryAmout = data.BISummaryAmout;
                    model.BlRemark = data.BlRemark;
                    if (data.BlStartDate.HasValue)
                        model.BlStartDate = data.BlStartDate;
                    if (data.BlEndDate.HasValue)
                        model.BlEndDate = data.BlEndDate;
                    if (data.BlCloseDate.HasValue)
                        model.BlCloseDate = data.BlCloseDate;
                    model.CardID = data.CardID;
                    model.Outstanding = data.Outstanding;
                    model.Outstanding = data.BlStatus;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Loan.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateLoanByEntity(BenefitLoanViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Loan.Single(x => x.UserID == newdata.UserID && x.BlID == newdata.BlID);
                    model.BlID = newdata.BlID;
                    model.UserID = newdata.UserID;
                    model.BlYear = newdata.BlYear;
                    model.BID = newdata.BID;
                    model.LtID = newdata.LtID;
                    model.BlAccountNo = newdata.BlAccountNo;
                    model.BlPeriod = newdata.BlPeriod;
                    model.BlPeriodPay = newdata.BlPeriodPay;
                    model.BISummaryAmout = newdata.BISummaryAmout;
                    model.BlRemark = newdata.BlRemark;
                    if (newdata.BlStartDate.HasValue)
                        model.BlStartDate = newdata.BlStartDate;
                    if (newdata.BlEndDate.HasValue)
                        model.BlEndDate = newdata.BlEndDate;
                    if (newdata.BlCloseDate.HasValue)
                        model.BlCloseDate = newdata.BlCloseDate;
                    model.CardID = newdata.CardID;
                    model.Outstanding = newdata.Outstanding;
                    model.BlStatus = newdata.BlStatus;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteLoanByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Loan.SingleOrDefault(x => x.BlID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Loan.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public List<SelectListItem> GetLoanStatus(int? defaultValue)
        {
            using (SATEntities db = new SATEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();

                var data = db.tb_Loan_Status.ToList();

                foreach (var item in data)
                {
                    SelectListItem select = new SelectListItem();
                    select.Value = item.StatusID.ToString();
                    select.Text = item.StatusName;
                    select.Selected = defaultValue.HasValue ? (item.StatusID == defaultValue ? true : false) : false;
                    list.Add(select);
                }
                return list;
            }
        }

        #endregion

        #region  5. ค่าเช่าบ้าน

        public BenefitHomeRentalViewModel GetHomeRentalByUser(int userid)
        {
            var data = new BenefitHomeRentalViewModel();
            var list = new List<BenefitHomeRentalViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var homeRental = db.tb_Benefit_Home_Rental.Where(x => x.UserID == userid).OrderByDescending(o => o.BhrID).ToList();

                    foreach (var item in homeRental)
                    {
                        BenefitHomeRentalViewModel model = new BenefitHomeRentalViewModel();
                        model.RowNumber = index++;
                        model.BhrID = item.BhrID;
                        model.UserID = item.UserID;
                        model.BhrYear = item.BhrYear;
                        model.RID = item.RID;
                        model.PID = item.PID;
                        model.BhrLevel = item.BhrLevel;
                        model.BhrStep = item.BhrStep;
                        model.BhrStartDate = item.BhrStartDate;
                        model.BhrEndDate = item.BhrEndDate;
                        model.BhrAmout = item.BhrAmout;
                        model.BhrRemark = item.BhrRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BhrStartDateText = (item.BhrStartDate.HasValue) ? item.BhrStartDate.Value.ToString("dd/MM/yyyy") : string.Empty;//(item.BhrStartDate.HasValue) ? UtilityService.ConvertDateThai(item.BhrStartDate.Value).ToString("dd/MM/yyyy") : string.Empty;
                        model.BhrEndDateText = (item.BhrEndDate.HasValue) ? item.BhrEndDate.Value.ToString("dd/MM/yyyy") : string.Empty; //(item.BhrEndDate.HasValue) ? UtilityService.ConvertDateThai(item.BhrEndDate.Value).ToString("dd/MM/yyyy") : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListHomeRental = list;
            return data;
        }

        public BenefitHomeRentalViewModel GetHomeRentalByID(int userid, int id)
        {
            BenefitHomeRentalViewModel data = new BenefitHomeRentalViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Home_Rental.Where(x => x.BhrID == id).FirstOrDefault();
                    BenefitHomeRentalViewModel model = new BenefitHomeRentalViewModel();
                    model.BhrID = item.BhrID;
                    model.UserID = item.UserID;
                    model.BhrYear = item.BhrYear;
                    model.RID = item.RID;
                    model.PID = item.PID;
                    model.BhrLevel = item.BhrLevel;
                    model.BhrStep = item.BhrStep;
                    model.BhrStartDate = item.BhrStartDate;
                    model.BhrEndDate = item.BhrEndDate;
                    model.BhrAmout = item.BhrAmout;
                    model.BhrRemark = item.BhrRemark;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    model.RName = "";
                    model.PName = "";
                    model.BhrStartDateText = (item.BhrStartDate.HasValue) ? item.BhrStartDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                    model.BhrEndDateText = (item.BhrEndDate.HasValue) ? item.BhrEndDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddHomeRentalByEntity(BenefitHomeRentalViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Home_Rental model = new tb_Benefit_Home_Rental();
                    model.BhrID = data.BhrID;
                    model.UserID = data.UserID;
                    model.BhrYear = data.BhrYear;
                    model.RID = data.RID;
                    model.PID = data.PID;
                    model.BhrLevel = data.BhrLevel;
                    model.BhrStep = data.BhrStep;
                    model.BhrAmout = data.BhrAmout;
                    model.BhrRemark = data.BhrRemark;
                    if (Convert.ToDateTime(data.BhrStartDate) > DateTime.MinValue)
                        model.BhrStartDate = Convert.ToDateTime(data.BhrStartDate);
                    if (Convert.ToDateTime(data.BhrEndDate) > DateTime.MinValue)
                        model.BhrEndDate = Convert.ToDateTime(data.BhrEndDate);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Home_Rental.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateHomeRentalByEntity(BenefitHomeRentalViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Home_Rental.Single(x => x.UserID == newdata.UserID && x.BhrID == newdata.BhrID);
                    model.BhrID = newdata.BhrID;
                    model.UserID = newdata.UserID;
                    model.BhrYear = newdata.BhrYear;
                    model.RID = newdata.RID;
                    model.PID = newdata.PID;
                    model.BhrLevel = newdata.BhrLevel;
                    model.BhrStep = newdata.BhrStep;
                    model.BhrAmout = newdata.BhrAmout;
                    model.BhrRemark = newdata.BhrRemark;
                    if (Convert.ToDateTime(newdata.BhrStartDate) > DateTime.MinValue)
                        model.BhrStartDate = Convert.ToDateTime(newdata.BhrStartDate);
                    if (Convert.ToDateTime(newdata.BhrEndDate) > DateTime.MinValue)
                        model.BhrEndDate = Convert.ToDateTime(newdata.BhrEndDate);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteHomeRentalByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Home_Rental.SingleOrDefault(x => x.BhrID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Home_Rental.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  6. เงินช่วยเหลือบุตร

        public BenefitChildFundViewModel GetChildFundByUser(int userid)
        {
            var data = new BenefitChildFundViewModel();
            var list = new List<BenefitChildFundViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var childFund = db.tb_User_Family.Where(x => x.UserID == userid && x.RecID == 5).OrderByDescending(o => o.UfName).ToList();
                    if (childFund.Count > 0)
                    {
                        foreach (var item in childFund)
                        {
                            BenefitChildFundViewModel model = new BenefitChildFundViewModel();
                            model.RowNumber = index++;
                            model.BcfID = item.UfID;
                            model.UserID = item.UserID;
                            model.BcfName = item.UfName;
                            model.BcfIDCard = item.UfCardID;
                            model.BcfBirthDate = item.UfDOB;
                            model.BcfExpireDate = item.UfDOB ?? item.UfDOB.Value.AddYears(18);
                            model.BcfAmout = item.ChildFundAmout;
                            model.BcfBirthDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                            model.BcfExpireDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.AddYears(18).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                            model.TypeFund = item.TypeFund;
                            model.PayRateFund = item.PayRateFund;
                            model.InvNoFund = item.InvNoFund;
                            model.DateFund = item.DateFund;
                            model.SchoolYear = item.SchoolYear;
                            list.Add(model);
                        }
                    }
                }
                data.UserID = userid;
                data.ListChildFund = list;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BenefitChildFundViewModel GetChildFundByID(int userid, int id)
        {
            BenefitChildFundViewModel data = new BenefitChildFundViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var item = db.tb_User_Family.Where(x => x.UfID == id).FirstOrDefault();
                    if (item != null)
                    {
                        BenefitChildFundViewModel model = new BenefitChildFundViewModel();
                        model.RowNumber = index++;
                        model.BcfID = item.UfID;
                        model.UserID = item.UserID;
                        model.BcfName = item.UfName;
                        model.BcfIDCard = item.UfCardID;
                        model.BcfBirthDate = item.UfDOB;
                        model.BcfExpireDate = item.UfDOB.Value.AddYears(18);
                        model.BcfAmout = item.ChildFundAmout;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BcfBirthDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                        model.BcfExpireDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.AddYears(18).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                        model.TypeFund = item.TypeFund;
                        model.PayRateFund = item.PayRateFund;
                        model.InvNoFund = item.InvNoFund;
                        model.DateFund = item.DateFund;
                        model.SchoolYear = item.SchoolYear;
                        data = model;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        public ResponseData UpdateChildFundByEntity(BenefitChildFundViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Family.Single(x => x.UserID == newdata.UserID && x.UfID == newdata.BcfID);
                    model.ChildFundAmout = newdata.BcfAmout;
                    model.TypeFund = newdata.TypeFund;
                    model.PayRateFund = newdata.PayRateFund;
                    model.InvNoFund = newdata.InvNoFund;
                    model.DateFund = newdata.DateFund;
                    model.SchoolYear = newdata.SchoolYear;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  7. เงินช่วยเหลือการศึกษาบุตร

        public BenefitChildEducationViewModel GetChildEducationByUser(int userid)
        {
            var data = new BenefitChildEducationViewModel();
            var list = new List<BenefitChildEducationViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var childEducation = db.tb_User_Family.Where(x => x.UserID == userid && x.RecID == 5).OrderByDescending(o => o.UfName).ToList();
                    if (childEducation.Count > 0)
                    {
                        foreach (var item in childEducation)
                        {
                            BenefitChildEducationViewModel model = new BenefitChildEducationViewModel();
                            model.RowNumber = index++;
                            model.BceID = item.UfID;
                            model.UserID = item.UserID;
                            model.BceName = item.UfName;
                            model.BceIDCard = item.UfCardID;
                            model.BcdBirthDate = item.UfDOB;
                            model.BcdExpireDate = item.UfDOB ?? item.UfDOB.Value.AddYears(18);
                            model.BcdAmout = item.ChildEducationAmout;
                            model.CreateDate = item.CreateDate;
                            model.CreateBy = item.CreateBy;
                            model.ModifyDate = item.ModifyDate;
                            model.ModifyBy = item.ModifyBy;
                            model.BcdBirthDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                            model.BcdExpireDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.AddYears(18).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                            model.PayRateEdu = item.PayRateEdu;
                            model.TimeRequestEdu = item.TimeRequestEdu;
                            list.Add(model);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListChildEducation = list;
            return data;
        }

        public BenefitChildEducationViewModel GetChildEducationByID(int userid, int id)
        {
            BenefitChildEducationViewModel data = new BenefitChildEducationViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_User_Family.Where(x => x.UfID == id).FirstOrDefault();
                    if (item != null)
                    {
                        BenefitChildEducationViewModel model = new BenefitChildEducationViewModel();
                        model.BceID = item.UfID;
                        model.UserID = item.UserID;
                        model.BceName = item.UfName;
                        model.BceIDCard = item.UfCardID;
                        model.BcdBirthDate = item.UfDOB;
                        model.BcdExpireDate = item.UfDOB.Value.AddYears(18);
                        model.BcdAmout = item.ChildEducationAmout;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BcdBirthDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.BcdExpireDateText = (item.UfDOB.HasValue) ? item.UfDOB.Value.AddYears(18).ToString("dd/MM/yyyy") : string.Empty;
                        model.PayRateEdu = item.PayRateEdu;
                        model.TimeRequestEdu = item.TimeRequestEdu;
                        data = model;
                    }
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData UpdateChildEducationByEntity(BenefitChildEducationViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Family.Single(x => x.UserID == newdata.UserID && x.UfID == newdata.BceID);
                    model.ChildEducationAmout = newdata.BcdAmout;
                    model.PayRateEdu = newdata.PayRateEdu;
                    model.TimeRequestEdu = newdata.TimeRequestEdu;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }


        #endregion

        #region  8. ฌาปนกิจสงเคราะห์

        public BenefitCremationViewModel GetCremationByUser(int userid)
        {
            var data = new BenefitCremationViewModel();
            var list = new List<BenefitCremationViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var cremation = db.vw_Benefit_Cremation.Where(x => x.UserID == userid).OrderByDescending(o => o.BcID).ToList();

                    foreach (var item in cremation)
                    {
                        BenefitCremationViewModel model = new BenefitCremationViewModel();
                        model.RowNumber = index++;
                        model.BcID = item.BcID;
                        model.UserID = item.UserID;
                        model.BcYear = item.BcYear;
                        model.MID = item.MID;
                        model.MName = item.MName;
                        model.BcMemberNo = item.BcMemberNo;
                        model.BcBeneficiary1 = item.BcBeneficiary1;
                        model.BcBeneficiary2 = item.BcBeneficiary2;
                        model.BcBeneficiary3 = item.BcBeneficiary3;
                        model.BcBeneficiary4 = item.BcBeneficiary4;
                        model.BcBeneficiary5 = item.BcBeneficiary5;
                        model.BcRecFullName = item.BcRecFullName;
                        model.BcBeneficiary = GetAllBpBeneficiary(item.BcBeneficiary1, item.BcBeneficiary2, item.BcBeneficiary3, item.BcBeneficiary4, item.BcBeneficiary5);
                        model.BcDate = item.BcDate;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BcDateText = (item.BcDate.HasValue) ? item.BcDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListCremation = list;
            return data;
        }

        public BenefitCremationViewModel GetCremationByID(int userid, int id)
        {
            BenefitCremationViewModel data = new BenefitCremationViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Cremation.Where(x => x.BcID == id).FirstOrDefault();
                    BenefitCremationViewModel model = new BenefitCremationViewModel();
                    model.BcID = item.BcID;
                    model.UserID = item.UserID;
                    model.BcYear = item.BcYear;
                    model.MID = item.MID;
                    model.BcMemberNo = item.BcMemberNo;
                    model.BcBeneficiary1 = item.BcBeneficiary1;
                    model.BcBeneficiary2 = item.BcBeneficiary2;
                    model.BcBeneficiary3 = item.BcBeneficiary3;
                    model.BcBeneficiary4 = item.BcBeneficiary4;
                    model.BcBeneficiary5 = item.BcBeneficiary5;
                    model.BcRecFullName = item.BcRecFullName;
                    model.BcDate = item.BcDate;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    model.BcDateText = (item.BcDate.HasValue) ? item.BcDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddCremationByEntity(BenefitCremationViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Cremation model = new tb_Benefit_Cremation();
                    model.BcID = data.BcID;
                    model.UserID = data.UserID;
                    model.BcYear = data.BcYear;
                    model.MID = data.MID;
                    model.BcMemberNo = data.BcMemberNo;
                    model.BcBeneficiary1 = data.BcBeneficiary1;
                    model.BcBeneficiary2 = data.BcBeneficiary2;
                    model.BcBeneficiary3 = data.BcBeneficiary3;
                    model.BcBeneficiary4 = data.BcBeneficiary4;
                    model.BcBeneficiary5 = data.BcBeneficiary5;
                    model.BcRecFullName = data.BcRecFullName;
                    if (Convert.ToDateTime(data.BcDate) > DateTime.MinValue)
                        model.BcDate = Convert.ToDateTime(data.BcDate);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Cremation.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateCremationByEntity(BenefitCremationViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Cremation.Single(x => x.UserID == newdata.UserID && x.BcID == newdata.BcID);
                    model.BcID = newdata.BcID;
                    model.UserID = newdata.UserID;
                    model.BcYear = newdata.BcYear;
                    model.MID = newdata.MID;
                    model.BcMemberNo = newdata.BcMemberNo;
                    model.BcBeneficiary1 = newdata.BcBeneficiary1;
                    model.BcBeneficiary2 = newdata.BcBeneficiary2;
                    model.BcBeneficiary3 = newdata.BcBeneficiary3;
                    model.BcBeneficiary4 = newdata.BcBeneficiary4;
                    model.BcBeneficiary5 = newdata.BcBeneficiary5;
                    model.BcRecFullName = newdata.BcRecFullName;
                    if (Convert.ToDateTime(newdata.BcDate) > DateTime.MinValue)
                        model.BcDate = Convert.ToDateTime(newdata.BcDate);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteCremationByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Cremation.SingleOrDefault(x => x.BcID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Cremation.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  9. เงินทดแทนกรณีเสียชีวิต

        public BenefitDeathReplacementViewModel GetDeathReplacementByUser(int userid)
        {
            var data = new BenefitDeathReplacementViewModel();
            var list = new List<BenefitDeathReplacementViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var replacement = db.vw_Benefit_Death_Replacement.Where(x => x.UserID == userid).OrderByDescending(o => o.BdID).ToList();

                    foreach (var item in replacement)
                    {
                        BenefitDeathReplacementViewModel model = new BenefitDeathReplacementViewModel();
                        model.RowNumber = index++;
                        model.BdID = item.BdID;
                        model.UserID = item.UserID;
                        model.RecID = item.RecID;
                        model.RecName = item.RecName;
                        model.BdYear = item.BdYear;
                        model.BdFullName = item.BdFullName;
                        model.BdTime = item.BdTime;
                        model.BdPer = item.BdPer;
                        model.BdAmout = item.BdAmout;
                        model.BdDate = item.BdDate;
                        model.BdRemark = item.BdRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BdDateText = (item.BdDate.HasValue) ? item.BdDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListDeathReplacement = list;
            return data;
        }

        public BenefitDeathReplacementViewModel GetDeathReplacementByID(int userid, int id)
        {
            BenefitDeathReplacementViewModel data = new BenefitDeathReplacementViewModel();
            data.UserID = userid;
            data.BdAmout = CalculateBenefitDeath(userid);
            data.BdPer = 3;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Death_Replacement.Where(x => x.BdID == id).FirstOrDefault();
                    BenefitDeathReplacementViewModel model = new BenefitDeathReplacementViewModel();
                    model.BdID = item.BdID;
                    model.UserID = item.UserID;
                    model.RecID = item.RecID;
                    model.BdYear = item.BdYear;
                    model.BdFullName = item.BdFullName;
                    model.BdTime = item.BdTime;
                    model.BdPer = item.BdPer;
                    model.BdAmout = item.BdAmout;
                    model.BdDate = item.BdDate;
                    model.BdRemark = item.BdRemark;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    model.BdDateText = (item.BdDate.HasValue) ? item.BdDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddDeathReplacementByEntity(BenefitDeathReplacementViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Death_Replacement model = new tb_Benefit_Death_Replacement();
                    model.BdID = data.BdID;
                    model.UserID = data.UserID;
                    model.BdYear = data.BdYear;
                    model.RecID = data.RecID;
                    model.BdFullName = data.BdFullName;
                    model.BdTime = data.BdTime;
                    model.BdPer = data.BdPer;
                    model.BdAmout = data.BdAmout;
                    model.BdDate = data.BdDate;
                    model.BdRemark = data.BdRemark;
                    if (Convert.ToDateTime(data.BdDate) > DateTime.MinValue)
                        model.BdDate = Convert.ToDateTime(data.BdDate);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Death_Replacement.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateDeathReplacementByEntity(BenefitDeathReplacementViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Death_Replacement.Single(x => x.UserID == newdata.UserID && x.BdID == newdata.BdID);
                    model.BdID = newdata.BdID;
                    model.UserID = newdata.UserID;
                    model.BdYear = newdata.BdYear;
                    model.RecID = newdata.RecID;
                    model.BdFullName = newdata.BdFullName;
                    model.BdTime = newdata.BdTime;
                    model.BdPer = newdata.BdPer;
                    model.BdAmout = newdata.BdAmout;
                    model.BdDate = newdata.BdDate;
                    model.BdRemark = newdata.BdRemark;
                    if (Convert.ToDateTime(newdata.BdDate) > DateTime.MinValue)
                        model.BdDate = Convert.ToDateTime(newdata.BdDate);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteDeathReplacementByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Death_Replacement.SingleOrDefault(x => x.BdID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Death_Replacement.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  10.เงินช่วยเหลือพิเศษกรณีเสียชีวิต

        public BenefitDeathSubsidyViewModel GetDeathSubsidyByUser(int userid)
        {
            var data = new BenefitDeathSubsidyViewModel();
            var list = new List<BenefitDeathSubsidyViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var deathSubsidy = db.vw_Benefit_Death_Subsidy.Where(x => x.UserID == userid).OrderByDescending(o => o.BdID).ToList();

                    foreach (var item in deathSubsidy)
                    {
                        BenefitDeathSubsidyViewModel model = new BenefitDeathSubsidyViewModel();
                        model.RowNumber = index++;
                        model.BdID = item.BdID;
                        model.UserID = item.UserID;
                        model.RecID = item.RecID;
                        model.RecName = item.RecName;
                        model.BdYear = item.BdYear;
                        model.BdFullName = item.BdFullName;
                        model.BdTime = item.BdTime;
                        model.BdPer = item.BdPer;
                        model.BdAmout = item.BdAmout;
                        model.BdDate = item.BdDate;
                        model.BdRemark = item.BdRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BdDateText = (item.BdDate.HasValue) ? item.BdDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListDeathSubsidy = list;
            return data;
        }

        public BenefitDeathSubsidyViewModel GetDeathSubsidyByID(int userid, int id)
        {
            BenefitDeathSubsidyViewModel data = new BenefitDeathSubsidyViewModel();
            data.UserID = userid;
            data.BdAmout = CalculateBenefitDeath(userid);
            data.BdPer = 3;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Death_Subsidy.Where(x => x.BdID == id).FirstOrDefault();
                    BenefitDeathSubsidyViewModel model = new BenefitDeathSubsidyViewModel();
                    model.BdID = item.BdID;
                    model.UserID = item.UserID;
                    model.BdYear = item.BdYear;
                    model.RecID = item.RecID;
                    model.BdFullName = item.BdFullName;
                    model.BdTime = item.BdTime;
                    model.BdPer = item.BdPer;
                    model.BdAmout = item.BdAmout;
                    model.BdDate = item.BdDate;
                    model.BdRemark = item.BdRemark;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    model.BdDateText = (item.BdDate.HasValue) ? item.BdDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddDeathSubsidyByEntity(BenefitDeathSubsidyViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Death_Subsidy model = new tb_Benefit_Death_Subsidy();
                    model.BdID = data.BdID;
                    model.UserID = data.UserID;
                    model.BdYear = data.BdYear;
                    model.RecID = data.RecID;
                    model.BdFullName = data.BdFullName;
                    model.BdTime = data.BdTime;
                    model.BdPer = data.BdPer;
                    model.BdAmout = data.BdAmout;
                    model.BdDate = data.BdDate;
                    model.BdRemark = data.BdRemark;
                    if (Convert.ToDateTime(data.BdDateText) > DateTime.MinValue)
                        model.BdDate = Convert.ToDateTime(data.BdDate);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Death_Subsidy.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateDeathSubsidyByEntity(BenefitDeathSubsidyViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Death_Subsidy.Single(x => x.UserID == newdata.UserID && x.BdID == newdata.BdID);
                    model.BdID = newdata.BdID;
                    model.UserID = newdata.UserID;
                    model.BdYear = newdata.BdYear;
                    model.RecID = newdata.RecID;
                    model.BdFullName = newdata.BdFullName;
                    model.BdTime = newdata.BdTime;
                    model.BdPer = newdata.BdPer;
                    model.BdAmout = newdata.BdAmout;
                    model.BdDate = newdata.BdDate;
                    model.BdRemark = newdata.BdRemark;
                    if (Convert.ToDateTime(newdata.BdDateText) > DateTime.MinValue)
                        model.BdDate = Convert.ToDateTime(newdata.BdDate);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteDeathSubsidyByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Death_Subsidy.SingleOrDefault(x => x.BdID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Death_Subsidy.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region 11.สวัสดิการอื่นๆ

        public BenefitOtherWelfareViewModel GetOtherWelfareByUser(int userid, int? year, int? type)
        {
            var data = new BenefitOtherWelfareViewModel();
            var list = new List<BenefitOtherWelfareViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var otherWelfare = db.vw_Benefit_Other_Welfare.Where(x => x.UserID == userid).OrderByDescending(o => o.BoID).ToList();

                    if(year.HasValue)
                        otherWelfare = otherWelfare.Where(x => x.BoYear == year).ToList();

                    if (type.HasValue)
                        otherWelfare = otherWelfare.Where(x => x.BenTID == type).ToList();

                    foreach (var item in otherWelfare)
                    {
                        BenefitOtherWelfareViewModel model = new BenefitOtherWelfareViewModel();
                        model.RowNumber = index++;
                        model.BoID = item.BoID;
                        model.UserID = item.UserID;
                        model.BoYear = item.BoYear;
                        model.BenTID = item.BenTID;
                        model.BoRecID = item.BoRecID;
                        model.BoRecFullName = item.BoRecFullName;
                        model.BoOptRecID = item.BoOptRecID;
                        model.BoOptFullName = item.BoOptFullName;
                        model.BoTime = item.BoTime;
                        model.BoPer = item.BoPer;
                        model.BoAmout = item.BoAmout;
                        model.BoDate = item.BoDate;
                        model.BoRemark = item.BoRemark;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.BoDateText = (item.BoDate.HasValue) ? item.BoDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : string.Empty;
                        model.BenTName = item.BenTName;
                        model.BoRecName = item.BoRecName;
                        model.BoOptRecName = item.BoOptRecName;
                        model.BoYear = item.BoYear;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListOtherWelfare = list;
            return data;
        }

        public BenefitOtherWelfareViewModel GetOtherWelfareByID(int userid, int id)
        {
            BenefitOtherWelfareViewModel data = new BenefitOtherWelfareViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Benefit_Other_Welfare.Where(x => x.BoID == id).FirstOrDefault();
                    BenefitOtherWelfareViewModel model = new BenefitOtherWelfareViewModel();
                    model.BoID = item.BoID;
                    model.UserID = item.UserID;
                    model.BoYear = item.BoYear;
                    model.BenTID = item.BenTID;
                    model.BoRecID = item.BoRecID;
                    model.BoRecFullName = item.BoRecFullName;
                    model.BoOptRecID = item.BoOptRecID;
                    model.BoOptFullName = item.BoOptFullName;
                    model.BoTime = item.BoTime;
                    model.BoPer = item.BoPer;
                    model.BoAmout = item.BoAmout;
                    model.BoDate = item.BoDate;
                    model.BoRemark = item.BoRemark;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    model.BoDateText = (item.BoDate.HasValue) ? item.BoDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                    model.BenTName = "";
                    model.BoRecName = "";
                    model.BoOptRecName = "";

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddOtherWelfareByEntity(BenefitOtherWelfareViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Other_Welfare model = new tb_Benefit_Other_Welfare();
                    model.BoID = data.BoID;
                    model.UserID = data.UserID;
                    model.BoYear = data.BoYear;
                    model.BenTID = data.BenTID;
                    model.BoRecID = data.BoRecID;
                    model.BoRecFullName = data.BoRecFullName;
                    model.BoOptRecID = data.BoOptRecID;
                    model.BoOptFullName = data.BoOptFullName;
                    model.BoTime = data.BoTime;
                    model.BoPer = data.BoPer;
                    model.BoAmout = data.BoAmout;
                    model.BoDate = data.BoDate;
                    model.BoRemark = data.BoRemark;
                    if (Convert.ToDateTime(data.BoDate) > DateTime.MinValue)
                        model.BoDate = Convert.ToDateTime(data.BoDate);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Other_Welfare.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateOtherWelfareByEntity(BenefitOtherWelfareViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Other_Welfare.Single(x => x.UserID == newdata.UserID && x.BoID == newdata.BoID);
                    model.BoID = newdata.BoID;
                    model.UserID = newdata.UserID;
                    model.BoYear = newdata.BoYear;
                    model.BenTID = newdata.BenTID;
                    model.BoRecID = newdata.BoRecID;
                    model.BoRecFullName = newdata.BoRecFullName;
                    model.BoOptRecID = newdata.BoOptRecID;
                    model.BoOptFullName = newdata.BoOptFullName;
                    model.BoTime = newdata.BoTime;
                    model.BoPer = newdata.BoPer;
                    model.BoAmout = newdata.BoAmout;
                    model.BoDate = newdata.BoDate;
                    model.BoRemark = newdata.BoRemark;
                    if (Convert.ToDateTime(newdata.BoDate) > DateTime.MinValue)
                        model.BoDate = Convert.ToDateTime(newdata.BoDate);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteOtherWelfareByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Benefit_Other_Welfare.SingleOrDefault(x => x.BoID == id);
                    if (model != null)
                    {
                        db.tb_Benefit_Other_Welfare.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public List<YearOtherWelfareViewModel> GetYearOtherWelfare()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = new List<YearOtherWelfareViewModel>();
                lists = db.tb_Benefit_Other_Welfare.GroupBy(g => g.BoYear).Select(group => new { BoYear = group.Key })
                            .Select(s => new YearOtherWelfareViewModel()
                            {
                                Year = (int)s.BoYear
                            })
                            .OrderByDescending(x => x.Year).ToList();
                return lists;
            }
        }


        #endregion

        public decimal CalculateBenefitDeath(int userID)
        {
            decimal Amount = 0;
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var salary = db.tb_User.AsEnumerable().Where(x => x.UserID == userID).FirstOrDefault().Salary;
                    Amount = Convert.ToDecimal(salary) * 3;
                }
                catch (Exception ex)
                {
                    throw ex;
                }                    
            }

            return Amount;
        }
    }
}