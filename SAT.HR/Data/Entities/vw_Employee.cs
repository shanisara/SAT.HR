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
    
    public partial class vw_Employee
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<decimal> IDCard { get; set; }
        public string FullNameTh { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> DivID { get; set; }
        public string DivName { get; set; }
        public Nullable<int> DepID { get; set; }
        public string DepName { get; set; }
        public Nullable<int> SecID { get; set; }
        public string SecName { get; set; }
        public Nullable<int> PoID { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public Nullable<int> EduID { get; set; }
        public string EduName { get; set; }
        public Nullable<int> MpCode { get; set; }
        public string MpMan { get; set; }
        public Nullable<int> SalaryLevel { get; set; }
        public Nullable<decimal> SalaryStep { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public Nullable<int> RoleID { get; set; }
    }
}
