using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitDocViewModel
    {
        public int RowNumber { get; set; }
        public int BdID { get; set; }
        public string BdDocName { get; set; }
        public string BdDocPath { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string ModifyDateText { get; set; }
    }

    public class BenefitDocResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<BenefitDocViewModel> data { get; set; }
    }
}