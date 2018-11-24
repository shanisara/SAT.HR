using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SAT.HR.Models
{
    public class PositionViewModel
    {
        public int RowNumber { get; set; }

        public int PoID { get; set; }

        public string PoCode { get; set; }

        public string PoName { get; set; }

        public bool? PoStatus { get; set; }

        public int? TypeID { get; set; }

        public string ProjectNo { get; set; }

        public string ProjectName { get; set; }

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
        public string MopYear { get; set; }
        public int? UserTID { get; set; }
        public int? MtID { get; set; }
        public string MopBookCmd { get; set; }
        public DateTime? MopDateCmd { get; set; }
        public DateTime? MopDateEff { get; set; }
        public string UserTName { get; set; }
        public string MopSignatory { get; set; }
        public string MopPathFile { get; set; }
        public bool? MopStatus { get; set; }
        public string MopStatusName { get; set; }
        public string MtName { get; set; }
        public int? MopTotal { get; set; }
        public string MopDateCmdText { get; set; }

        public string CreateDateText { get; set; }
        public string MopDateEffText { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

        public HttpPostedFileBase fileUpload { get; set; }
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
        public int RowNumber { get; set; }
        public int MopID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public int? CurMpID { get; set; }
        public string CurPoName { get; set; }
        public int? MovMpID { get; set; }
        public string MovPoName { get; set; }
        public Nullable<int> AgentPoTID { get; set; }
        public string AgentPoTName { get; set; }
        public Nullable<int> AgentMpID { get; set; }
        public string AgentPoName { get; set; }
        public string MovRemark { get; set; }

        public string CurrentPo { get; set; }
        public string MovePo { get; set; }
        public string AgentPo { get; set; }
        public string DivName { get; set; }
        public string DepName { get; set; }
        public string SecName { get; set; }
        public string BelongTo { get; set; }
    }

    public class PositionTransferDetailResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<PositionTransferDetailViewModel> data { get; set; }
    }

    public class PositionTypeViewModel
    {
        public int PoTID { get; set; }

        public string PoTName { get; set; }
    }

    public class PositionAgentViewModel
    {
        public int PoAID { get; set; }

        public string PoAName { get; set; }
    }

    public class PositionRateViewModel
    {
        public int RowNumber { get; set; }
        public int MpID { get; set; }
        public string MpCode { get; set; }
        public Nullable<int> DivID { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<int> SecID { get; set; }
        public Nullable<int> PoID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<int> DisID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> EduID { get; set; }
        public string TiShortName { get; set; }
        public string FullNameTh { get; set; }
        public string DivName { get; set; }
        public string DepName { get; set; }
        public string SecName { get; set; }
        public string PoName { get; set; }
        public int DivSeq { get; set; }
        public string PoCode { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    public class PositionRatePageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<PositionRateViewModel> data { get; set; }
    }

    public class ManPowerViewModel
    {
        public int? MpID { get; set; }
        public string FullName { get; set; }
        public string BelongTo { get; set; }
        public string Position { get; set; }
        public string Level { get; set; }
        public string Step { get; set; }
        public string Salary { get; set; }
        
    }

}
