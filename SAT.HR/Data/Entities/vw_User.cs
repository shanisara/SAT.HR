//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAT.HR.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> TitleID { get; set; }
        public string TiFullName { get; set; }
        public string TiShortName { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameTh { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusName { get; set; }
        public Nullable<decimal> IDCard { get; set; }
        public Nullable<decimal> TIN { get; set; }
        public Nullable<int> SexID { get; set; }
        public string SexName { get; set; }
        public Nullable<int> BloodID { get; set; }
        public string BloodName { get; set; }
        public Nullable<int> ReligionID { get; set; }
        public string RelName { get; set; }
        public Nullable<int> EthnicityID { get; set; }
        public string EthnicityName { get; set; }
        public Nullable<int> NationalityID { get; set; }
        public string NationalityName { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public string MaritalName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<System.DateTime> RetireDate { get; set; }
        public Nullable<System.DateTime> StartWorkDate { get; set; }
        public Nullable<System.DateTime> ProbationDate { get; set; }
        public string Remuneration { get; set; }
        public Nullable<int> WorkingTypeID { get; set; }
        public string WorkingTypeName { get; set; }
        public string FingerScan { get; set; }
        public string CardScan { get; set; }
        public Nullable<int> SalaryLevel { get; set; }
        public Nullable<decimal> SalaryStep { get; set; }
        public Nullable<int> DivID { get; set; }
        public string DivName { get; set; }
        public Nullable<int> DepID { get; set; }
        public string DepName { get; set; }
        public Nullable<int> SecID { get; set; }
        public string SecName { get; set; }
        public Nullable<int> PoID { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public Nullable<int> EmpowerID { get; set; }
        public string EmpowerName { get; set; }
        public Nullable<int> EmpowerDivID { get; set; }
        public string EmpowerDivName { get; set; }
        public Nullable<int> EmpowerDepID { get; set; }
        public string EmpowerDepName { get; set; }
        public Nullable<int> EmpowerSecID { get; set; }
        public string EmpowerSecName { get; set; }
        public Nullable<int> PoTID { get; set; }
        public string PoTName { get; set; }
        public Nullable<int> AgentDivID { get; set; }
        public string AgentDivName { get; set; }
        public Nullable<int> AgentDepID { get; set; }
        public string AgentDepName { get; set; }
        public Nullable<int> AgentSecID { get; set; }
        public string AgentSecName { get; set; }
        public Nullable<int> AgentPoID { get; set; }
        public string PoAName { get; set; }
        public string HomeAddr { get; set; }
        public Nullable<int> HomeSubDistrictID { get; set; }
        public string HomeSubDistrictName { get; set; }
        public Nullable<int> HomeDistrictID { get; set; }
        public string HomeDistrictName { get; set; }
        public Nullable<int> HomeProvinceID { get; set; }
        public string HomeProvinceName { get; set; }
        public Nullable<int> HomeZipCode { get; set; }
        public string CurrAddr { get; set; }
        public Nullable<int> CurrSubDistrictID { get; set; }
        public string CurrSubDistrictName { get; set; }
        public Nullable<int> CurrDistrictID { get; set; }
        public string CurrDistrictName { get; set; }
        public Nullable<int> CurrProvinceID { get; set; }
        public string CurrProvinceName { get; set; }
        public Nullable<int> CurrZipCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Avatar { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
