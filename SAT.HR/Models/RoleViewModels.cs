using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class RoleViewModel
    {
        public string RowNumber { get; set; }

        public int PerID { get; set; }

        public int? MenuID { get; set; }

        public bool? Status { get; set; }

        public int? GroupID { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class RoleResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<RoleViewModel> data { get; set; }
    }
}
