using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class LevelTransferViewModel
    {
        public int RowNumber { get; set; }
        public int MlID { get; set; }
        public int? MlYear { get; set; }
        public string MlBookCmd { get; set; }
        public Nullable<System.DateTime> MlDateCmd { get; set; }
        public string MlSignatory { get; set; }
        public string MIPathFile { get; set; }
        public Nullable<bool> MlStatus { get; set; }
        public int MlTotal { get; set; }
        public string MlDateCmdText { get; set; }
        public string MlStatusName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateDateText { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public HttpPostedFileBase fileUpload { get; set; }
        public List<LevelTransferDetailViewModel> ListDetail { get; set; }
    }

    public class LevelTransferResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<LevelTransferViewModel> data { get; set; }
    }

    public class LevelTransferDetailViewModel
    {
        public int RowNumber { get; set; }
        public int MlID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public Nullable<decimal> MlLevelOld { get; set; }
        public Nullable<decimal> MlStepOld { get; set; }
        public Nullable<decimal> MlSalaryOld { get; set; }
        public Nullable<decimal> MlLevelNew { get; set; }
        public Nullable<decimal> MlStepNew { get; set; }
        public Nullable<decimal> MlSalaryNew { get; set; }
        public string MlStatus { get; set; }
        public string MlRemark { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    public class LevelTransferDetailResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<LevelTransferDetailViewModel> data { get; set; }
    }

}