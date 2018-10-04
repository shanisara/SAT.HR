using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class TitleViewModel
    {
        public string RowNumber { get; set; }

        public int TiID { get; set; }

        public string TiFullName { get; set; }

        public string TiShortName { get; set; }

        public bool? TiStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class TitleResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<TitleViewModel> data { get; set; }
    }
}
