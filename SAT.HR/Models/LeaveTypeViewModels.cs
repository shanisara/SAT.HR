using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class LeaveTypeViewModel
    {
        public int RowNumber { get; set; }

        public int LevID { get; set; }

        public int LevYear { get; set; }

        public string LevName { get; set; }

        public DateTime? LevStartDate { get; set; }

        public DateTime? LevEndDate { get; set; }

        public decimal? LevMax { get; set; }

        public bool? LevStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public string LevStartDateText { get; set; }

        public string LevEndDateText { get; set; }

        public string Status { get; set; }
    }

    public class LeaveTypeResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<LeaveTypeViewModel> data { get; set; }
    }
}
