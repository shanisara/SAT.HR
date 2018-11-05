using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitOtherWelfareViewModel
    {
        public int RowNumber { get; set; }
        public int BoID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BenTID { get; set; }
        public Nullable<int> BoRecID { get; set; }
        public string BoRecFullName { get; set; }
        public Nullable<int> BoOptRecID { get; set; }
        public string BoOptFullName { get; set; }
        public string BoTime { get; set; }
        public Nullable<decimal> BoPer { get; set; }
        public Nullable<decimal> BoAmout { get; set; }
        public Nullable<System.DateTime> BoDate { get; set; }
        public string BoRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FullNameTh { get; set; }
        public string BenTName { get; set; }
        public string BoRecName { get; set; }
        public string BoOptRecName { get; set; }
        public string BoDateText { get; set; }

        public List<BenefitOtherWelfareViewModel> ListOtherWelfare { get; set; }
    }
}