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
    
    public partial class tb_Trans_Step_Route
    {
        public int StepID { get; set; }
        public Nullable<int> FormMasterID { get; set; }
        public Nullable<int> FormHeaderID { get; set; }
        public Nullable<int> FormStepID { get; set; }
        public Nullable<int> NextStepID { get; set; }
    }
}
