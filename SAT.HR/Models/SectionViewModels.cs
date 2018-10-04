using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class SectionViewModel
    {
        public string RowNumber { get; set; }

        public int SecID { get; set; }

        public string SecCode { get; set; }

        public string SecName { get; set; }

        public bool? SecStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class SectionResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SectionViewModel> data { get; set; }
    }
}