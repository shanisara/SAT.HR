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
    
    public partial class tb_LeaveType
    {
        public int LevID { get; set; }
        public int LevYear { get; set; }
        public Nullable<System.DateTime> LevStartDate { get; set; }
        public Nullable<System.DateTime> LevEndDate { get; set; }
        public Nullable<decimal> LevMax { get; set; }
        public string LevName { get; set; }
        public Nullable<bool> LevStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    }
}
