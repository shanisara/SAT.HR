using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserPositionViewModel
    {
        public int RowNumber { get; set; }
        public int UpID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> ActID { get; set; }
        public string ActName { get; set; }
        public string UpCmd { get; set; }
        public Nullable<int> PoTID { get; set; }
        public string PoTName { get; set; }
        public Nullable<int> DivID { get; set; }
        public string DivName { get; set; }
        public Nullable<int> DepID { get; set; }
        public string DepName { get; set; }
        public Nullable<int> SecID { get; set; }
        public string SecName { get; set; }
        public Nullable<int> PoID { get; set; }
        public string PoName { get; set; }
        public Nullable<int> PoAID { get; set; }
        public string PoAName { get; set; }
        public string UpLevel { get; set; }
        public Nullable<decimal> UpSalary { get; set; }
        public Nullable<System.DateTime> UpCmdDate { get; set; }
        public Nullable<System.DateTime> UpForceDate { get; set; }
        public string UpRemark { get; set; }
        public string UpPathFile { get; set; }
        public string UpCmdDateText { get; set; }
        public string UpForceDateText { get; set; }


        public List<UserPositionViewModel> ListPosition { get; set; }
    }

}