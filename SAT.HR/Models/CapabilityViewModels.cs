using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public partial class CapabilityViewModel
    {
        public int RowNumber { get; set; }

        public int CapID { get; set; }

        public string CapYear { get; set; }

        public int? CapTID { get; set; }

        public int? MenuID { get; set; }

        public int? CapGroupID { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }


    public class CapabilityTypeViewModel
    {
        public int CapTID { get; set; }

        public string CapTName { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public string CreateBy { get; set; }

        public Nullable<System.DateTime> ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class CapabilityResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<CapabilityViewModel> data { get; set; }
    }
}
