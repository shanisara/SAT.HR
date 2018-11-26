using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserTrainningViewModel
    {
        public int RowNumber { get; set; }
        public int UtID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> TtID { get; set; }
        public string TtName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CountryName { get; set; }
        public string UtCourse { get; set; }
        public Nullable<System.DateTime> UtStartDate { get; set; }
        public Nullable<System.DateTime> UtEndDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string UtStartDateText { get; set; }
        public string UtEndDateText { get; set; }

        public List<UserTrainningViewModel> ListTrainning { get; set; }

    }

}