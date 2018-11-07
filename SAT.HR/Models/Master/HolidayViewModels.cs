using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class HolidayViewModel
    {
        public int RowNumber { get; set; }

        public int HolID { get; set; }

        public int HolYear { get; set; }

        public DateTime? HolDate { get; set; }

        public string HolDateText { get; set; }

        public string HolDescription { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }
    }

    public class HolidayResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<HolidayViewModel> data { get; set; }
    }
}
