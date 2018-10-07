using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

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

        #region  RoleUser

        public RoleViewModel GetRoleUser(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                RoleViewModel model = new RoleViewModel();
                var data = db.tb_Role.Where(x => x.RoleID == id).FirstOrDefault();
                model.RoleID = data.RoleID;
                model.RoleName = data.RoleName;
                model.RoleDesc = data.RoleDesc;
                model.RoleStatus = data.RoleStatus;
                model.ListRoleUser = GetUserByRole(id);
                return model;
            }
        }

        public List<RoleUserViewModel> GetUserByRole(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_RoleUser.Where(x => x.RoleID == roleid).Select(s => new RoleUserViewModel()
                {
                    RoleID = s.RoleID,
                    RoleName = s.RoleName,
                    UserID = s.UserID,
                    UserName = s.UserName,
                }).OrderBy(x => x.UserName).ToList();
                return data;
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
                            model.CreateBy = string.Empty;
                            model.CreateDate = DateTime.Now;
                            model.ModifyBy = string.Empty;
                            model.ModifyDate = DateTime.Now;
                            db.tb_RoleUser.Add(model);
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

        #region  RoleMenu

        public List<RoleMenuViewModel> GetMenuByRole(int roleid)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_RoleMenu.Where(x => x.RoleID == roleid).Select(s => new RoleMenuViewModel()
                {
                    RoleID = s.RoleID,
                    RoleName = s.RoleName,
                    MenuID = (int)s.MenuID,
                    MenuName = s.MenuName,
                }).OrderBy(x => x.MenuName).ToList();
                return data;
            }
        }

        #endregion

    }
}