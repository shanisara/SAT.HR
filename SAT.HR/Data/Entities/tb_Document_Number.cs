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
    
    public partial class tb_Document_Number
    {
        public int ObjectID { get; set; }
        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public Nullable<int> NumLength { get; set; }
        public Nullable<int> CurrentNum { get; set; }
        public Nullable<bool> ZeroContain { get; set; }
        public Nullable<bool> ResetMonth { get; set; }
        public Nullable<bool> ResetYear { get; set; }
        public Nullable<int> LastMonth { get; set; }
        public Nullable<int> LastYear { get; set; }
    }
}