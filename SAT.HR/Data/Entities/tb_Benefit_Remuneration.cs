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
    
    public partial class tb_Benefit_Remuneration
    {
        public int BrID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BrYear { get; set; }
        public int RecID { get; set; }
        public Nullable<decimal> BrAmout { get; set; }
        public Nullable<System.DateTime> BrDate { get; set; }
        public string BrRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    
        public virtual tb_Recieve_Type tb_Recieve_Type { get; set; }
    }
}
