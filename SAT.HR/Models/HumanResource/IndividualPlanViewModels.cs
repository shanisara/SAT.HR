using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class IndividualPlanViewModel
    {
        public int RowNumber { get; set; }
        public int PlanID { get; set; }
        public int UserID { get; set; }
        public string PlanName { get; set; }
        public string PlanDesc { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }

        public string StartDateText { get; set; }
        public string EndDateText { get; set; }
        public string CreateDateText { get; set; }

        public decimal? IDCard { get; set; }
        public string FullNameTh { get; set; }
        public int PoID { get; set; }
        public string PoName { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

        public List<IndividualPlanViewModel> ListIndividualPlan { get; set; }
    }
}