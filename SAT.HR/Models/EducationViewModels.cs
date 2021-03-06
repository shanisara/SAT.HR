using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class EducationViewModel
    {
        public int RowNumber { get; set; }

        public int EduID { get; set; }

        public string EduCode { get; set; }

        public string EduName { get; set; }

        public bool? EduStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public string Status { get; set; }
    }

    public class EducationResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EducationViewModel> data { get; set; }
    }
}
