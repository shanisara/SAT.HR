using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitCremationViewModel
    {
        public int RowNumber { get; set; }
        public int BcID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BcYear { get; set; }
        public Nullable<int> MID { get; set; }
        public string BcMemberNo { get; set; }
        public string BcBeneficiary1 { get; set; }
        public string BcBeneficiary2 { get; set; }
        public string BcBeneficiary3 { get; set; }
        public string BcBeneficiary4 { get; set; }
        public string BcBeneficiary5 { get; set; }
        public string BcRecFullName { get; set; }
        public Nullable<System.DateTime> BcDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string MName { get; set; }
        public string BcDateText { get; set; }
        public string BcBeneficiary { get; set; }
        public List<BenefitCremationViewModel> ListCremation { get; set; }
    }
}