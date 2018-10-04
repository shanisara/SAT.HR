using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class ReligionViewModel
    {
        public int RelD { get; set; }

        public string RelName { get; set; }

        public bool? RelStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class ReligionResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ReligionViewModel> data { get; set; }
    }
}
