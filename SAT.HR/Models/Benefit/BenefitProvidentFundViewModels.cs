using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitProvidentFundViewModel
    {
        public int RowNumber { get; set; }
        public int BpID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BpYear { get; set; }
        public Nullable<System.DateTime> BpDateChangeFund { get; set; }
        public Nullable<int> BpAccumFundCuID { get; set; }
        public Nullable<int> BpAssoFundCuID { get; set; }
        public string BpBeneficiary1 { get; set; }
        public string BpBeneficiary2 { get; set; }
        public string BpBeneficiary3 { get; set; }
        public string BpBeneficiary4 { get; set; }
        public string BpBeneficiary5 { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FullNameTh { get; set; }
        public string BpDateChangeFundText { get; set; }
        public string BpAccumFundCuName { get; set; }
        public string BpAssoFundCuName { get; set; }
        public string BpBeneficiary { get; set; }

        public string ProvidentFundNo { get; set; }
        public Nullable<System.DateTime> ProvidentFundDate { get; set; }

        public List<BenefitProvidentFundViewModel> ListProvidentFund { get; set; }
    }
}