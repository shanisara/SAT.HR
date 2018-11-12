using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserExcellentViewModel
    {
        public int RowNumber { get; set; }
        public int UeID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> ExID { get; set; }
        public Nullable<int> ExTID { get; set; }
        public string ExName { get; set; }
        public string ExTName { get; set; }

        public string UeProjectName { get; set; }
        public string UeRecYear { get; set; }
        public Nullable<System.DateTime> UeRecDate { get; set; }
        public string UeRecDateText { get; set; }
        public string PoName { get; set; }
        public string FullPosition { get; set; }
        public List<UserExcellentViewModel> ListExcellent { get; set; }
    }

}