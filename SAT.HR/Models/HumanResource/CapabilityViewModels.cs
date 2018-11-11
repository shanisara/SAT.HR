using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public partial class CapabilityViewModel
    {
        public int RowNumber { get; set; }

        public int CapID { get; set; }

        public int? CapYear { get; set; }

        public int? CapTID { get; set; }

        public string CapTName { get; set; }

        public int? CapGID { get; set; }

        public string CapGName { get; set; }

        public int? CapGTID { get; set; }

        public string CapGTName { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }
    }

    public class CapabilityDetailViewModel
    {
        public int CapDID { get; set; }

        public string CapDName { get; set; }

        public string CapDDesc { get; set; }

        public int? CapID { get; set; }

        public int? Score1 { get; set; }

        public int? Score2 { get; set; }

        public List<CapabilityDetailViewModel> _ListCapabilityDetail { get; set; }
    }

    public class CapabilityTypeViewModel
    {
        public int CapTID { get; set; }

        public string CapTName { get; set; }
    }

    public class CapabilityGroupViewModel
    {
        public int CapGID { get; set; }

        public string CapGName { get; set; }

        public string TableName { get; set; }
    }

    public class CapabilityGroupTypeViewModel
    {
        public int CapGTID { get; set; }

        public string CapGTName { get; set; }
    }

    public class CapabilityResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<CapabilityViewModel> data { get; set; }
    }

    public class YearCapabilityViewModel
    {
        public int Year { get; set; }
    }

    public partial class CapabilityModel
    {
        public int CapID { get; set; }
        public int CapYear { get; set; }
        public string CapName { get; set; }
    }
}
