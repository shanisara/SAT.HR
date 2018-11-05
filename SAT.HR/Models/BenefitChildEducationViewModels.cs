using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitChildEducationViewModel
    {
        public int RowNumber { get; set; }
        public int BceID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string BceName { get; set; }
        public Nullable<decimal> BceIDCard { get; set; }
        public Nullable<System.DateTime> BcdBirthDate { get; set; }
        public Nullable<System.DateTime> BcdExpireDate { get; set; }
        public Nullable<decimal> BcdAmout { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string BcdBirthDateText { get; set; }
        public string BcdExpireDateText { get; set; }
        public List<BenefitChildEducationViewModel> ListChildEducation { get; set; }
}
}