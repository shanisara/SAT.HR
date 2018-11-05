using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserInsigniaViewModel
    {
        public int RowNumber { get; set; }
        public int UiID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> InsID { get; set; }
        public string InsFullName { get; set; }
        public string InsShortName { get; set; }
        public string UiYear { get; set; }
        public string UiBook { get; set; }
        public string UiPart { get; set; }
        public string UiPage { get; set; }
        public Nullable<System.DateTime> UiRecDate { get; set; }
        public Nullable<System.DateTime> UiRetDate { get; set; }
        public string UiCmd { get; set; }
        public string UiPartFile { get; set; }
        public string UiRecDateText { get; set; }
        public string UiRetDateText { get; set; }
        //public HttpPostedFileBase fileUpload { get; set; }
        public List<UserInsigniaViewModel> ListInsignia { get; set; }
    }

}