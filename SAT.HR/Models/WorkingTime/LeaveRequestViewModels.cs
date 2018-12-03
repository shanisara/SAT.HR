using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class LeaveRequestViewModel
    {
        public int RowNumber { get; set; }
        public int LeaveID { get; set; }
        public string LeaveNo { get; set; }
        public Nullable<int> LeaveType { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> DayTime { get; set; }
        public Nullable<decimal> TotalDay { get; set; }
        public string LeaveReason { get; set; }
        public string CancelReason { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }

        public List<LeaveRequestViewModel> ListLeave { get; set; }
    }

    public class LeaveStatusViewModel
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}