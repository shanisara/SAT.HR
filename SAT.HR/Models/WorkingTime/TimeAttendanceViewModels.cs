using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class TimeAttendanceViewModel
    {
        public int TaID { get; set; }
        public Nullable<int> TaTID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> Std_TimeIn { get; set; }
        public Nullable<System.DateTime> Act_TimeIn { get; set; }
        public Nullable<System.DateTime> Std_TimeOut { get; set; }
        public Nullable<System.DateTime> Act_TimeOut { get; set; }
        public Nullable<System.DateTime> Adj_TimeIn { get; set; }
        public Nullable<System.DateTime> Adj_TimeOut { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }

        public List<TimeAttendanceViewModel> ListTimeAttendance { get; set; }
    }

    public class TimeAttendanceTypeViewModel
    {
        public int TaTID { get; set; }
        public string TaTName { get; set; }
    }

    public class TimeAttendancePageViewModel
    {
        public int RowNumber { get; set; }
        public int TaID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string TaTName { get; set; }
        public string StdTimeIn { get; set; }
        public string ActTimeIn { get; set; }
        public string StdTimeOut { get; set; }
        public string ActTimeOut { get; set; }
        public string AdjTimeIn { get; set; }
        public string AdjTimeOut { get; set; }
        public string Remark { get; set; }

        public List<TimeAttendancePageViewModel> ListTimeAttendance { get; set; }
    }
}