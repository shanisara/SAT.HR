using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserHistoryViewModel
    {
        public int RowNumber { get; set; }
        public int UhID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<System.DateTime> UhEditDate { get; set; }
        public Nullable<int> OldTiID { get; set; }
        public string OldTiFullName { get; set; }
        public string OldTiShortName { get; set; }
        public Nullable<int> NewTiID { get; set; }
        public string NewTiFullName { get; set; }
        public string NewTiShortName { get; set; }
        public string OldFirstNameTh { get; set; }
        public string OldLastNameTh { get; set; }
        public string OldFirstNameEn { get; set; }
        public string OldLastNameEn { get; set; }
        public string NewLastNameEn { get; set; }
        public string NewFirstNameTh { get; set; }
        public string NewLastNameTh { get; set; }
        public string NewFirstNameEn { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> UhStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string UhEditDateText { get; set; }
        public int? SexID { get; set; }
        public bool? IsChange { get; set; }
        public string OldFullNameTh { get; set; }
        public string OldFullNameEn { get; set; }
        public string NewFullNameTh { get; set; }
        public string NewFullNameEn { get; set; }

        public List<UserHistoryViewModel> ListHistory { get; set; }
    }

}