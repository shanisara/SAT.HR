using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitLoanViewModel
    {
        public int RowNumber { get; set; }
        public int? BlYear { get; set; }
        public int BlID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BID { get; set; }
        public Nullable<int> LtID { get; set; }
        public Nullable<decimal> BlAccountNo { get; set; }
        public Nullable<System.DateTime> BlStartDate { get; set; }
        public Nullable<System.DateTime> BlEndDate { get; set; }
        public Nullable<System.DateTime> BlCloseDate { get; set; }
        public Nullable<decimal> BlPeriod { get; set; }
        public Nullable<decimal> BlPeriodPay { get; set; }
        public Nullable<decimal> BISummaryAmout { get; set; }
        public string BlRemark { get; set; }
        public Nullable<int> CardID { get; set; }
        public Nullable<int> BlStatus { get; set; }
        public Nullable<decimal> Outstanding { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FullNameTh { get; set; }
        public string BName { get; set; }
        public string LtName { get; set; }
        public string BlStatusName { get; set; }

        public List<BenefitLoanViewModel> ListLoan { get; set; }
    }
}