using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Data.Repository
{
    public class ImportRepository
    {
        public List<SelectListItem> Subject()
        {
            using (SATEntities db = new SATEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();

                var data = db.tb_Import_Master.Where(m => m.ParentID == null).ToList();
                foreach (var item in data)
                {
                    SelectListItem select = new SelectListItem();
                    select.Value = item.ID.ToString();
                    select.Text = item.Name;
                    list.Add(select);
                }
                return list;
            }
        }
        public List<SelectListItem> SubSubject(int? parentid)
        {
            using (SATEntities db = new SATEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();

                var data = db.tb_Import_Master.Where(m => m.ParentID == parentid).ToList();
                foreach (var item in data)
                {
                    SelectListItem select = new SelectListItem();
                    select.Value = item.ID.ToString();
                    select.Text = item.Name;
                    list.Add(select);
                }
                return list;
            }
        }
        public void Add_Benefit_Loan(List<tb_Benefit_Loan> data)
        {
            List<tb_Benefit_Loan> _list = new List<tb_Benefit_Loan>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Loan item in data)
                {
                    tb_Benefit_Loan l = new tb_Benefit_Loan();

                    l.UserID = item.UserID;
                    l.BlYear = item.BlYear;
                    l.BID = item.BID;
                    l.LtID = item.LtID;
                    l.BlAccountNo = item.BlAccountNo;
                    l.BlStartDate = item.BlStartDate;
                    l.BlEndDate = item.BlEndDate;
                    l.BlCloseDate = item.BlCloseDate;
                    l.BlPeriod = item.BlPeriod;
                    l.BlPeriodPay = item.BlPeriodPay;
                    l.BISummaryAmout = item.BISummaryAmout;
                    l.BlRemark = item.BlRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Loan.Add(l);
                    db.SaveChanges();

                }
            }            
        }
        public void Update_User_Family(List<tb_User_Family> data, int mapColumn)
        {
            //List<tb_User_Family> _listLoan = new List<tb_User_Family>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_User_Family item in data)
                {
                    tb_User_Family l = db.tb_User_Family.Where(x => x.UserID == item.UserID && x.UfCardID == item.UfCardID).FirstOrDefault();

                    if (mapColumn.Equals(14))
                    {
                        l.ChildFundAmout = item.ChildFundAmout;
                    }

                    if (mapColumn.Equals(21))
                    {
                        l.ChildEducationAmout = item.ChildEducationAmout;
                    }

                    db.SaveChanges();

                }
            }
        }
        public void Add_Provident_Fund(List<tb_Benefit_Provident_Fund> data)
        {
            List<tb_Benefit_Provident_Fund> _list = new List<tb_Benefit_Provident_Fund>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Provident_Fund item in data)
                {
                    tb_Benefit_Provident_Fund l = new tb_Benefit_Provident_Fund();

                    l.UserID = item.UserID;
                    l.BpYear = item.BpYear;
                    l.BpDateChangeFund = item.BpDateChangeFund;
                    l.BpAccumFundCuID = item.BpAccumFundCuID;
                    l.BpAssoFundCuID = item.BpAssoFundCuID;
                    l.BpBeneficiary1 = item.BpBeneficiary1;
                    l.BpBeneficiary2 = item.BpBeneficiary2;
                    l.BpBeneficiary3 = item.BpBeneficiary3;
                    l.BpBeneficiary4 = item.BpBeneficiary4;
                    l.BpBeneficiary5 = item.BpBeneficiary5;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;
                    l.BpBeneficiary1Percent = item.BpBeneficiary1Percent;
                    l.BpBeneficiary2Percent = item.BpBeneficiary2Percent;
                    l.BpBeneficiary3Percent = item.BpBeneficiary3Percent;
                    l.BpBeneficiary4Percent = item.BpBeneficiary4Percent;
                    l.BpBeneficiary5Percent = item.BpBeneficiary5Percent;

                    _list.Add(l);
                    db.tb_Benefit_Provident_Fund.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Home_Rental(List<tb_Benefit_Home_Rental> data)
        {
            List<tb_Benefit_Home_Rental> _list = new List<tb_Benefit_Home_Rental>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Home_Rental item in data)
                {
                    tb_Benefit_Home_Rental l = new tb_Benefit_Home_Rental();

                    l.UserID = item.UserID;
                    l.BhrYear = item.BhrYear;
                    l.RID = item.RID;
                    l.PID = item.PID;
                    l.BhrLevel = item.BhrLevel;
                    l.BhrStep = item.BhrStep;
                    l.BhrStartDate = item.BhrStartDate;
                    l.BhrEndDate = item.BhrEndDate;
                    l.BhrAmout = item.BhrAmout;
                    l.BhrRemark = item.BhrRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Home_Rental.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Medical(List<tb_Benefit_Medical> data)
        {
            List<tb_Benefit_Medical> _list = new List<tb_Benefit_Medical>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Medical item in data)
                {
                    tb_Benefit_Medical l = new tb_Benefit_Medical();

                    l.UserID = item.UserID;
                    l.BmYear = item.BmYear;
                    l.ClID = item.ClID;
                    l.RecID = item.RecID;
                    l.RecFullName = item.RecFullName;
                    l.BmCardID = item.BmCardID;
                    l.BmDate = item.BmDate;
                    l.BmAmoutService = item.BmAmoutService;
                    l.BmAmoutCare = item.BmAmoutCare;
                    l.BmRemark = item.BmRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Medical.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Cremation(List<tb_Benefit_Cremation> data)
        {
            List<tb_Benefit_Cremation> _list = new List<tb_Benefit_Cremation>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Cremation item in data)
                {
                    tb_Benefit_Cremation l = new tb_Benefit_Cremation();

                    l.UserID = item.UserID;
                    l.BcYear = item.BcYear;
                    l.MID = item.MID;
                    l.BcMemberNo = l.BcMemberNo;
                    l.BcDate = item.BcDate;
                    l.BcBeneficiary1 = item.BcBeneficiary1;
                    l.BcBeneficiary2 = item.BcBeneficiary2;
                    l.BcBeneficiary3 = item.BcBeneficiary3;
                    l.BcBeneficiary4 = item.BcBeneficiary4;
                    l.BcBeneficiary5 = item.BcBeneficiary5;
                    l.BcRecFullName = item.BcRecFullName;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Cremation.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Death_Subsidy(List<tb_Benefit_Death_Subsidy> data)
        {
            List<tb_Benefit_Death_Subsidy> _list = new List<tb_Benefit_Death_Subsidy>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Death_Subsidy item in data)
                {
                    tb_Benefit_Death_Subsidy l = new tb_Benefit_Death_Subsidy();

                    l.UserID = item.UserID;
                    l.BdYear = item.BdYear;
                    l.RecID = item.RecID;
                    l.BdFullName = item.BdFullName;
                    l.BdTime = item.BdTime;
                    l.BdPer = item.BdPer;
                    l.BdAmout = item.BdAmout;
                    l.BdDate = item.BdDate;
                    l.BdRemark = item.BdRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Death_Subsidy.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Death_Replacement(List<tb_Benefit_Death_Replacement> data)
        {
            List<tb_Benefit_Death_Replacement> _list = new List<tb_Benefit_Death_Replacement>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Death_Replacement item in data)
                {
                    tb_Benefit_Death_Replacement l = new tb_Benefit_Death_Replacement();

                    l.UserID = item.UserID;
                    l.BdYear = item.BdYear;
                    l.RecID = item.RecID;
                    l.BdFullName = item.BdFullName;
                    l.BdTime = item.BdTime;
                    l.BdPer = item.BdPer;
                    l.BdAmout = item.BdAmout;
                    l.BdDate = item.BdDate;
                    l.BdRemark = item.BdRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Death_Replacement.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Remuneration(List<tb_Benefit_Remuneration> data)
        {
            List<tb_Benefit_Remuneration> _list = new List<tb_Benefit_Remuneration>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Remuneration item in data)
                {
                    tb_Benefit_Remuneration l = new tb_Benefit_Remuneration();

                    l.UserID = item.UserID;
                    l.BrYear = item.BrYear;
                    l.RecID = item.RecID;
                    l.RecFullName = item.RecFullName;
                    l.BrAmout = item.BrAmout;
                    l.BrDate = item.BrDate;
                    l.BrRemark = item.BrRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Remuneration.Add(l);
                    db.SaveChanges();

                }
            }
        }
        public void Add_Other_Welfare(List<tb_Benefit_Other_Welfare> data)
        {
            List<tb_Benefit_Other_Welfare> _list = new List<tb_Benefit_Other_Welfare>();
            using (SATEntities db = new SATEntities())
            {
                foreach (tb_Benefit_Other_Welfare item in data)
                {
                    tb_Benefit_Other_Welfare l = new tb_Benefit_Other_Welfare();

                    l.UserID = item.UserID;
                    l.BoYear = item.BoYear;
                    l.BenTID = item.BenTID;
                    l.BoRecID = item.BoRecID;
                    l.BoRecFullName = item.BoRecFullName;
                    l.BoOptRecID = item.BoOptRecID;
                    l.BoOptFullName = item.BoOptFullName;
                    l.BoTime = item.BoTime;
                    l.BoPer = item.BoPer;
                    l.BoAmout = item.BoAmout;
                    l.BoDate = item.BoDate;
                    l.BoRemark = item.BoRemark;
                    l.CreateBy = UtilityService.User.UserID;
                    l.CreateDate = DateTime.Now;
                    l.ModifyBy = UtilityService.User.UserID;
                    l.ModifyDate = DateTime.Now;

                    _list.Add(l);
                    db.tb_Benefit_Other_Welfare.Add(l);
                    db.SaveChanges();

                }
            }
        }


    }
}