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
    
    public partial class vw_User_Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> UserTID { get; set; }
        public string UserTName { get; set; }
        public Nullable<int> DivID { get; set; }
        public string DivName { get; set; }
        public Nullable<int> DepID { get; set; }
        public string DepName { get; set; }
        public Nullable<int> SecID { get; set; }
        public string SecName { get; set; }
        public Nullable<int> PoID { get; set; }
        public string PoName { get; set; }
        public Nullable<int> SexID { get; set; }
        public string SexName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Avatar { get; set; }
    }
}
