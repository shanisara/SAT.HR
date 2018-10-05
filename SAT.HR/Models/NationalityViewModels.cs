using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class NationalityViewModel
    {
        public string RowNumber { get; set; }

        public int NatID { get; set; }

        public string NatName { get; set; }

        public string NatStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class NationalityResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<NationalityViewModel> data { get; set; }
    }
}
