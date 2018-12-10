using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class LeaveRequestViewModel
    {
        public int RowNumber { get; set; }
        public int FormID { get; set; }
        public string DocNo { get; set; }
        public Nullable<int> LeaveType { get; set; }
        public Nullable<int> RequestID { get; set; }
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
        public Nullable<decimal> LeaveQuota { get; set; }
        public Nullable<decimal> LeaveBalance { get; set; }
        public Nullable<decimal> LeaveStandard { get; set; }
        public Nullable<decimal> LeaveUsed { get; set; }
        public Nullable<decimal> LeaveMax { get; set; }
        public Nullable<decimal> LeaveTotalDay { get; set; }
        public string Approver { get; set; }
        public string RequestName { get; set; }
        public string LeaveTypeName { get; set; }
        public string StatusName { get; set; }
        public string CreateByName { get; set; }
        public Nullable<int> LeaveYear { get; set; }
        public string PathFile { get; set; }
        public HttpPostedFileBase LeaveFile { get; set; }
        public string CreateDateText { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<LeaveRequestViewModel> ListLeave { get; set; }
    }

    public class LeaveRequestPageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<LeaveRequestViewModel> data { get; set; }
    }

    public class LeaveStatusViewModel
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}