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
    
    public partial class tb_Man_Power
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Man_Power()
        {
            this.tb_Move_Man_Power_Detail = new HashSet<tb_Move_Man_Power_Detail>();
            this.tb_Move_Man_Power_Detail1 = new HashSet<tb_Move_Man_Power_Detail>();
            this.tb_Move_Man_Power_Detail2 = new HashSet<tb_Move_Man_Power_Detail>();
        }
    
        public int MpID { get; set; }
        public Nullable<int> MpCode { get; set; }
        public string MpMan { get; set; }
        public Nullable<int> DivID { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<int> SecID { get; set; }
        public Nullable<int> PoID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> EduID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    
        public virtual tb_Department tb_Department { get; set; }
        public virtual tb_Division tb_Division { get; set; }
        public virtual tb_Education tb_Education { get; set; }
        public virtual tb_Section tb_Section { get; set; }
        public virtual tb_User tb_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Move_Man_Power_Detail> tb_Move_Man_Power_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Move_Man_Power_Detail> tb_Move_Man_Power_Detail1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Move_Man_Power_Detail> tb_Move_Man_Power_Detail2 { get; set; }
    }
}
