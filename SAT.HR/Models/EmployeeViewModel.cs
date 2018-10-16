using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EmployeeViewModel
    {
        public int RowNumber { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> UserTID { get; set; }
        public string UserTName { get; set; }
        public int DivID { get; set; }
        public string DivName { get; set; }
        public int DepID { get; set; }
        public string DepName { get; set; }
        public int SecID { get; set; }
        public string SecName { get; set; }
        public int PoID { get; set; }
        public string PoName { get; set; }
        public Nullable<int> SexID { get; set; }
        public string SexName { get; set; }
        public bool? IsActive { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }

        public int? TitleID { get; set; }
        public int? UserStatusID { get; set; }
        public int? ReligionID { get; set; }
        public int? EthnicityID { get; set; }
        public int? NationalitID { get; set; }
        public int? BloodTypeID { get; set; }
        public int? MaritalStatusID { get; set; }
        public int? SararyLevel { get; set; }
        public int? SararyStep { get; set; }
        public int? ProvinceID { get; set; }
        public int? DistrictID { get; set; }
        public int? SubDistrictID { get; set; }
        public int? AgentID { get; set; }
        public int? GovernmentHelperID { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }

    }

    public class EmployeePageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeViewModel> data { get; set; }
    }

    public class FamilyTypeViewModel
    {
        public int FamTID { get; set; }
        public string FamTName { get; set; }
    }

    public class UserStatusViewModel
    {
        public int UserSID { get; set; }
        public string UserSName { get; set; }
    }
    

}