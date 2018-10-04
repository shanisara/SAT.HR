using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class DivisionViewModel
    {
        public string RowNumber { get; set; }
        public int DivID { get; set; }

        public int DivCode { get; set; }

        public string DivName { get; set; }

        public bool? DivStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class DivisionResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DivisionViewModel> data { get; set; }
    }

}
