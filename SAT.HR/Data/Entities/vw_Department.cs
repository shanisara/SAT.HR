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
    
    public partial class vw_Department
    {
        public int DepID { get; set; }
        public Nullable<int> DivID { get; set; }
        public string DepName { get; set; }
        public string DivName { get; set; }
        public Nullable<bool> DivStatus { get; set; }
        public Nullable<bool> DepStatus { get; set; }
    }
}
