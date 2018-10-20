using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class MenuRepository
    {
        public UserRoleMenuViewModel MenuByUser(int userid)
        {
            using (SATEntities db = new SATEntities())
            {
                UserRoleMenuViewModel model = new UserRoleMenuViewModel();

                model.UserID = userid;
                model.FullName = UtilityService.User.FullNameTh;
                model.Avatar = !string.IsNullOrEmpty(UtilityService.User.Avatar) ? UtilityService.User.Avatar : "avatar.png";

                var menu = db.sp_Menu_GetByUser(model.UserID).Select(s => new MenuViewModel()
                {
                    MenuID = (int)s.MenuID,
                    MenuName = s.MenuName,
                    ControllerName = s.ControllerName,
                    ActionName = s.ActionName,
                    Icon = s.Icon,
                    ParentID = s.ParentID,
                    MenuType = s.MenuType
                }).ToList();

                model.ListMenu = menu;

                return model;
            }
        }

        public RoleMenuViewModel MenuReportByRole(int menuid)
        {
            using (SATEntities db = new SATEntities())
            {
                RoleMenuViewModel data = new RoleMenuViewModel();
                int userid = UtilityService.User.UserID;

                var menu = db.sp_Menu_Report_GetByUser(userid, menuid).Select(s => new RoleMenuViewModel()
                {
                    MenuID = (int)s.MenuID,
                    MenuName = s.MenuName,
                    ControllerName = s.ControllerName,
                    ActionName = s.ActionName,
                    Icon = s.Icon,
                }).ToList();

                data.ListRoleMenuReport = menu;

                return data;
            }
        }
    }
}