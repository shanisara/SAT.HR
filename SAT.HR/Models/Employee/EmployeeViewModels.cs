using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EmployeeViewModel
    {
        public int RowNumber { get; set; }
        public string FullNameTh { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<int> UserStatus { get; set; }
        public Nullable<int> TitleID { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameTh { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<decimal> IDCard { get; set; }
        public Nullable<decimal> TIN { get; set; }
        public Nullable<int> SexID { get; set; }
        public Nullable<int> BloodID { get; set; }
        public Nullable<int> ReligionID { get; set; }
        public Nullable<int> EthnicityID { get; set; }
        public Nullable<int> NationalityID { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<System.DateTime> RetireDate { get; set; }
        public Nullable<System.DateTime> StartWorkDate { get; set; }
        public Nullable<System.DateTime> ProbationDate { get; set; }
        public string Remuneration { get; set; }
        public Nullable<int> WorkingTypeID { get; set; }
        public string FingerScan { get; set; }
        public string CardScan { get; set; }
        public Nullable<int> SalaryLevel { get; set; }
        public Nullable<decimal> SalaryStep { get; set; }
        public Nullable<int> MpID { get; set; }
        public string MpCode { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<int> DepLvl { get; set; }
        public Nullable<int> DepLvl1 { get; set; }
        public Nullable<int> DepLvl2 { get; set; }
        public Nullable<int> DepLvl3 { get; set; }
        public Nullable<int> DepLvl4 { get; set; }
        public Nullable<int> DepLvl5 { get; set; }
        public Nullable<int> PoID { get; set; }
        public Nullable<int> DisID { get; set; }
        public Nullable<int> EmpowerID { get; set; }
        public Nullable<int> EmpowerDepID { get; set; }
        public Nullable<int> EmpowerDepLvl { get; set; }
        public Nullable<int> EmpowerDepLvl1 { get; set; }
        public Nullable<int> EmpowerDepLvl2 { get; set; }
        public Nullable<int> EmpowerDepLvl3 { get; set; }
        public Nullable<int> EmpowerDepLvl4 { get; set; }
        public Nullable<int> EmpowerDepLvl5 { get; set; }
        public Nullable<int> AgentPoAID { get; set; }
        public Nullable<int> AgentDepID { get; set; }
        public Nullable<int> AgentDepLvl { get; set; }
        public Nullable<int> AgentDepLvl1 { get; set; }
        public Nullable<int> AgentDepLvl2 { get; set; }
        public Nullable<int> AgentDepLvl3 { get; set; }
        public Nullable<int> AgentDepLvl4 { get; set; }
        public Nullable<int> AgentDepLvl5 { get; set; }
        public Nullable<int> AgentPoID { get; set; }
        public string HomeAddr { get; set; }
        public Nullable<int> HomeSubDistrictID { get; set; }
        public Nullable<int> HomeDistrictID { get; set; }
        public Nullable<int> HomeProvinceID { get; set; }
        public Nullable<int> HomeZipCode { get; set; }
        public string CurrAddr { get; set; }
        public Nullable<int> CurrSubDistrictID { get; set; }
        public Nullable<int> CurrDistrictID { get; set; }
        public Nullable<int> CurrProvinceID { get; set; }
        public Nullable<int> CurrZipCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Avatar { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string DivName { get; set; }
        public string DepName { get; set; }
        public string SecName { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public string DisName { get; set; }
        public decimal Salary { get; set; }
        public string Age { get; set; }
        public string Experience { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public Nullable<int> CrpID { get; set; }
        public Nullable<int> CrpTID { get; set; }
        public string CrpName { get; set; }
        public string CrpTypeName { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public int? ResignID { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
        public string ResignRemark { get; set; }

        public Nullable<bool> IsGPALower250 { get; set; }
        public Nullable<bool> IsToeicLower300 { get; set; }
        public Nullable<bool> IsAgeOver35 { get; set; }
    }
    

    public class EmployeePageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeViewModel> data { get; set; }
    }

}