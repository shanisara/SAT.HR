using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public partial class ActionTypeViewModel
    {
        public int ActID { get; set; }

        public string ActCode { get; set; }

        public string ActName { get; set; }

        public string ActType { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public string CreateBy { get; set; }

        public Nullable<System.DateTime> ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class ActionTypeResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ActionTypeViewModel> data { get; set; }
    }
}
