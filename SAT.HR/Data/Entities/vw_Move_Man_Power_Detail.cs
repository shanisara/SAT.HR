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
    
    public partial class vw_Move_Man_Power_Detail
    {
        public int MopID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPoCode { get; set; }
        public string UserPoName { get; set; }
        public Nullable<int> MovPoID { get; set; }
        public string MovPoCode { get; set; }
        public string MovPoName { get; set; }
        public Nullable<int> AgentPoTID { get; set; }
        public string AgentPoTName { get; set; }
        public Nullable<int> AgentPoID { get; set; }
        public string AgentPoCode { get; set; }
        public string AgentPoName { get; set; }
        public string MovRemark { get; set; }
    }
}