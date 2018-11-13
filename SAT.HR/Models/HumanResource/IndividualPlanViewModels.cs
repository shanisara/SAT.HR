using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class IndividualPlanViewModel
    {
        public int RowNumber { get; set; }
        public int UserID { get; set; }
        public decimal? IDCard { get; set; }
        public string FullNameTh { get; set; }
        public int PoID { get; set; }
        public string PoName { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }



        public List<IndividualPlanViewModel> ListIndividualPlan { get; set; }
    }
}