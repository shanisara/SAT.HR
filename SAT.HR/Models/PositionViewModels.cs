using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class PositionViewModel
    {
        public int PoID { get; set; }

        public int? PoCode { get; set; }

        public string PoName { get; set; }

        public int? PoGroup { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class PositionResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<PositionViewModel> data { get; set; }
    }
}
