using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitDeathReplacementViewModel
    {
        public int RowNumber { get; set; }
        public int BdID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> RecID { get; set; }
        public string BdFullName { get; set; }
        public string BdTime { get; set; }
        public Nullable<decimal> BdPer { get; set; }
        public Nullable<decimal> BdAmout { get; set; }
        public Nullable<System.DateTime> BdDate { get; set; }
        public string BdRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string RecName { get; set; }
        public string BdDateText { get; set; }
        public Nullable<int> BdYear { get; set; }
        public List<BenefitDeathReplacementViewModel> ListDeathReplacement { get; set; }
}
}