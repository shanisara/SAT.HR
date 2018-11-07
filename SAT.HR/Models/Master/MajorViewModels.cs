using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class MajorViewModel
    {
        public int RowNumber { get; set; }

        public int MajID { get; set; }

        public string MajName { get; set; }

        public bool? MajStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public string Status { get; set; }
    }

    public class MajorResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<MajorViewModel> data { get; set; }
    }
}
