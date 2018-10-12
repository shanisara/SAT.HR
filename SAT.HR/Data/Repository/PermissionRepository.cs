using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class PermissionRepository
    {
        #region Role

        public List<RoleViewModel> GetRoleAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Role.ToList();

                var list = data.Select(s => new RoleViewModel()
                {
                    RoleID = s.RoleID,
                    RoleName = s.RoleName,
                    RoleDesc = s.RoleDesc,
                    RoleStatus = s.RoleStatus,
                })
                .OrderBy(x => x.RoleName).ToList();

                return list;
            }
        }

        public RoleViewModel GetRoleByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Role.Where(x => x.RoleID == id).FirstOrDefault();
                RoleViewModel model = new Models.RoleViewModel();
                model.RoleID = data.RoleID;
                model.RoleName = data.RoleName;
                model.RoleDesc = data.RoleDesc;
                model.RoleStatus = data.RoleStatus;
                return model;
            }
        }

        public ResponseData AddRoleByEntity(RoleViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Role model = new tb_Role();
                    model.RoleID = data.RoleID;
                    model.RoleName = data.RoleName;
                    data.RoleDesc = data.RoleDesc;
                    model.RoleStatus = data.RoleStatus;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Role.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateRoleByEntity(RoleViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Role.Single(x => x.RoleID == newdata.RoleID);
                    data.RoleName = newdata.RoleName;
                    data.RoleDesc = newdata.RoleDesc;
                    data.RoleStatus = newdata.RoleStatus;
                    data.ModifyBy = newdata.ModifyBy;
                    data.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData RemoveRoleByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_Role.SingleOrDefault(c => c.RoleID == id);
                    if (obj != null)
                    {
                        db.tb_Role.Remove(obj);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        #endregion

        #region  User Role

        public RoleViewModel RoleUser(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                RoleViewModel model = new RoleViewModel();

                var data = db.vw_RoleUser.Where(x => x.RoleID == roleid).Select(s => new RoleUserViewModel()
                {
                    RoleID = s.RoleID,
                    RoleName = s.RoleName,
                    RoleDesc = s.RoleDesc,
                    UserID = (int)s.UserID,
                    UserName = s.UserName,
                    FullName = s.FirstName + " " + s.LastName,
                    DivID = (int)s.DivID,
                    DivName = s.DivName,
                    DepID = (int)s.DepID,
                    DepName = s.DepName,
                    SecID = (int)s.SecID,
                    SecName = s.SecName,
                    PoID = (int)s.PoID,
                    PoName = s.PoName
                }).OrderBy(x => x.UserName).ToList();

                if (data.Count > 0)
                {
                    model.RoleID = data[0].RoleID;
                    model.RoleName = data[0].RoleName;
                    model.RoleDesc = data[0].RoleDesc;
                }
                model.ListUserRole = data;

                return model;
            }
        }

        public ResponseData SaveRoleUser(int roleid, string users)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    if (!string.IsNullOrEmpty(users))
                    {
                        string[] user = users.Split(',');
                        foreach (var userid in user)
                        {
                            tb_RoleUser model = new tb_RoleUser();
                            model.RoleID = roleid;
                            model.UserID = Convert.ToInt32(userid);
                            model.CreateBy = UtilityService.User.UserID;
                            model.CreateDate = DateTime.Now;
                            model.ModifyBy = UtilityService.User.UserID;
                            model.ModifyDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData RemoveRoleUser(int roleid, int userid)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_RoleUser.SingleOrDefault(m => m.RoleID == roleid && m.UserID == userid);
                    if (obj != null)
                    {
                        db.tb_RoleUser.Remove(obj);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }


        #endregion

        #region  Menu Role

        //public RoleViewModel RoleMenu(int roleid)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        RoleViewModel model = new RoleViewModel();
        //        var data = db.vw_RoleMenu.Where(x => x.RoleID == roleid).FirstOrDefault();
        //        model.RoleID = data.RoleID;
        //        model.RoleName = data.RoleName;
        //        model.RoleDesc = data.RoleDesc;
        //        //model.ListRoleMenu = MenuByRole(roleid);
        //        //model.ListRoleMenuTab = MenuTabByRole(roleid);
        //        //model.ListRoleMenuReport = MenuReportByRole(roleid);
        //        return model;
        //    }
        //}

        public ResponseData SaveRoleMenu(List<RoleMenuViewModel> model)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    if (model != null)
                    {
                        foreach (var item in model)
                        {
                            if (item.MenuID == 7)
                            {
                                string xxxx = "xx";
                            }

                            if (item.MenuType == "M" || (item.ParentID != 0 && (item.MenuType == "T" || item.MenuType == "R")))
                            {
                                var data = db.tb_RoleMenu.Where(m => m.RoleID == item.RoleID && m.MenuID == item.MenuID).FirstOrDefault();
                                if (data != null)
                                {
                                    data.R_View = item.R_View;
                                    data.R_Add = item.R_Add;
                                    data.R_Edit = item.R_Edit;
                                    data.R_Delete = item.R_Delete;
                                    data.ModifyBy = UtilityService.User.UserID;
                                    data.ModifyDate = DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    #region

                                    tb_RoleMenu obj = new tb_RoleMenu();
                                    obj.RoleID = item.RoleID;
                                    obj.MenuID = item.MenuID;
                                    obj.R_View = item.R_View;
                                    obj.R_Add = item.R_Add;
                                    obj.R_Edit = item.R_Edit;
                                    obj.R_Delete = item.R_Delete;
                                    obj.CreateBy = UtilityService.User.UserID;
                                    obj.CreateDate = DateTime.Now;
                                    obj.ModifyBy = UtilityService.User.UserID;
                                    obj.ModifyDate = DateTime.Now;
                                    db.tb_RoleMenu.Add(obj);
                                    db.SaveChanges();

                                    #endregion
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public RoleMenuViewModel MenuRole(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                RoleMenuViewModel model = new RoleMenuViewModel();

                var role = GetRoleByID(roleid);
                model.RoleID = role.RoleID;
                model.RoleName = role.RoleName;
                model.RoleDesc = role.RoleDesc;
                model.ListRoleMenu = MenuByRole(roleid);
                model.ListRoleMenuTab = MenuTabByRole(roleid);
                model.ListRoleMenuReport = MenuReportByRole(roleid);
                return model;
            }
        }

        public List<RoleMenuViewModel> MenuByRole(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_RoleMenu.Where(m => m.RoleID == roleid).Select(s => new RoleMenuViewModel()
                {
                    RoleID = s.RoleID,
                    MenuID = (int)s.MenuID,
                    MenuName = s.MenuName,
                    ParentID = s.ParentID,
                    R_View = (bool)s.R_View,
                    R_Add = (bool)s.R_Add,
                    R_Edit = (bool)s.R_Edit,
                    R_Delete = (bool)s.R_Delete
                }).ToList();

                if (data.Count == 0)
                {
                    data = db.tb_Menu.Where(m => m.MenuType == "M").Select(s => new RoleMenuViewModel()
                    {
                        RoleID = roleid,
                        MenuID = (int)s.MenuID,
                        MenuName = s.MenuName,
                        ParentID = s.ParentID,
                        R_View = false,
                        R_Add = false,
                        R_Edit = false,
                        R_Delete = false
                    }).ToList();
                }

                return data;
            }
        }

        public List<RoleMenuViewModel> MenuTabByRole(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.sp_Menu_GetByRole(roleid, "T").Select(s => new RoleMenuViewModel()
                {
                    RoleID = s.RoleID,
                    MenuID = (int)s.MenuID,
                    MenuName = s.MenuName,
                    ParentID = s.ParentID,
                    R_View = s.R_View ?? false,
                    R_Add = s.R_Add ?? false,
                    R_Edit = s.R_Edit ?? false,
                    R_Delete = s.R_Delete ?? false,
                }).ToList();

                return data;
            }
        }

        public List<RoleMenuViewModel> MenuReportByRole(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.sp_Menu_GetByRole(roleid, "R").Select(s => new RoleMenuViewModel()
                {
                    RoleID = s.RoleID,
                    MenuID = (int)s.MenuID,
                    MenuName = s.MenuName,
                    ParentID = s.ParentID,
                    R_View = s.R_View ?? false,
                    R_Add = s.R_Add ?? false,
                    R_Edit = s.R_Edit ?? false,
                    R_Delete = s.R_Delete ?? false,
                }).ToList();

                return data;
            }
        }

        #endregion
    }
}