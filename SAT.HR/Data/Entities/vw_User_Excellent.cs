//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAT.HR.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_User_Excellent
    {
        public int UeID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> ExTID { get; set; }
        public string ExTName { get; set; }
        public string UeProjectName { get; set; }
        public Nullable<int> UeRecYear { get; set; }
        public Nullable<System.DateTime> UeRecDate { get; set; }
    }
}
