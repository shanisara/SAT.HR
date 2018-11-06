using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitRemunerationViewModel
    {
        public int RowNumber { get; set; }
        public int BrID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BrYear { get; set; }
        public int RecID { get; set; }
        public Nullable<decimal> BrAmout { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string BrRemark { get; set; }
        public Nullable<System.DateTime> BrDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FullNameTh { get; set; }
        public string RecName { get; set; }

        public List<BenefitRemunerationViewModel> ListRemuneration { get; set; }
}
}