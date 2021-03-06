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
    
    public partial class tb_Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Department()
        {
            this.tb_Man_Power = new HashSet<tb_Man_Power>();
            this.tb_User_Position = new HashSet<tb_User_Position>();
        }
    
        public int DepID { get; set; }
        public Nullable<int> DivID { get; set; }
        public string DepName { get; set; }
        public Nullable<bool> DepStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    
        public virtual tb_Division tb_Division { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Man_Power> tb_Man_Power { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_User_Position> tb_User_Position { get; set; }
    }
}
