﻿using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class MasterRepository
    {
        public List<WorkingTypeViewModel> GetWorkingType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Working_Type.Select(s => new WorkingTypeViewModel()
                {
                    WorkingTypeID = s.WorkingTypeID,
                    WorkingTypeName = s.WorkingTypeName
                }).ToList();
                return list;
            }
        }

        public List<UserStatusViewModel> GetUserStatus()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_User_Status.Select(s => new UserStatusViewModel()
                {
                    UserStatusID = s.StatusID,
                    UserStatusName = s.StatusName
                }).ToList();
                return list;
            }
        }

        public List<UserTypeViewModel> GetUserType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_User_Type.Select(s => new UserTypeViewModel()
                {
                    UserTypeID = s.TypeID,
                    UserTypeName = s.TypeName
                }).ToList();
                return list;
            }
        }

        public List<RecieveTypeViewModel> GetRecieveType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Recieve_Type.Select(s => new RecieveTypeViewModel()
                {
                    RecID = s.RecID,
                    RecName = s.RecName
                }).ToList();
                return list;
            }
        }

        public List<MaritalStatusViewModel> GetMaritalStatus()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Marital_Status.Select(s => new MaritalStatusViewModel()
                {
                    MaritalStatusID = s.MaritalID,
                    MaritalStatusName = s.MaritalName
                }).ToList();
                return list;
            }
        }

        public List<EmpowerViewModel> GetEmpower()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Empower.Select(s => new EmpowerViewModel()
                {
                    EmpowerID = s.EmpowerID,
                    EmpowerName = s.EmpowerName
                }).ToList();
                return list;
            }
        }

        public List<BloodTypeViewModel> GetBloodType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Blood_Type.Select(s => new BloodTypeViewModel()
                {
                    BloodTypeID = s.BloodID,
                    BloodTypeName = s.BloodName
                }).ToList();
                return list;
            }
        }

        public List<OccupationViewModel> GetOccupation()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Occupation.Select(s => new OccupationViewModel()
                {
                    OcID = s.OcID,
                    OcName = s.OcName
                }).ToList();
                return list;
            }
        }

        public List<ExcellentViewModel> GetExcellentType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Excellent_Type.Select(s => new ExcellentViewModel()
                {
                    ExTID = s.ExTID,
                    ExTName = s.ExTName
                }).ToList();
                return list;
            }
        }

        public List<MoveTypeViewModel> GetMoveType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Action_Type.Where(m => m.ActMove == true)
                    .Select(s => new MoveTypeViewModel()
                    {
                        MoveTypeID = s.ActID,
                        MoveTypeName = s.ActName
                    })
                    .OrderBy(o => o.MoveTypeName).ToList();
                return list;
            }
        }

        public List<MemberTypeViewModel> GetMemberType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Member_Type.Select(s => new MemberTypeViewModel()
                {
                    MemberTypeID = s.MID,
                    MemberTypeName = s.MName
                }).ToList();
                return list;
            }
        }

        public List<RentTypeViewModel> GetRentType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Rent_Type.Select(s => new RentTypeViewModel()
                {
                    RentTypeID = s.RID,
                    RentTypeName = s.RName
                }).ToList();
                return list;
            }
        }

        public List<PartTypeViewModel> GetPartType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Part_Type.Select(s => new PartTypeViewModel()
                {
                    PartTypeID = s.PID,
                    PartTypeName = s.PName
                }).ToList();
                return list;
            }
        }

        public List<BankLoanViewModel> GetBankLoan()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Bank_Loan.Select(s => new BankLoanViewModel()
                {
                    BankTypeID = s.BID,
                    BankTypeName = s.BName
                }).ToList();
                return list;
            }
        }

        public List<LoanTypeViewModel> GetLoanType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Loan_Type.Select(s => new LoanTypeViewModel()
                {
                    LoanTypeID = s.LtID,
                    LoanTypeName = s.LtName
                }).ToList();
                return list;
            }
        }

        public List<ClaimTypeViewModel> GetClaimType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Claim_Type.Select(s => new ClaimTypeViewModel()
                {
                    ClaimTypeID = s.ClID,
                    ClaimTypeName = s.ClName
                }).ToList();
                return list;
            }
        }

        public List<AccumulativeFundViewModel> GetAccumulativeFund()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Accumulative_Fund.Select(s => new AccumulativeFundViewModel()
                {
                    CuID = s.CuID,
                    CuAmoutPer = s.CuAmoutPer
                }).ToList();
                return list;
            }
        }

        public List<BenefitTypeViewModel> GetBenefitType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Benefit_Type.Select(s => new BenefitTypeViewModel()
                {
                    BenTID = s.BenTID,
                    BenTName = s.BenTName
                }).ToList();
                return list;
            }
        }

        public List<CrippleViewModel> GetCripple()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Cripple.Select(s => new CrippleViewModel()
                {
                    CrpID = s.CrpID,
                    CrpName = s.CrpName
                }).ToList();
                return list;
            }
        }

        public List<CrippleTypeViewModel> GetCrippleType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Cripple_Type.Select(s => new CrippleTypeViewModel()
                {
                    CrpTID = s.CrpTID,
                    CrpTName = s.CrpTypeName
                }).ToList();
                return list;
            }
        }

        public List<LanguageViewModel> GetLanguage()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Language.Select(s => new LanguageViewModel()
                {
                    LID = s.LID,
                    Language = s.Language
                }).ToList();
                return list;
            }
        }

        public List<LanguageSkillViewModel> GetLanguageSkill()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Language_Skill.Select(s => new LanguageSkillViewModel()
                {
                    LkID = s.LkID,
                    LkName = s.LkName
                }).ToList();
                return list;
            }
        }

        public List<LanguageSkillTypeViewModel> GetLanguageSkillType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Language_Skill_Type.Select(s => new LanguageSkillTypeViewModel()
                {
                    LkTID = s.LkTID,
                    LkTName = s.LkTName
                }).ToList();
                return list;
            }
        }

        public List<YearViewModel> GetYearHoliday()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = new List<YearViewModel>();
                lists = db.tb_Holiday.GroupBy(g => g.HolDate.Value.Year).Select(group => new { Year = group.Key })
                            .Select(s => new YearViewModel()
                            {
                                Year = s.Year
                            })
                            .OrderByDescending(x => x.Year).ToList();
                return lists;
            }
        }

        public List<ResignViewModel> GetResignType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Resign_Type.Select(s => new ResignViewModel()
                {
                    ResignID = s.ResignID,
                    ResignName = s.ResignName
                }).ToList();
                return list;
            }
        }

        public List<TimeAttendanceTypeViewModel> GetAttendanceType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_TimeAttendance_Type.Select(s => new TimeAttendanceTypeViewModel()
                {
                    TaTID = s.TaTID,
                    TaTName = s.TaTName
                }).ToList();
                return list;
            }
        }

        public List<YearViewModel> GetYearLeave()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = new List<YearViewModel>();
                lists = db.tb_Leave_Type.GroupBy(g => g.LevYear).Select(group => new { Year = group.Key })
                            .Select(s => new YearViewModel()
                            {
                                Year = s.Year
                            })
                            .OrderByDescending(x => x.Year).ToList();
                return lists;
            }
        }

        public List<MonthViewModel> GetMonth()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Month.Select(s => new MonthViewModel()
                {
                    MonthID = s.MID,
                    MonthName = s.MName
                }).ToList();
                return list;
            }
        }

    }

    public class YearViewModel
    {
        public int Year { get; set; }
    }

    public class MonthViewModel
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
    }
}