using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class DepartmentViewModel
    {
        public int RowNumber { get; set; }

        public int DepID { get; set; }

        public int? DivID { get; set; }

        public string DivName { get; set; }

        public string DepName { get; set; }

        public bool DepStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

    }


    public class DepartmentResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DepartmentViewModel> data { get; set; }
    }
}
