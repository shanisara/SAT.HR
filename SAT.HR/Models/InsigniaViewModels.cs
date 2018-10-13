using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class InsigniaViewModel
    {
        public int RowNumber { get; set; }

        public int InsID { get; set; }

        public string InsFullName { get; set; }

        public string InsShortName { get; set; }

        public bool? InsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public string Status { get; set; }
    }

    public class InsigniaResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<InsigniaViewModel> data { get; set; }
    }
}
