using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EvaluationViewModel
    {
        public int RowNumber { get; set; }
        public int UserID { get; set; }
        public decimal? IDCard { get; set; }
        public string FullNameTh { get; set; }
        public int PoID { get; set; }
        public string PoName { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

        public string EvaluationName { get; set; }
        public int? CapYear { get; set; }
        public string CapTName { get; set; }
        public string CapGName { get; set; }
        public string CapGTName { get; set; }

        public List<EvaluationDetailViewModel> ListEvaluation { get; set; }
    }

    public class EvaluationPageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EvaluationViewModel> data { get; set; }
    }

    public class EvaluationDetailViewModel
    {
        public int? CapID { get; set; }
        public int? UserID { get; set; }
        public int CapDID { get; set; }
        public string CapDName { get; set; }
        public string CapDDesc { get; set; }
        public int? CapScore1 { get; set; }
        public int? CapScore2 { get; set; }
        public int? UserScore1 { get; set; }
        public int? UserScore2 { get; set; }
    }
}