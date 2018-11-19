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
    public class EducationReport
    {
       public string UserID { get; set; }
        public string TiFullName { get; set; }
        public string TiShortName { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameTh { get; set; }
        public string FullNameTh { get; set; }
        public string EduName { get; set; }
        public string DegName { get; set; }
        public string MajName { get; set; }
        public string CountryName { get; set; }
        public string SalaryLevel { get; set; }
        public string PoName { get; set; }
    }
}
