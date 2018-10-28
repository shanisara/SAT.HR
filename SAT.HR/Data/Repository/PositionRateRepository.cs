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
                    if (model != null)
                    {
                        PositionRateViewModel obj = new PositionRateViewModel();
                        obj.MpID = data.MpID;
                        obj.MpCode = data.UserTID == 1 ? data.MpID.ToString().PadLeft(3, '0') : data.MpID.ToString().PadLeft(4, '0');
                        obj.DivID = data.DivID;
                        obj.DepID = data.DepID;
                        obj.SecID = data.SecID;
                        obj.PoID = data.PoID;
                        obj.MpMan = data.MpMan;
                        obj.UserID = data.UserID;
                        obj.FullNameTh = data.TiShortName + data.FullNameTh;
                        obj.EduID = data.EduID;
                        obj.UserTID = data.UserTID;
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
                    tb_Man_Power model = new tb_Man_Power();
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
                }
                catch (Exception)
                {

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
                catch (Exception)
                {

                }
                return result;
            }
        }

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

                    var deivision = db.vw_Man_Power.Where(m => m.UserTID == usertype)
                        .GroupBy(item => item.DivID, (key, group) => new { DivID = key, DivName = group.FirstOrDefault().DivName }).ToList();

                    foreach (var item in deivision)
                    {
                        var countChild = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DivID == item.DivID && !string.IsNullOrEmpty(m.DepName))
                            .GroupBy(g => g.DepID).Select(group => new { DepID = group.Key }).Count();

                        var model = new TreeViewModel();
                        model.id = item.DivID.ToString();
                        model.text = item.DivName + " ("  + countChild + ")";
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
                var department = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DivID == divid
                    && !string.IsNullOrEmpty(m.DepName))
                    .GroupBy(item => item.DepID, (key, group) => new { DepID = key, DepName = group.FirstOrDefault().DepName }).ToList();

                foreach (var item in department)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DepID == item.DepID && m.DivID == divid && !string.IsNullOrEmpty(m.SecName))
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
                var section = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DivID == divid && m.DepID == depid
                    && !string.IsNullOrEmpty(m.SecName)).GroupBy(item => item.SecID, (key, group) => new { SecID = key, SecName = group.FirstOrDefault().SecName }).ToList();

                foreach (var item in section)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.SecID == item.SecID && !string.IsNullOrEmpty(m.PoName))
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
                var position = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && !string.IsNullOrEmpty(m.PoName))
                    .GroupBy(item => item.PoID, (key, group) => new { PoID = key, PoName = group.FirstOrDefault().PoName }).ToList();

                foreach (var item in position)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == item.PoID
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
                var user = db.vw_Man_Power.Where(m => m.UserTID == usertype && m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == poid
                                && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
                            .GroupBy(item => item.UserID, (key, group) => new {
                                UserID = key,
                                TiShortName = group.FirstOrDefault().TiShortName,
                                FullNameTh = group.FirstOrDefault().FullNameTh,
                                MpID = group.FirstOrDefault().MpID
                            }).ToList();

                foreach (var item in user)
                {

                    var model = new TreeViewModel();
                    model.id = item.UserID.ToString();
                    model.text = "("+ item.MpID.ToString().PadLeft(4, '0') +") "+ item.TiShortName + item.FullNameTh; // + " [" + item.MpCode + "]";
                    model.children = false;
                    model.node_type = "user";
                    model.node_mpid = item.MpID;
                    items.Add(model);
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreeAll(int usertype)
        {
            var items1 = new List<TreeViewModel>();
            var first = new[]
            {
                new
                {
                    id = "0",
                    text = "การกีฬาแห่งประเทศไทย",
                    state = new  { opened= true, selected = true, disabled= true },
                    children = new[]
                    {
                        new { id = "1", text = "ฝ่าย",
                            state = new  { opened= true, selected = true, disabled= true },
                            children = new[]
                            {
                                new { id = "2", text = "กอง",
                                    state = new  { opened= true, selected = true, disabled= true },
                                    children = new[]
                                    {
                                        new { id = "3", text = "งาน",
                                            state = new  { opened= true, selected = true, disabled= true },
                                            children = new[]
                                            {
                                                new { id = "4",  text = "ตำแหน่ง",
                                                    state = new  { opened= true, selected = true, disabled= true },
                                                    children = new[]
                                                    {
                                                        new { id = "51", text = "พนักงาน" },
                                                        new { id = "52", text = "พนักงาน" },
                                                        new { id = "53", text = "พนักงาน" },
                                                        new { id = "54", text = "พนักงาน" }
                                                    }
                                                }
                                            },
                                        }
                                    },
                                }
                            },
                        }
                    }
                }
            }
            .ToList();


            //var first = new[]
            //{
            //    new
            //    {
            //        id = "0",
            //        text = "การกีฬาแห่งประเทศไทย",
            //        state = new  { opened= true, selected= true, disabled= true },
            //        children = new[]
            //        {
            //            new { id = "child-1", text = "Child 1", children = true },
            //            new { id = "child-2", text = "Child 2", children = true }
            //        }
            //    }
            //}
            //.ToList();


            //var g1 = Guid.NewGuid().ToString();
            //var g2 = Guid.NewGuid().ToString();
            //var next = new[]
            //{
            //    new { id = "child-" + g1, text = "Child " + g1, children = true },
            //    new { id = "child-" + g2, text = "Child " + g2, children = true }
            //}
            //.ToList();

            return items1;
        }

        public List<PositionRateViewModel> GetPositionRate(int? type)
        {
            using (SATEntities db = new SATEntities())
            {
                List<PositionRateViewModel> list = new List<PositionRateViewModel>();

                var position = db.vw_Man_Power.Where(m => m.UserTID == type && !string.IsNullOrEmpty(m.PoName))
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
    }
}