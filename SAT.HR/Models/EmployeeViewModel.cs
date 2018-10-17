using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EmployeeViewModel
    {
        public int RowNumber { get; set; }
        public string FullName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
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
        public Nullable<int> DivID { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<int> SecID { get; set; }
        public Nullable<int> PoID { get; set; }
        public Nullable<int> EmpowerID { get; set; }
        public Nullable<int> EmpowerDivID { get; set; }
        public Nullable<int> EmpowerDepID { get; set; }
        public Nullable<int> EmpowerSecID { get; set; }
        public Nullable<int> PoTID { get; set; }
        public Nullable<int> AgentDivID { get; set; }
        public Nullable<int> AgentDepID { get; set; }
        public Nullable<int> AgentSecID { get; set; }
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
        public decimal Salary { get; set; }
        public string Age { get; set; }
        public string Experience { get; set; }
    }

    public class EmployeePageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeViewModel> data { get; set; }
    }

    public class RecieveTypeViewModel
    {
        public int RecID { get; set; }
        public string RecName { get; set; }
    }

    public class UserStatusViewModel
    {
        public int UserStatusID { get; set; }
        public string UserStatusName { get; set; }
    }

    public class WorkingTypeViewModel
    {
        public int WorkingTypeID { get; set; }
        public string WorkingTypeName { get; set; }
    }

    public class AgentViewModel
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
    }

    public class EmpowerViewModel
    {
        public int EmpowerID { get; set; }
        public string EmpowerName { get; set; }
    }

    public class BloodTypeViewModel
    {
        public int BloodTypeID { get; set; }
        public string BloodTypeName { get; set; }
    }

    public class MaritalStatusViewModel
    {
        public int MaritalStatusID { get; set; }
        public string MaritalStatusName { get; set; }
    }

    public class UserFamilyViewModel
    {
        public int RowNumber { get; set; }
        public int UserID { get; set; }
        public int UfID { get; set; }
        public string UfName { get; set; }
        public Nullable<decimal> UfCardID { get; set; }
        public Nullable<System.DateTime> UfDOB { get; set; }
        public Nullable<bool> UfLifeStatus { get; set; }
        public Nullable<int> TdID { get; set; }
        public Nullable<int> PoID { get; set; }
        public Nullable<bool> UfStudyStatus { get; set; }
        public int RecID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string UfAge { get; set; }
        public int? MaritalID { get; set; }
        public Nullable<DateTime> WeddingDate { get; set; }
        public Nullable<DateTime> DivorceDate { get; set; }
        public string Career { get; set; }


        public string UfLifeStatusName { get; set; }
        public string TdName { get; set; }
        public string PoName { get; set; }
        public string MaritalName { get; set; }
        public string UfStudyStatusName { get; set; }



        public List<UserFamilyViewModel> ListFather { get; set; }
        public List<UserFamilyViewModel> ListMother { get; set; }
        public List<UserFamilyViewModel> ListSpouse { get; set; }
        public List<UserFamilyViewModel> ListChild { get; set; }
    }

    public class UserEducationViewModel
    {

    }

    public class UserPositionViewModel
    {

    }

    public class UserTrainningViewModel
    {

    }

    public class UserInsigniaViewModel
    {

    }

    public class UserExcellentViewModel
    {

    }

    public class UserCertificateViewModel
    {

    }

    public class UserHistoryViewModel
    {

    }

}