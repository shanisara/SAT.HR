using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class AccidentViewModel
    {
        public int RowNumber { get; set; }
        public int ActID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> ActDate { get; set; }
        public string ActPlace { get; set; }
        public string ActDesc { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public string ModifyDate { get; set; }
        public Nullable<System.DateTime> ModifyBy { get; set; }

    }

    public class AccidentResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<AccidentViewModel> data { get; set; }
    }
}