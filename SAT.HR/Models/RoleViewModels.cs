using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class RoleViewModel
    {
        public int RowNumber { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleDesc { get; set; }

        public bool? RoleStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class RoleUserViewModel
    {
        public int RowNumber { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }

    public class RoleMenuViewModel
    {
        public int RowNumber { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }
}
