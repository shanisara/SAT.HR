using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class AnnouncementViewModel
    {
        public int RowNumber { get; set; }
        public int AnnID { get; set; }
        public string AnnTopic { get; set; }
        public string AnnSubTopic { get; set; }
        public string AnnDescription { get; set; }
        public Nullable<System.DateTime> AnnDate { get; set; }
        public string AnnFileName { get; set; }
        public string AnnFilePath { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string AnnDateText { get; set; }
        public HttpPostedFileBase fileUpload { get; set; }
    }

    public class AnnouncementResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<AnnouncementViewModel> data { get; set; }
    }
}