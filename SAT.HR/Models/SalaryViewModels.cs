using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class SalaryViewModel
    {
        public int RowNumber { get; set; }

        public int SaID { get; set; }

        public decimal? SaLevel { get; set; }

        public decimal? SaStep { get; set; }

        public decimal? SaRate { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class SalaryResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SalaryViewModel> data { get; set; }
    }
}
