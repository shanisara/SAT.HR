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
    
    public partial class tb_Organization
    {
        public int OrgID { get; set; }
        public string OrgCode { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<int> PosID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> Level { get; set; }
    
        public virtual tb_Departments tb_Departments { get; set; }
        public virtual tb_Position tb_Position { get; set; }
        public virtual tb_User tb_User { get; set; }
    }
}
