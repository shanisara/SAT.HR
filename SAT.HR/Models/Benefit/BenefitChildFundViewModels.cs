using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitChildFundViewModel
    {
        public int RowNumber { get; set; }
        public int BcfID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string BcfName { get; set; }
        public Nullable<decimal> BcfIDCard { get; set; }
        public Nullable<System.DateTime> BcfBirthDate { get; set; }
        public Nullable<System.DateTime> BcfExpireDate { get; set; }
        public Nullable<decimal> BcfAmout { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string BcfBirthDateText { get; set; }
        public string BcfExpireDateText { get; set; }
        public string TypeFund { get; set; }
        public Nullable<decimal> PayRateFund { get; set; }
        public string InvNoFund { get; set; }
        public Nullable<System.DateTime> DateFund { get; set; }
        public string SchoolYear { get; set; }
        public List<BenefitChildFundViewModel> ListChildFund { get; set; }
    }
}