using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class InsigniaViewModel
    {
        public int RowNumber { get; set; }

        public int InsID { get; set; }

        public string InsFullName { get; set; }

        public string InsShortName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class InsigniaResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<InsigniaViewModel> data { get; set; }
    }
}
