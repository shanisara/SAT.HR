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
    
    public partial class tb_Benefit_Other_Welfare
    {
        public int BoID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BenTID { get; set; }
        public Nullable<int> BoRecID { get; set; }
        public string BoRecFullName { get; set; }
        public Nullable<int> BoOptRecID { get; set; }
        public string BoOptFullName { get; set; }
        public string BoTime { get; set; }
        public Nullable<decimal> BoPer { get; set; }
        public Nullable<decimal> BoAmout { get; set; }
        public Nullable<System.DateTime> BoDate { get; set; }
        public string BoRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    
        public virtual tb_Benefit_Type tb_Benefit_Type { get; set; }
        public virtual tb_Recieve_Type tb_Recieve_Type { get; set; }
        public virtual tb_Recieve_Type tb_Recieve_Type1 { get; set; }
        public virtual tb_User tb_User { get; set; }
    }
}
