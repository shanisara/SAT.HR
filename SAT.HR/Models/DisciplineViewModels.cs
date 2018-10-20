using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class DisciplineViewModel
    {
        public int RowNumber { get; set; }

        public int DisID { get; set; }

        public string DisName { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public bool? DisStatus { get; set; }

        public string Status { get; set; }
    }

    public class DisciplineResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DisciplineViewModel> data { get; set; }
    }

}