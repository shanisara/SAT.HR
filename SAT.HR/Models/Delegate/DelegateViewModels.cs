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
        public string FromMp { get; set; }
        public string ToMp { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
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
    }


    public class DelegateResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DelegateViewModel> data { get; set; }
    }
}