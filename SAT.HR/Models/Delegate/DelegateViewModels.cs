using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class DelegateViewModel
    {
        public int RowNumber { get; set; }
        public int DelegateID { get; set; }
        public string DelegateTypeName { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string FromMp { get; set; }
        public string ToMp { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> DelegateType { get; set; }
        public Nullable<int> FormMasterID { get; set; }
        public Nullable<int> FromMpID { get; set; }
        public Nullable<int> FromUserID { get; set; }
        public Nullable<int> ToMpID { get; set; }
        public Nullable<int> ToUserID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string FromUserDivName { get; set; }
        public string FromUserDepName { get; set; }
        public string FromUserSecName { get; set; }
        public string FromUserPoName { get; set; }
        public string ToUserDivName { get; set; }
        public string ToUserDepName { get; set; }
        public string ToUserSecName { get; set; }
        public string ToUserPoName { get; set; }
        public string FromMPDivName { get; set; }
        public string FromMPDepName { get; set; }
        public string FromMPSecName { get; set; }
        public string FromMPFullName { get; set; }
        public string ToMPDivName { get; set; }
        public string ToMPDepName { get; set; }
        public string ToMPSecName { get; set; }
        public string ToMPFullName { get; set; }
        public string FromUserDepartment { get; set; }
        public string ToUserDepartment { get; set; }
        public string FromMPDepartment { get; set; }
        public string ToMPDepartment { get; set; }
        public string StartDateText { get; set; }
        public string EndDateText { get; set; }
        public string FromDelegate { get; set; }
        public string ToDelegate { get; set; }
    }


    public class DelegateResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DelegateViewModel> data { get; set; }
    }
}