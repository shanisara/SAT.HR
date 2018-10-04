using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class LeaveTypeViewModel
    {
        public string RowNumber { get; set; }
        public int LevID { get; set; }

        public string LevYear { get; set; }

        public DateTime? LevStartDate { get; set; }

        public DateTime? LevEndDate { get; set; }

        public decimal? LevMax { get; set; }

        public bool? LevStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class LeaveTypeResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<LeaveTypeViewModel> data { get; set; }
    }
}
