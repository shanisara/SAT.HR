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
    
    public partial class sp_Leave_Balance_User_Result
    {
        public int LevID { get; set; }
        public int LevYear { get; set; }
        public string LevName { get; set; }
        public Nullable<decimal> LevMax { get; set; }
        public Nullable<decimal> LevStandard { get; set; }
        public Nullable<decimal> LevUsed { get; set; }
        public Nullable<decimal> LevBalance { get; set; }
    }
}