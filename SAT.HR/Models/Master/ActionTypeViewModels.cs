using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public partial class ActionTypeViewModel
    {
        public int RowNumber { get; set; }

        public int ActID { get; set; }

        public string ActName { get; set; }

        public bool? ActType { get; set; }

        public bool? ActPos { get; set; }

        public bool? ActMove { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public Nullable<System.DateTime> ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }
    }

    public class ActionTypeResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ActionTypeViewModel> data { get; set; }
    }
}
