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
    
    public partial class tb_Leave_Request
    {
        public int FormID { get; set; }
        public string DocNo { get; set; }
        public int LeaveYear { get; set; }
        public Nullable<int> LeaveType { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> DayTime { get; set; }
        public Nullable<decimal> TotalDay { get; set; }
        public string LeaveReason { get; set; }
        public string CancelReason { get; set; }
        public string Remark { get; set; }
        public string PathFile { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    
        public virtual tb_User tb_User { get; set; }
    }
}