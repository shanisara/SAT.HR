using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class SectionViewModel
    {
        public int RowNumber { get; set; }

        public int SecID { get; set; }

        public string SecName { get; set; }

        public int? DivID { get; set; }

        public string DivName { get; set; }

        public int? DepID { get; set; }

        public string DepName { get; set; }

        public bool? SecStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public string Status { get; set; }
    }

    public class SectionResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SectionViewModel> data { get; set; }
    }
}