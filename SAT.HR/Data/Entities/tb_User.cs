
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
    
public partial class tb_User
{

    public int UserID { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string FullName { get; set; }

    public string SurName { get; set; }

    public Nullable<int> GroupID { get; set; }

    public Nullable<int> SexID { get; set; }

    public Nullable<System.DateTime> CreateDate { get; set; }

    public string CreateBy { get; set; }

    public Nullable<System.DateTime> ModifyDate { get; set; }

    public string ModifyBy { get; set; }



    public virtual tb_GroupRole tb_GroupRole { get; set; }

    public virtual tb_Sex tb_Sex { get; set; }

}

}
