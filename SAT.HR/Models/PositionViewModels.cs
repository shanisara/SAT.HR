using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class PositionViewModel
    {
        public int RowNumber { get; set; }

        public int PoID { get; set; }

        public string PoCode { get; set; }

        public string PoName { get; set; }

        public bool? PoStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public string Status { get; set; }
    }

    public class PositionResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<PositionViewModel> data { get; set; }
    }

    public class PositionTransferViewModel
    {
        public int RowNumber { get; set; }

        public int MopID { get; set; }

        public int MopYear { get; set; }

        public string EmpTName { get; set; }

        public string MtName { get; set; }

        public string MopBookCmd { get; set; }

        public int MopTotal { get; set; }

        public string MopDateCmdText { get; set; }

        public string MopStatusName { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<PositionTransferDetailViewModel> ListDetail { get; set; }
    }

    public class PositionTransferResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<PositionTransferViewModel> data { get; set; }
    }

    public class PositionTransferDetailViewModel
    {

    }

    public class PositionTypeViewModel
    {
        public int PoTID { get; set; }

        public string PoTName { get; set; }
    }
}
