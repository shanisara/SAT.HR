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

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public List<RoleUserViewModel> ListRoleUser { get; set; }
        
    }

    public class RoleUserViewModel
    {
        public int RowNumber { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleDesc { get; set; }

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

        public string RoleDesc { get; set; }

        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Icon { get; set; }

        public int? ParentID { get; set; }

        public string MenuType { get; set; }

        public int? Sequence { get; set; }

        public int R_View { get; set; }

        public int R_Add { get; set; }

        public int R_Edit { get; set; }

        public int R_Delete { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }

        public List<RoleMenuViewModel> ListRoleMenu { get; set; }

        public List<RoleMenuViewModel> ListRoleMenuTab { get; set; }

        public List<RoleMenuViewModel> ListRoleMenuReport { get; set; }
    }

    public class UserRoleMenuViewModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Icon { get; set; }

        public int? ParentID { get; set; }

        public string MenuType { get; set; }

        public List<MenuViewModel> ListMenu { get; set; }

}
}
