using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitMedicalViewModel
    {
        public int RowNumber { get; set; }
        public int BmID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> BmYear { get; set; }
        public Nullable<int> ClID { get; set; }
        public Nullable<int> RecID { get; set; }
        public Nullable<decimal> BmCardID { get; set; }
        public Nullable<System.DateTime> BmDate { get; set; }
        public Nullable<decimal> BmAmoutService { get; set; }
        public Nullable<decimal> BmAmoutCare { get; set; }
        public string BmRemark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FullNameTh { get; set; }
        public string RecName { get; set; }
        public string BmCardName { get; set; }
        public string BmDateText { get; set; }

        public List<BenefitMedicalViewModel> ListMedical { get; set; }
    }
}