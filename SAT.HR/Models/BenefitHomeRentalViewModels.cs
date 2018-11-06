using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitHomeRentalViewModel
    {
        public int RowNumber { get; set; }
        public int BhrID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BhrYear { get; set; }
        public Nullable<int> RID { get; set; }
        public Nullable<int> PID { get; set; }
        public Nullable<int> BhrLevel { get; set; }
        public Nullable<int> BhrStep { get; set; }
        public Nullable<System.DateTime> BhrStartDate { get; set; }
        public Nullable<System.DateTime> BhrEndDate { get; set; }
        public Nullable<decimal> BhrAmout { get; set; }
        public string BhrRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FunnNameTh { get; set; }
        public string RName { get; set; }
        public string PName { get; set; }
        public string BhrStartDateText { get; set; }
        public string BhrEndDateText { get; set; }

        public List<BenefitHomeRentalViewModel> ListHomeRental { get; set; }
    }
}