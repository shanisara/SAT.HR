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
        public string FullNameTh { get; set; }
        public Nullable<decimal> IDCard { get; set; }
        public Nullable<System.DateTime> ActDate { get; set; }
        public string ActPlace { get; set; }
        public string ActDesc { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string ActDateText { get; set; }
        
        public List<AccidentViewModel> listAccident { get; set; }
    }

    public class AccidentResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<AccidentViewModel> data { get; set; }
    }
}