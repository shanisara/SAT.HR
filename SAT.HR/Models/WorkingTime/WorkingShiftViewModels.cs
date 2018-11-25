using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class WorkingShiftViewModel
    {
        public int WsID { get; set; }
        public Nullable<int> WsYear { get; set; }
        public Nullable<int> WsMonth { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> Act_TimeIn { get; set; }
        public Nullable<System.DateTime> Act_TimeOut { get; set; }
        public Nullable<System.DateTime> Adj_TimeIn { get; set; }
        public Nullable<System.DateTime> Adj_TimeOut { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    }

    public class WorkingShiftPageViewModel
    {
        public int RowNumber { get; set; }
        public int UserID { get; set; }
        public int WsID { get; set; }
        public Nullable<int> WsYear { get; set; }
        public string WsMonth { get; set; }
        public string ActTimeIn { get; set; }
        public string ActTimeOut { get; set; }
        public string AdjTimeIn { get; set; }
        public string AdjTimeOut { get; set; }
        public List<WorkingShiftPageViewModel> ListWorkingShift { get; set; }
    }
}