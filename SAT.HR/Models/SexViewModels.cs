using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class SexViewModelsViewModel
    {
        public int SexID { get; set; }

        public string SexName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }
}
