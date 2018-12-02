using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class OrganizationViewModel
    {
        public int MpID { get; set; }
        public string MpCode { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string DivName { get; set; }
        public string DepName { get; set; }
        public string SecName { get; set; }
        public string PoName { get; set; }
        public string FullNameTh { get; set; }
        public Nullable<int> TypeID { get; set; }
        public int DepLevel { get; set; }
        public string PathMpID { get; set; }
        public string MpName { get; set; }
        //public int MpID { get; set; }
        //public string MpCode { get; set; }
        //public Nullable<int> ParentID { get; set; }
        //public Nullable<int> DepID { get; set; }
        //public Nullable<int> PoID { get; set; }
        //public Nullable<int> DisID { get; set; }
        //public Nullable<int> EduID { get; set; }
        //public Nullable<int> TypeID { get; set; }
        //public Nullable<int> UserID { get; set; }
        //public Nullable<int> LevelID { get; set; }
        //public Nullable<int> Seq { get; set; }
        //public string TiShortName { get; set; }
        //public string FullNameTh { get; set; }
    }
}