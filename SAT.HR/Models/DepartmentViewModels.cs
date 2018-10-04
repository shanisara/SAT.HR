using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class DepartmentViewModel
    {
        public string RowNumber { get; set; }

        public int DepID { get; set; }

        public int DivID { get; set; }

        public int DepCode { get; set; }

        public string DepName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }

    }


    public class DepartmentResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DepartmentViewModel> data { get; set; }
    }
}
