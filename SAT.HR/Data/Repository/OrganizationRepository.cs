using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class OrganizationRepository
    {
        public List<TreeViewModel> GetTree(int usertype)
        {
            var items = new List<TreeViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    List<OrganizationViewModel> list = new List<OrganizationViewModel>();
                    var data = db.vw_Organization.Where(m => m.TypeID == usertype && m.ParentID == null).ToList();

                    foreach (var item in data)
                    {
                        var model = new TreeViewModel();
                        model.id = item.MpID.ToString();
                        model.text = item.DivName;
                        model.children = true;
                        model.state = new TreeStateViewModel() { opened = true };
                        model.icon = SysConfig.ApplicationRoot + "Content/assets/img/home.png";
                        items.Add(model);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TreeViewModel> GetChildren(int usertype, string id)
        {
            var items = new List<TreeViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    List<OrganizationViewModel> list = new List<OrganizationViewModel>();

                    int parentid = Convert.ToInt32(id);
                    var organization = db.vw_Organization.Where(m => m.TypeID == usertype && m.ParentID == parentid)/*.OrderBy(o => o.Seq)*/.ToList();

                    var data = new List<OrganizationViewModel>();
                    if (organization.Count > 0)
                    {
                        if (organization[0].DepLevel == 1 || organization[0].DepLevel == 2 || organization[0].DepLevel == 3)
                        {
                            data = organization
                                   .Select(s => new OrganizationViewModel
                                   {
                                       MpName = !string.IsNullOrEmpty(s.DepName) ? s.DepName : s.DivName,
                                       MpID = s.MpID,
                                       MpCode = s.MpCode,
                                       ParentID = s.ParentID,
                                       DivName = s.DivName,
                                       DepName = s.DepName,
                                       SecName = s.SecName,
                                   }).ToList();
                        }
                        else if (organization[0].DepLevel == 4)
                        {
                            var secname = organization[0].SecName;
                            if (!string.IsNullOrEmpty(secname))
                            {
                                data = organization.GroupBy(g => g.SecName)
                                        .Select(group => new OrganizationViewModel
                                        {
                                            MpName = group.Key,
                                            MpID = group.FirstOrDefault().MpID,
                                            MpCode = group.FirstOrDefault().MpCode,
                                            ParentID = group.FirstOrDefault().ParentID,
                                            DivName = group.FirstOrDefault().DivName,
                                            DepName = group.FirstOrDefault().DepName,
                                            SecName = group.FirstOrDefault().SecName,
                                        }).ToList();

                            }
                            else
                            {
                                data = organization.GroupBy(g => g.DepName)
                                         .Select(group => new OrganizationViewModel
                                         {
                                             MpName = group.Key,
                                             MpID = group.FirstOrDefault().MpID,
                                             MpCode = group.FirstOrDefault().MpCode,
                                             ParentID = group.FirstOrDefault().ParentID,
                                             DivName = group.FirstOrDefault().DivName,
                                             DepName = group.FirstOrDefault().DepName,
                                             SecName = group.FirstOrDefault().SecName,
                                         }).ToList();
                            }
                        }
                        else if (organization[0].DepLevel == 5)
                        {
                            data = organization.GroupBy(g => g.SecName)
                                    .Select(group => new OrganizationViewModel
                                    {
                                        MpName = group.Key,
                                        MpID = group.FirstOrDefault().MpID,
                                        MpCode = group.FirstOrDefault().MpCode,
                                        ParentID = group.FirstOrDefault().ParentID,
                                        DivName = group.FirstOrDefault().DivName,
                                        DepName = group.FirstOrDefault().DepName,
                                        SecName = group.FirstOrDefault().SecName,
                                    }).ToList();
                        }

                        var orgemp = db.vw_Man_Power.Where(m => m.MpID == parentid).ToList();
                        foreach (var org in orgemp)
                        {
                            var emp = new TreeViewModel();
                            emp.id = org.MpID.ToString() + "_" + org.DivID.ToString();
                            emp.text = " (" + org.MpCode + ") " + org.TiShortName + (string.IsNullOrEmpty(org.FullNameTh) ? "ตำแหน่งว่าง ✓" : org.FullNameTh) + " (" + org.PoName + ")";
                            emp.children = false;
                            emp.state = new TreeStateViewModel() { opened = true };
                            emp.icon = SysConfig.ApplicationRoot + "Content/assets/img/user2.png";
                            items.Add(emp);
                        }
                    }

                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            var model = new TreeViewModel();
                            model.id = item.MpID.ToString();
                            model.text = item.MpName;
                            model.children = true;
                            model.state = new TreeStateViewModel() { opened = true };
                            model.icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif";
                            //model.is_po = (countChild > 0) ? true : false;
                            items.Add(model);
                        }
                    }
                    else
                    {
                        var sec = db.vw_Man_Power.Where(m => m.MpID == parentid).FirstOrDefault();
                        var secid = sec.SecID;

                        var users = db.vw_Man_Power.Where(m => m.SecID == secid).ToList();
                        foreach (var item in users)
                        {
                            //var countEmp = db.vw_Man_Power.Where(m => m.MpID == item.MpID && m.UserID != null).Count();

                            var model = new TreeViewModel();
                            model.id = item.MpID.ToString() + "_" + item.PoID.ToString();
                            model.text = " (" + item.MpCode + ") " + item.TiShortName + (string.IsNullOrEmpty(item.FullNameTh) ? "ตำแหน่งว่าง ✓" : item.FullNameTh) + " (" + item.PoName + ")";
                            model.children = false;
                            model.state = new TreeStateViewModel() { opened = true };
                            model.icon = SysConfig.ApplicationRoot + "Content/assets/img/user2.png";
                            items.Add(model);
                        }
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ManPowerViewModel GetDetailByUser(int userid)
        {
            using (SATEntities db = new SATEntities())
            {
                ManPowerViewModel model = new ManPowerViewModel();
                var data = db.vw_Man_Power.Where(m => m.UserID == userid).FirstOrDefault();
                if (data != null)
                {
                    model.ManPower = data.DivName + (!string.IsNullOrEmpty(data.DepName) ? " / " : string.Empty) + data.DepName + (!string.IsNullOrEmpty(data.SecName) ? " / " : string.Empty) + data.SecName;
                    model.MpID = data.MpID;
                    model.Position = "(" + (data.TypeID.ToString() == "1" ? data.MpID.ToString().PadLeft(3, '0') : data.MpID.ToString().PadLeft(4, '0')) + ") " + data.PoName;
                    model.Level = data.SalaryLevel.ToString();
                    model.Step = data.SalaryStep.ToString();
                    model.Salary = data.Salary.ToString();
                }

                return model;
            }
        }

        public ManPowerViewModel GetDetailByMp(int mpid)
        {
            using (SATEntities db = new SATEntities())
            {
                ManPowerViewModel model = new ManPowerViewModel();
                var data = db.vw_Man_Power.Where(m => m.MpID == mpid).FirstOrDefault();
                if (data != null)
                {
                    model.ManPower = data.DivName + (!string.IsNullOrEmpty(data.DepName) ? " / " : string.Empty) + data.DepName + (!string.IsNullOrEmpty(data.SecName) ? " / " : string.Empty) + data.SecName;
                    model.FullNameTh = data.FullNameTh;
                }
                return model;
            }
        }


        //public List<TreeViewModel> GetPosition(int mpid)
        //{
        //    var items = new List<TreeViewModel>();
        //    try
        //    {
        //        using (SATEntities db = new SATEntities())
        //        {
        //            List<OrganizationViewModel> list = new List<OrganizationViewModel>();

        //            //var data = db.vw_Man_Power.Where(m => m.DepID == depid)
        //            //            .GroupBy(item => item.PoID, (key, group) => new
        //            //            {
        //            //                PoID = key,
        //            //                PoName = group.FirstOrDefault().PoName,
        //            //                MpID = group.FirstOrDefault().MpID,
        //            //                MpCode = group.FirstOrDefault().MpCode
        //            //            }).ToList();

        //            var data = db.vw_Man_Power.Where(m => m.MpID == mpid)/*.OrderBy(o => o.DepLevel)*/.ToList();

        //            foreach (var item in data)
        //            {
        //                //var countEmp = db.vw_Man_Power.Where(m => m.MpID == item.MpID && m.UserID != null).Count();

        //                var model = new TreeViewModel();
        //                model.id = item.MpID.ToString() + "_" + item.PoID.ToString();
        //                model.text = " (" + item.MpCode + ") " + item.TiShortName + (string.IsNullOrEmpty(item.FullNameTh) ? "ตำแหน่งว่าง ✓" : item.FullNameTh) + " (" + item.PoName + ")";
        //                model.children = false;
        //                model.state = new TreeStateViewModel() { opened = true };
        //                model.icon = SysConfig.ApplicationRoot + "Content/assets/img/user2.png";
        //                items.Add(model);
        //            }

        //            return items;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



        #region ManPowerViewModel (No use)

        //public List<PositionRateViewModel> GetPositionRate(int? type)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        List<PositionRateViewModel> list = new List<PositionRateViewModel>();

        //        var position = db.vw_Man_Power.Where(m => m.TypeID == type && !string.IsNullOrEmpty(m.PoName))
        //           .GroupBy(item => item.MpID, (key, group) => new { MpID = key, PoName = group.FirstOrDefault().PoName })
        //           .OrderBy(o => o.MpID)
        //           .ToList();

        //        foreach (var item in position)
        //        {
        //            PositionRateViewModel model = new PositionRateViewModel();
        //            model.MpID = (int)item.MpID;
        //            //model.MpCode = type == 1 ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
        //            model.PoName = item.PoName;
        //            list.Add(model);
        //        }

        //        return list;
        //    }
        //}

        //public List<PositionRateViewModel> GetDivisionManPower(int? type)
        //{
        //    List<PositionRateViewModel> list = new List<PositionRateViewModel>();
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var deivision = db.vw_Man_Power.Where(m => m.TypeID == type /*&& m.DivID != null*/)
        //                         //.GroupBy(item => item.DivID, (key, group) => new
        //                         //{
        //                         //    DivID = key,
        //                         //    DivName = group.FirstOrDefault().DivName,
        //                         //    MpID = group.FirstOrDefault().MpID,
        //                         //    DivSeq = group.FirstOrDefault().DivSeq
        //                         //}).OrderBy(o => o.DivSeq)
        //                         .ToList();

        //        foreach (var item in deivision)
        //        {
        //            PositionRateViewModel model = new PositionRateViewModel();
        //            model.MpID = item.MpID;
        //            //model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
        //            //model.DivID = item.DivID;
        //            //model.DivName = item.DivName;
        //            list.Add(model);
        //        }
        //    }
        //    return list;
        //}

        //public List<PositionRateViewModel> GetDepartmentManPower(int? type, int? divid)
        //{
        //    List<PositionRateViewModel> list = new List<PositionRateViewModel>();
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var department = db.vw_Man_Power.Where(m => m.TypeID == type /*&& m.DivID == divid && !string.IsNullOrEmpty(m.DepName)*/)
        //                         //.GroupBy(item => item.DepID, (key, group) => new
        //                         //{
        //                         //    DepID = key,
        //                         //    DepName = group.FirstOrDefault().DepName,
        //                         //    MpID = group.FirstOrDefault().MpID,
        //                         //    DivID = group.FirstOrDefault().DivID
        //                         //}).OrderBy(o => o.DepName)
        //                         .ToList();

        //        foreach (var item in department)
        //        {
        //            PositionRateViewModel model = new PositionRateViewModel();
        //            model.MpID = item.MpID;
        //            //model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
        //            //model.DepID = item.DepID;
        //            //model.DepName = item.DepName;
        //            //model.DivID = item.DivID;
        //            list.Add(model);
        //        }
        //    }
        //    return list;
        //}

        //public List<PositionRateViewModel> GetSectionManPower(int? type, int? divid, int? depid)
        //{
        //    List<PositionRateViewModel> list = new List<PositionRateViewModel>();
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var section = db.vw_Man_Power.Where(m => m.TypeID == type /*&& m.DivID == divid && m.DepID == depid && !string.IsNullOrEmpty(m.SecName)*/)
        //                       //.GroupBy(item => item.SecID, (key, group) => new
        //                       //{
        //                       //    SecID = key,
        //                       //    SecName = group.FirstOrDefault().SecName,
        //                       //    MpID = group.FirstOrDefault().MpID,
        //                       //    DivID = group.FirstOrDefault().DivID,
        //                       //    DepID = group.FirstOrDefault().DepID
        //                       //}).OrderBy(o => o.SecName)
        //                       .ToList();

        //        foreach (var item in section)
        //        {
        //            PositionRateViewModel model = new PositionRateViewModel();
        //            model.MpID = item.MpID;
        //            //model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
        //            //model.SecID = item.SecID;
        //            //model.SecName = item.SecName;
        //            //model.DivID = item.DivID;
        //            //model.DepID = item.DepID;
        //            list.Add(model);
        //        }
        //    }
        //    return list;
        //}

        //public List<PositionRateViewModel> GetPositionManPower(int? type, int? divid, int? depid, int? secid)
        //{
        //    List<PositionRateViewModel> list = new List<PositionRateViewModel>();
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var position = db.vw_Man_Power.Where(m => m.TypeID == type /*&& m.DivID == divid && m.DepID == depid && m.SecID == secid */&& !string.IsNullOrEmpty(m.PoName))
        //                        .GroupBy(item => item.PoID, (key, group) => new
        //                        {
        //                            PoID = key,
        //                            PoName = group.FirstOrDefault().PoName,
        //                            MpID = group.FirstOrDefault().MpID,
        //                            //DivID = group.FirstOrDefault().DivID,
        //                            //DepID = group.FirstOrDefault().DepID,
        //                            //SecID = group.FirstOrDefault().SecID
        //                        }).OrderBy(o => o.PoName)
        //                        .ToList();

        //        foreach (var item in position)
        //        {
        //            PositionRateViewModel model = new PositionRateViewModel();
        //            model.MpID = item.MpID;
        //            //model.MpCode = (type == 1) ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0');
        //            model.PoID = item.PoID;
        //            model.PoName = item.PoName;
        //            //model.DivID = item.DivID;
        //            //model.DepID = item.DepID;
        //            //model.SecID = item.SecID;
        //            list.Add(model);
        //        }
        //    }
        //    return list;
        //}
       
        #endregion

        #region TreeViewModel (No use)

        //public List<TreeViewModel> GetTree(int usertype)
        //{
        //    var items = GetTreeDivision(usertype);
        //    return items;
        //}

        //public List<TreeViewModel> GetTree(string parenttype, int usertype, string div, string dep, string sec, string po)
        //{
        //    var items = new List<TreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        int divid = div != "null" ? Convert.ToInt32(div.Split('_')[1]) : 0;
        //        int depid = dep != "null" ? Convert.ToInt32(dep.Split('_')[1]) : 0;
        //        int secid = sec != "null" ? Convert.ToInt32(sec.Split('_')[1]) : 0;
        //        int poid = po != "null" ? Convert.ToInt32(po.Split('_')[1]) : 0;

        //        switch (parenttype)
        //        {
        //            //case "div":
        //            //    items = GetDepartment(divid, usertype); break;
        //            //case "dep":
        //            //    items = GetTreeSection(divid, depid, usertype); break;
        //            //case "sec":
        //            //    items = GetTreePosition(divid, depid, secid, usertype); break;
        //            //case "pos":
        //            //    items = GetTreeUser(divid, depid, secid, poid, usertype); break;
        //        }
        //    }

        //    return items;
        //}

        //public List<TreeViewModel> GetTreeDivision(int usertype)
        //{
        //    var items = new List<TreeViewModel>();
        //    try
        //    {
        //        using (SATEntities db = new SATEntities())
        //        {

        //            var deivision = GetDivisionManPower(usertype);

        //            foreach (var item in deivision)
        //            {
        //                var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == item.DivID && !string.IsNullOrEmpty(m.DepName))
        //                                .GroupBy(g => g.DepID).Select(group => new { DepID = group.Key }).Count();

        //                var model = new TreeViewModel();
        //                model.id = item.MpID + "_" + item.DivID.ToString();
        //                model.text = item.DivName + " (" + countChild + ")";
        //                model.children = (countChild > 0) ? true : false;
        //                model.state = new TreeStateViewModel() { opened = true };
        //                model.icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif";
        //                model.node_type = "div";
        //                items.Add(model);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return items;
        //}

        //public List<TreeViewModel> GetDepartment(int? divid, int usertype)
        //{
        //    var items = new List<TreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var department = GetDepartmentManPower(usertype, divid);

        //        foreach (var item in department)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DepID == item.DepID && m.DivID == divid && !string.IsNullOrEmpty(m.SecName))
        //                .GroupBy(g => g.SecID).Select(group => new { SecID = group.Key }).Count();

        //            var model = new TreeViewModel();
        //            model.id = item.MpID + "_" + item.DepID.ToString();
        //            model.text = item.DepName + " (" + countChild + ")";
        //            model.children = (countChild > 0) ? true : false;
        //            model.state = new TreeStateViewModel() { opened = true };
        //            model.icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif";
        //            model.node_type = "dep";
        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}

        //public List<TreeViewModel> GetTreeSection(int? divid, int? depid, int usertype)
        //{
        //    var items = new List<TreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var section = GetSectionManPower(usertype, divid, depid);

        //        foreach (var item in section)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.SecID == item.SecID && !string.IsNullOrEmpty(m.PoName))
        //                .GroupBy(g => g.PoID).Select(group => new { PoID = group.Key }).Count();

        //            var model = new TreeViewModel();
        //            model.id = item.MpID + "_" + item.SecID.ToString();
        //            model.text = item.SecName + " (" + countChild + ")";
        //            model.children = (countChild > 0) ? true : false;
        //            model.state = new TreeStateViewModel() { opened = true };
        //            model.icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif";
        //            model.node_type = "sec";
        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}

        //public List<TreeViewModel> GetTreePosition(int? divid, int? depid, int? secid, int usertype)
        //{
        //    var items = new List<TreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var position = GetPositionManPower(usertype, divid, depid, secid);

        //        foreach (var item in position)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == item.PoID
        //                    && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
        //                .GroupBy(g => g.UserID).Select(group => new { UserID = group.Key }).Count();

        //            var model = new TreeViewModel();
        //            model.id = item.MpID + "_" + item.PoID.ToString();
        //            model.text = item.PoName + " (" + countChild + ")";
        //            model.children = (countChild > 0) ? true : false;
        //            model.state = new TreeStateViewModel() { opened = true };
        //            model.icon = SysConfig.ApplicationRoot + "Content/assets/img/flag_white.gif";
        //            model.node_type = "pos";
        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}

        //public List<TreeViewModel> GetTreeUser(int? divid, int? depid, int? secid, int? poid, int usertype)
        //{
        //    var items = new List<TreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var user = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == poid
        //                      && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
        //                      .GroupBy(item => item.UserID, (key, group) => new
        //                      {
        //                          UserID = key,
        //                          TiShortName = group.FirstOrDefault().TiShortName,
        //                          FullNameTh = group.FirstOrDefault().FullNameTh,
        //                          MpID = group.FirstOrDefault().MpID,
        //                          DivID = group.FirstOrDefault().DivID,
        //                          DepID = group.FirstOrDefault().DepID,
        //                          SecID = group.FirstOrDefault().SecID,
        //                          PoID = group.FirstOrDefault().PoID,
        //                      }).OrderBy(o => o.MpID).ToList();

        //        foreach (var item in user)
        //        {
        //            var model = new TreeViewModel();
        //            model.id = item.MpID.ToString();
        //            model.text = "(" + (usertype == 1 ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0')) + ") " + item.TiShortName + item.FullNameTh;
        //            model.children = false;
        //            model.node_mpid = item.MpID;
        //            model.icon = SysConfig.ApplicationRoot + "Content/assets/img/user2.png";
        //            model.node_type = "user";

        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}



        //public JSTreeViewModel GetTreeAll(int usertype)
        //{
        //    var model = new JSTreeViewModel()
        //    {
        //        id = "#",
        //        text = "การกีฬาแห่งประเทศไทย",
        //        state = new TreeStateViewModel() { opened = true },
        //        icon = SysConfig.ApplicationRoot + "Content/assets/img/home.png",
        //        children = GetDivision(usertype)
        //    };
        //    return model;
        //}

        //public List<JSTreeViewModel> GetDivision(int usertype)
        //{
        //    var items = new List<JSTreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var deivision = GetDivisionManPower(usertype);
        //        foreach (var item in deivision)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == item.DivID && !string.IsNullOrEmpty(m.DepName))
        //                .GroupBy(g => g.DepID).Select(group => new { DepID = group.Key }).Count();

        //            var model = new JSTreeViewModel()
        //            {
        //                id = "Div" + item.DivID.ToString(),
        //                text = item.DivName + " (" + countChild + ")",
        //                state = new TreeStateViewModel() { opened = true },
        //                icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif",
        //                node_type = "div",
        //                children = GetDepartmentByDiv(usertype, (int)item.DivID),
        //            };
        //            items.Add(model);
        //        }
        //    }
        //    return items;
        //}

        //public List<JSTreeViewModel> GetDepartmentByDiv(int usertype, int divid)
        //{
        //    var items = new List<JSTreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var department = GetDepartmentManPower(usertype, divid);

        //        foreach (var item in department)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DepID == item.DepID && m.DivID == divid && !string.IsNullOrEmpty(m.SecName))
        //                .GroupBy(g => g.SecID).Select(group => new { SecID = group.Key }).Count();

        //            var model = new JSTreeViewModel()
        //            {
        //                id = "Dep" + item.DepID.ToString(),
        //                text = item.DepName + " (" + countChild + ")",
        //                state = new TreeStateViewModel() { opened = true },
        //                icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif",
        //                node_type = "dep",
        //                children = GetSectionByDep(usertype, item.DivID, item.DepID),
        //            };
        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}

        //public List<JSTreeViewModel> GetSectionByDep(int usertype, int? divid, int? depid)
        //{
        //    var items = new List<JSTreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var section = GetSectionManPower(usertype, divid, depid);

        //        foreach (var item in section)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.SecID == item.SecID && !string.IsNullOrEmpty(m.PoName))
        //                .GroupBy(g => g.PoID).Select(group => new { PoID = group.Key }).Count();

        //            var model = new JSTreeViewModel()
        //            {
        //                id = "Sec" + item.SecID.ToString(),
        //                text = item.SecName + " (" + countChild + ")",
        //                state = new TreeStateViewModel() { opened = true },
        //                icon = SysConfig.ApplicationRoot + "Content/assets/img/department.gif",
        //                node_type = "sec",
        //                children = GetPositionBySec(usertype, item.DivID, item.DepID, item.SecID),
        //            };
        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}

        //public List<JSTreeViewModel> GetPositionBySec(int usertype, int? divid, int? depid, int? secid)
        //{
        //    var items = new List<JSTreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var position = GetPositionManPower(usertype, divid, depid, secid);

        //        foreach (var item in position)
        //        {
        //            var countChild = db.vw_Man_Power.Where(m => m.TypeID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == item.PoID
        //                    && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
        //                .GroupBy(g => g.UserID).Select(group => new { UserID = group.Key }).Count();

        //            var model = new JSTreeViewModel()
        //            {
        //                id = "Pos" + item.MpID.ToString(),
        //                text = item.PoName + " (" + countChild + ")",
        //                state = new TreeStateViewModel() { opened = false },
        //                icon = SysConfig.ApplicationRoot + "Content/assets/img/flag_white.gif",
        //                node_type = "pos",
        //                children = GetUserManPower(usertype, item.DivID, item.DepID, item.SecID, item.PoID),
        //            };
        //            items.Add(model);
        //        }
        //    }

        //    return items;
        //}

        //public List<JSTreeViewModel> GetUserManPower(int? type, int? divid, int? depid, int? secid, int? poid)
        //{
        //    var items = new List<JSTreeViewModel>();

        //    using (SATEntities db = new SATEntities())
        //    {
        //        var user = db.vw_Man_Power.Where(m => m.TypeID == type && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == poid
        //                      && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
        //                      .GroupBy(item => item.UserID, (key, group) => new
        //                      {
        //                          UserID = key,
        //                          TiShortName = group.FirstOrDefault().TiShortName,
        //                          FullNameTh = group.FirstOrDefault().FullNameTh,
        //                          MpID = group.FirstOrDefault().MpID,
        //                          DivID = group.FirstOrDefault().DivID,
        //                          DepID = group.FirstOrDefault().DepID,
        //                          SecID = group.FirstOrDefault().SecID,
        //                          PoID = group.FirstOrDefault().PoID,
        //                      }).OrderBy(o => o.MpID).ToList();

        //        foreach (var item in user)
        //        {
        //            var model = new JSTreeViewModel()
        //            {
        //                id = item.MpID.ToString(),
        //                icon = SysConfig.ApplicationRoot + "Content/assets/img/user2.png",
        //                node_type = "user",
        //                text = "(" + (type.ToString() == "1" ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0')) + ") " + item.TiShortName + item.FullNameTh,
        //            };
        //            items.Add(model);
        //        }
        //    }
        //    return items;
        //}

        #endregion
    }
}