using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class PositionRateRepository
    {
        public PositionRateViewModel GetByID(int? id)
        {
            PositionRateViewModel model = new PositionRateViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_Man_Power.Where(x => x.MpID == id).FirstOrDefault();
                    if (data != null)
                    {
                        model.MpID = data.MpID;
                        model.MpCode = data.TypeID == 1 ? data.MpID.ToString().PadLeft(3, '0') : data.MpID.ToString().PadLeft(4, '0');
                        model.DivID = data.DivID;
                        model.DepID = data.DepID;
                        model.SecID = data.SecID;
                        model.PoID = data.PoID;
                        model.MpMan = data.MpMan;
                        model.UserID = data.UserID;
                        model.FullNameTh = data.TiShortName + data.FullNameTh;
                        model.EduID = data.EduID;
                        model.TypeID = data.TypeID;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public ResponseData AddByEntity(PositionRateViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    int maxID = db.vw_Man_Power.Where(m => m.TypeID == data.TypeID).Max(m => m.MpID);

                    tb_Man_Power model = new tb_Man_Power();
                    model.MpCode = maxID;
                    model.TypeID = data.TypeID;
                    model.DivID = data.DivID;
                    model.DepID = data.DepID;
                    model.SecID = data.SecID;
                    model.PoID = data.PoID;
                    model.MpMan = data.MpMan;
                    model.UserID = data.UserID;
                    model.EduID = data.EduID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Man_Power.Add(model);
                    db.SaveChanges();
                    result.ID = model.MpID;
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(PositionRateViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Man_Power.Single(x => x.MpID == newdata.MpID);
                    model.DivID = newdata.DivID;
                    model.DepID = newdata.DepID;
                    model.SecID = newdata.SecID;
                    model.PoID = newdata.PoID;
                    model.MpMan = newdata.MpMan;
                    model.UserID = newdata.UserID;
                    model.EduID = newdata.EduID;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public List<PositionRateViewModel> GetPositionRate(int? type)
        {
            using (SATEntities db = new SATEntities())
            {
                List<PositionRateViewModel> list = new List<PositionRateViewModel>();

                var position = db.vw_Man_Power.Where(m => m.TypeID == type && !string.IsNullOrEmpty(m.PoName))
                   .GroupBy(item => item.PoID, (key, group) => new { PoID = key, MpID = group.FirstOrDefault().MpID, PoName = group.FirstOrDefault().PoName })
                   .OrderBy(o => o.MpID)
                   .ToList();

                foreach (var item in position)
                {
                    PositionRateViewModel model = new PositionRateViewModel();
                    model.MpID = item.MpID;
                    model.MpCode = type == 1 ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
                    model.PoName = item.PoName;
                    list.Add(model);
                }

                return list;
            }
        }

        public List<PositionRateViewModel> GetDivisionManPower(int? type)
        {
            List<PositionRateViewModel> list = new List<PositionRateViewModel>();
            using (SATEntities db = new SATEntities())
            {
                var deivision = db.vw_Man_Power.Where(m => m.TypeID == type && m.DivID != null)
                                 .GroupBy(item => item.DivID, (key, group) => new
                                 {
                                     DivID = key,
                                     DivName = group.FirstOrDefault().DivName,
                                     MpID = group.FirstOrDefault().MpID
                                 }).OrderBy(o => o.DivName).ToList();

                foreach (var item in deivision)
                {
                    PositionRateViewModel model = new PositionRateViewModel();
                    model.MpID = item.MpID;
                    model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
                    model.DivID = item.DivID;
                    model.DivName = item.DivName;
                    list.Add(model);
                }
            }
            return list;
        }

        public List<PositionRateViewModel> GetDepartmentManPower(int? type, int? divid)
        {
            List<PositionRateViewModel> list = new List<PositionRateViewModel>();
            using (SATEntities db = new SATEntities())
            {
                var department = db.vw_Man_Power.Where(m => m.TypeID == type && m.DivID == divid && !string.IsNullOrEmpty(m.DepName))
                                 .GroupBy(item => item.DepID, (key, group) => new
                                 {
                                     DepID = key,
                                     DepName = group.FirstOrDefault().DepName,
                                     MpID = group.FirstOrDefault().MpID,
                                     DivID = group.FirstOrDefault().DivID
                                 }).OrderBy(o => o.DepName).ToList();

                foreach (var item in department)
                {
                    PositionRateViewModel model = new PositionRateViewModel();
                    model.MpID = item.MpID;
                    model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
                    model.DepID = item.DepID;
                    model.DepName = item.DepName;
                    model.DivID = item.DivID;
                    list.Add(model);
                }
            }
            return list;
        }

        public List<PositionRateViewModel> GetSectionManPower(int? type, int? divid, int? depid)
        {
            List<PositionRateViewModel> list = new List<PositionRateViewModel>();
            using (SATEntities db = new SATEntities())
            {
                var section = db.vw_Man_Power.Where(m => m.TypeID == type && m.DivID == divid && m.DepID == depid && !string.IsNullOrEmpty(m.SecName))
                               .GroupBy(item => item.SecID, (key, group) => new
                               {
                                   SecID = key,
                                   SecName = group.FirstOrDefault().SecName,
                                   MpID = group.FirstOrDefault().MpID,
                                   DivID = group.FirstOrDefault().DivID,
                                   DepID = group.FirstOrDefault().DepID
                               }).OrderBy(o => o.SecName).ToList();

                foreach (var item in section)
                {
                    PositionRateViewModel model = new PositionRateViewModel();
                    model.MpID = item.MpID;
                    model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
                    model.SecID = item.SecID;
                    model.SecName = item.SecName;
                    model.DivID = item.DivID;
                    model.DepID = item.DepID;
                    list.Add(model);
                }
            }
            return list;
        }

        public List<PositionRateViewModel> GetPositionManPower(int? type, int? divid, int? depid, int? secid)
        {
            List<PositionRateViewModel> list = new List<PositionRateViewModel>();
            using (SATEntities db = new SATEntities())
            {
                var position = db.vw_Man_Power.Where(m => m.TypeID == type && m.DivID == divid && m.DepID == depid && m.SecID == secid && !string.IsNullOrEmpty(m.PoName))
                                .GroupBy(item => item.PoID, (key, group) => new
                                {
                                    PoID = key,
                                    PoName = group.FirstOrDefault().PoName,
                                    MpID = group.FirstOrDefault().MpID,
                                    DivID = group.FirstOrDefault().DivID,
                                    DepID = group.FirstOrDefault().DepID,
                                    SecID = group.FirstOrDefault().SecID
                                }).OrderBy(o => o.PoName).ToList();

                foreach (var item in position)
                {
                    PositionRateViewModel model = new PositionRateViewModel();
                    model.MpID = item.MpID;
                    model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
                    model.PoID = item.PoID;
                    model.PoName = item.PoName;
                    model.DivID = item.DivID;
                    model.DepID = item.DepID;
                    model.SecID = item.SecID;
                    list.Add(model);
                }
            }
            return list;
        }


        #region JSTreeViewModel

        public JSTreeViewModel GetTreeAll(int usertype)
        {
            var model = new JSTreeViewModel()
            {
                id = "0",
                text = "การกีฬาแห่งประเทศไทย",
                state = new JSTreeState() { opened = true },
                icon = SysConfig.ApplicationRoot + "Content/assets/img/home.png",
                children = GetDivision(usertype)
            };
            return model;
        }

        public List<JSTreeViewModel> GetDivision(int usertype)
        {
            var items = new List<JSTreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var deivision = GetDivisionManPower(usertype);
                foreach (var item in deivision)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == item.DivID && !string.IsNullOrEmpty(m.DepName))
                        .GroupBy(g => g.DepID).Select(group => new { DepID = group.Key }).Count();

                    var model = new JSTreeViewModel()
                    {
                        id = "Div" + item.DivID.ToString(),
                        text = item.DivName + " (" + countChild + ")",
                        state = new JSTreeState() { opened = true },
                        icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif",
                        node_type = "div",
                        children = GetDepartmentByDiv(usertype, (int)item.DivID),
                    };
                    items.Add(model);
                }
            }
            return items;
        }

        public List<JSTreeViewModel> GetDepartmentByDiv(int usertype, int divid)
        {
            var items = new List<JSTreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var department = GetDepartmentManPower(usertype, divid);

                foreach (var item in department)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DepID == item.DepID && m.DivID == divid && !string.IsNullOrEmpty(m.SecName))
                        .GroupBy(g => g.SecID).Select(group => new { SecID = group.Key }).Count();

                    var model = new JSTreeViewModel()
                    {
                        id = "Dep" + item.DepID.ToString(),
                        text = item.DepName + " (" + countChild + ")",
                        state = new JSTreeState() { opened = true },
                        icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif",
                        node_type = "dep",
                        children = GetSectionByDep(usertype, item.DivID, item.DepID),
                    };
                    items.Add(model);
                }
            }

            return items;
        }

        public List<JSTreeViewModel> GetSectionByDep(int usertype, int? divid, int? depid)
        {
            var items = new List<JSTreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var section = GetSectionManPower(usertype, divid, depid);

                foreach (var item in section)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.SecID == item.SecID && !string.IsNullOrEmpty(m.PoName))
                        .GroupBy(g => g.PoID).Select(group => new { PoID = group.Key }).Count();

                    var model = new JSTreeViewModel()
                    {
                        id = "Sec" + item.SecID.ToString(),
                        text = item.SecName + " (" + countChild + ")",
                        state = new JSTreeState() { opened = true },
                        icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif",
                        node_type = "sec",
                        children = GetPositionBySec(usertype, item.DivID, item.DepID, item.SecID),
                    };
                    items.Add(model);
                }
            }

            return items;
        }

        public List<JSTreeViewModel> GetPositionBySec(int usertype, int? divid, int? depid, int? secid)
        {
            var items = new List<JSTreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var position = GetPositionManPower(usertype, divid, depid, secid);

                foreach (var item in position)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == item.PoID
                            && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
                        .GroupBy(g => g.UserID).Select(group => new { UserID = group.Key }).Count();

                    var model = new JSTreeViewModel()
                    {
                        id = "Pos" + item.PoID.ToString(),
                        text = item.PoName + " (" + countChild + ")",
                        state = new JSTreeState() { opened = true },
                        icon = SysConfig.ApplicationRoot + "Content/assets/img/flag_orange.gif",
                        node_type = "pos",
                        children = GetUserManPower(usertype, item.DivID, item.DepID, item.SecID, item.PoID),
                    };
                    items.Add(model);
                }
            }

            return items;
        }

        public List<JSTreeViewModel> GetUserManPower(int? type, int? divid, int? depid, int? secid, int? poid)
        {
            var items = new List<JSTreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var user = db.vw_Man_Power.Where(m => m.TypeID == type && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == poid
                              && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
                              .GroupBy(item => item.UserID, (key, group) => new
                              {
                                  UserID = key,
                                  TiShortName = group.FirstOrDefault().TiShortName,
                                  FullNameTh = group.FirstOrDefault().FullNameTh,
                                  MpID = group.FirstOrDefault().MpID,
                                  DivID = group.FirstOrDefault().DivID,
                                  DepID = group.FirstOrDefault().DepID,
                                  SecID = group.FirstOrDefault().SecID,
                                  PoID = group.FirstOrDefault().PoID,
                              }).OrderBy(o => o.MpID).ToList();

                foreach (var item in user)
                {
                    var model = new JSTreeViewModel()
                    {
                        id = item.MpID.ToString(),
                        icon = SysConfig.ApplicationRoot + "Content/assets/img/user2.png",
                        node_type = "user",
                        text = "(" + item.MpID.ToString().PadLeft(4, '0') + ") " + item.TiShortName + item.FullNameTh,
                    };
                    items.Add(model);
                }
            }
            return items;
        }

        #endregion


        #region TreeViewModel

        public List<TreeViewModel> GetTree(int usertype)
        {
            var items = GetTreeDivision(usertype);
            return items;
        }

        public List<TreeViewModel> GetTree(string parenttype, int usertype, int? divid, int? depid, int? secid, int? poid)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                switch (parenttype)
                {
                    case "div":
                        items = GetDepartment(divid, usertype); break;
                    case "dep":
                        items = GetTreeSection(divid, depid, usertype); break;
                    case "sec":
                        items = GetTreePosition(divid, depid, secid, usertype); break;
                    case "pos":
                        items = GetTreeUser(divid, depid, secid, poid, usertype); break;
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreeDivision(int usertype)
        {
            var items = new List<TreeViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {

                    var deivision = GetDivisionManPower(usertype);

                    foreach (var item in deivision)
                    {
                        var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == item.DivID && !string.IsNullOrEmpty(m.DepName))
                            .GroupBy(g => g.DepID).Select(group => new { DepID = group.Key }).Count();

                        var model = new TreeViewModel();
                        model.id = item.DivID.ToString();
                        model.text = item.DivName + " (" + countChild + ")";
                        model.node_type = "div";
                        model.children = (countChild > 0) ? true : false;
                        items.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return items;
        }

        public List<TreeViewModel> GetDepartment(int? divid, int usertype)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var department = GetDepartmentManPower(usertype, divid);

                foreach (var item in department)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DepID == item.DepID && m.DivID == divid && !string.IsNullOrEmpty(m.SecName))
                        .GroupBy(g => g.SecID).Select(group => new { SecID = group.Key }).Count();

                    var model = new TreeViewModel();
                    model.id = item.DepID.ToString();
                    model.text = item.DepName + " (" + countChild + ")";
                    model.children = (countChild > 0) ? true : false;
                    model.node_type = "dep";
                    items.Add(model);
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreeSection(int? divid, int? depid, int usertype)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var section = GetSectionManPower(usertype, divid, depid);

                foreach (var item in section)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.SecID == item.SecID && !string.IsNullOrEmpty(m.PoName))
                        .GroupBy(g => g.PoID).Select(group => new { PoID = group.Key }).Count();

                    var model = new TreeViewModel();
                    model.id = item.SecID.ToString();
                    model.text = item.SecName + " (" + countChild + ")";
                    model.children = (countChild > 0) ? true : false;
                    model.node_type = "sec";
                    items.Add(model);
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreePosition(int? divid, int? depid, int? secid, int usertype)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var position = GetPositionManPower(usertype, divid, depid, secid);

                foreach (var item in position)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == item.PoID
                            && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
                        .GroupBy(g => g.UserID).Select(group => new { UserID = group.Key }).Count();

                    var model = new TreeViewModel();
                    model.id = item.PoID.ToString();
                    model.text = item.PoName + " (" + countChild + ")";
                    model.children = (countChild > 0) ? true : false;
                    model.node_type = "pos";
                    items.Add(model);
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreeUser(int? divid, int? depid, int? secid, int? poid, int usertype)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var user = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == poid
                              && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
                              .GroupBy(item => item.UserID, (key, group) => new
                              {
                                  UserID = key,
                                  TiShortName = group.FirstOrDefault().TiShortName,
                                  FullNameTh = group.FirstOrDefault().FullNameTh,
                                  MpID = group.FirstOrDefault().MpID,
                                  DivID = group.FirstOrDefault().DivID,
                                  DepID = group.FirstOrDefault().DepID,
                                  SecID = group.FirstOrDefault().SecID,
                                  PoID = group.FirstOrDefault().PoID,
                              }).ToList();

                foreach (var item in user)
                {
                    var model = new TreeViewModel();
                    model.id = item.UserID.ToString();
                    model.text = "(" + item.MpID.ToString().PadLeft(4, '0') + ") " + item.TiShortName + item.FullNameTh; // + " [" + item.MpCode + "]";
                    model.children = false;
                    model.node_type = "user";
                    model.node_mpid = item.MpID;
                    items.Add(model);
                }
            }

            return items;
        }

        #endregion 
    }
}



