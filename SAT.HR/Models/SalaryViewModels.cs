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

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }
    }

    public class SalaryResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SalaryViewModel> data { get; set; }
    }

    public class SalaryLevelViewModel
    {
        public int Level { get; set; }
    }
    public class SalaryStepViewModel
    {
        public int Step { get; set; }
    }
}
