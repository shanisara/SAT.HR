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
                    model = db.vw_Man_Power.Where(x => x.MpID == id).Select(s => new PositionRateViewModel
                    {
                        MpID = s.MpID,
                        DivID = s.DivID,
                        DepID = s.DepID,
                        SecID = s.SecID,
                        PoID = s.PoID,
                        MpCode = s.MpCode,
                        MpMan = s.MpMan,
                        UserID = s.UserID,
                        FullNameTh = s.TiShortName + s.FullNameTh,
                        EduID = s.EduID
                    }).FirstOrDefault();
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
                    model.MpCode = data.MpCode;
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
                    model.MpCode = newdata.MpCode;
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

        public List<TreeViewModel> GetTree()
        {
            var items = GetTreeDivision();
            return items;
        }

        public List<TreeViewModel> GetTree(string type, int? divid, int? depid, int? secid, int? poid)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                switch (type)
                {
                    case "div":
                        items = GetDepartment(divid); break;
                    case "dep":
                        items = GetTreeSection(divid, depid); break;
                    case "sec":
                        items = GetTreePosition(divid, depid, secid); break;
                    case "pos":
                        items = GetTreeUser(divid, depid, secid, poid); break;
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreeDivision()
        {
            var items = new List<TreeViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {

                    var deivision = db.vw_Man_Power
                        .GroupBy(item => item.DivID, (key, group) => new { DivID = key, DivName = group.FirstOrDefault().DivName }).ToList();

                    foreach (var item in deivision)
                    {
                        var countChild = db.vw_Man_Power.Where(m => m.DivID == item.DivID && !string.IsNullOrEmpty(m.DepName))
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

        public List<TreeViewModel> GetDepartment(int? divid)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var department = db.vw_Man_Power.Where(m => m.DivID == divid
                    && !string.IsNullOrEmpty(m.DepName))
                    .GroupBy(item => item.DepID, (key, group) => new { DepID = key, DepName = group.FirstOrDefault().DepName }).ToList();

                foreach (var item in department)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.DepID == item.DepID && m.DivID == divid && !string.IsNullOrEmpty(m.SecName))
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

        public List<TreeViewModel> GetTreeSection(int? divid, int? depid)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var section = db.vw_Man_Power.Where(m => m.DivID == divid && m.DepID == depid
                    && !string.IsNullOrEmpty(m.SecName)).GroupBy(item => item.SecID, (key, group) => new { SecID = key, SecName = group.FirstOrDefault().SecName }).ToList();

                foreach (var item in section)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.SecID == item.SecID && !string.IsNullOrEmpty(m.PoName)).GroupBy(g => g.PoID).Select(group => new { PoID = group.Key }).Count();

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

        public List<TreeViewModel> GetTreePosition(int? divid, int? depid, int? secid)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var position = db.vw_Man_Power.Where(m => m.DivID == divid && m.DepID == depid && m.SecID == secid && !string.IsNullOrEmpty(m.PoName))
                    .GroupBy(item => item.PoID, (key, group) => new { PoID = key, PoName = group.FirstOrDefault().PoName }).ToList();

                foreach (var item in position)
                {
                    var countChild = db.vw_Man_Power.Where(m => m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == item.PoID
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

        public List<TreeViewModel> GetTreeUser(int? divid, int? depid, int? secid, int? poid)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                var user = db.vw_Man_Power.Where(m => m.DivID == divid && m.DepID == depid && m.SecID == secid && m.PoID == poid
                                && !string.IsNullOrEmpty(m.DivName) && !string.IsNullOrEmpty(m.DepName) && !string.IsNullOrEmpty(m.SecName) && !string.IsNullOrEmpty(m.FullNameTh))
                            .GroupBy(item => item.UserID, (key, group) => new {
                                UserID = key,
                                TiShortName = group.FirstOrDefault().TiShortName,
                                FullNameTh = group.FirstOrDefault().FullNameTh,
                                MpCode = group.FirstOrDefault().MpCode,
                                MpID = group.FirstOrDefault().MpID
                            }).ToList();

                foreach (var item in user)
                {

                    var model = new TreeViewModel();
                    model.id = item.UserID.ToString();
                    model.text = item.TiShortName + item.FullNameTh; // + " [" + item.MpCode + "]";
                    model.children = false;
                    model.node_type = "user";
                    model.node_mpid = item.MpID;
                    items.Add(model);
                }
            }

            return items;
        }
    }
}