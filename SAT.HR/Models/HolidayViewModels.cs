using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class HolidayViewModel
    {
        public int HolID { get; set; }

        public string HolYear { get; set; }

        public DateTime? HolDate { get; set; }

        public string HolDesc { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class HolidayResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<HolidayViewModel> data { get; set; }
    }
}
