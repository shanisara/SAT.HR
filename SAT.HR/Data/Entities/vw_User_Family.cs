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
    
    public partial class vw_User_Family
    {
        public int UfID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<decimal> IDCard { get; set; }
        public string UfName { get; set; }
        public Nullable<decimal> UfCardID { get; set; }
        public Nullable<System.DateTime> UfDOB { get; set; }
        public Nullable<bool> UfLifeStatus { get; set; }
        public string UfLifeStatusName { get; set; }
        public Nullable<int> TdID { get; set; }
        public string TdName { get; set; }
        public Nullable<int> PoID { get; set; }
        public string PoName { get; set; }
        public Nullable<int> OcID { get; set; }
        public string OcName { get; set; }
        public Nullable<System.DateTime> UfWeddingDate { get; set; }
        public Nullable<System.DateTime> DivorceDate { get; set; }
        public Nullable<bool> UfStudyStatus { get; set; }
        public string UfStudyStatusName { get; set; }
        public Nullable<int> RecID { get; set; }
        public string RecName { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public string MaritalName { get; set; }
    }
}
