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
                    model = db.tb_Man_Power.Where(x => x.MpID == id).Select(s => new PositionRateViewModel
                    {
                        MpID = s.MpID,
                        DivID = s.DivID,
                        DepID = s.DepID,
                        SecID = s.SecID,
                        PoID = s.PoID,
                        MpCode = s.MpCode,
                        MpMan = s.MpMan,
                        UserID = s.UserID,
                        FullNameTh = "(" + s.UserID + ") " + s.UserID,
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

        public List<TreeViewModel> GetTree(string id, string type)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                switch (type)
                {
                    case "div":
                        items = GetDepartment(id); break;
                    case "dep":
                        items = GetTreeSection(id); break;
                    case "sec":
                        items = GetTreePosition(id); break;
                    case "po":
                        items = GetTreeUser(id); break;
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
                    var deivision = db.vw_Man_Power_Division.ToList();

                    foreach (var item in deivision)
                    {
                        var countChild = db.tb_Department.Where(m => m.DivID == item.DivID && m.DepStatus == true).Count();
                        var model = new TreeViewModel();
                        model.id = item.DivID.ToString();
                        model.text = item.DivName + " ("  + countChild + ")";
                        model.node_type = "div";
                        model.children = (countChild > 0) ? true : false;
                        items.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }

        public List<TreeViewModel> GetDepartment(string id)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {

                int divid = Convert.ToInt32(id);
                var department = db.tb_Department.Where(m => m.DivID == divid && m.DepStatus == true).ToList();

                foreach (var item in department)
                {
                    var countChild = db.tb_Section.Where(m => m.DepID == item.DepID && m.SecStatus == true).Count();

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

        public List<TreeViewModel> GetTreeSection(string id)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                int depid = Convert.ToInt32(id);
                var section = db.tb_Section.Where(m => m.DepID == depid && m.SecStatus == true).ToList();

                foreach (var item in section)
                {
                    var countChild = db.tb_Man_Power.Where(m => m.DivID == item.DivID && m.DepID == item.DepID && m.SecID == item.SecID).Count();

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

        public List<TreeViewModel> GetTreePosition(string id)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                int poid = Convert.ToInt32(id);
                var position = db.tb_Man_Power.Where(m => m.PoID == poid).ToList();

                foreach (var item in position)
                {
                    var countChild = db.tb_Man_Power.Where(m => m.DivID == 0 && m.DepID == 0 && m.SecID == 0).Count();
                    var PoName = db.tb_Position.Where(m => m.PoID == item.PoID).FirstOrDefault();

                    var model = new TreeViewModel();
                    model.id = item.PoID.ToString();
                    model.text = PoName.PoName + " (" + countChild + ")";
                    model.children = (countChild > 0) ? true : false;
                    model.node_type = "po";
                    items.Add(model);
                }
            }

            return items;
        }

        public List<TreeViewModel> GetTreeUser(string id)
        {
            var items = new List<TreeViewModel>();

            using (SATEntities db = new SATEntities())
            {
                int poid = Convert.ToInt32(id);
                var user = db.vw_Employee.Where(m => m.UserID == poid && m.IsActive == true).ToList();

                foreach (var item in user)
                {
                    var countChild = db.vw_Employee.Where(m => m.DivID == item.DivID && m.DepID == item.DepID && m.SecID == item.SecID && m.PoID == item.PoID).Count();

                    var model = new TreeViewModel();
                    model.id = item.UserID.ToString();
                    model.text = item.FullNameTh + " (" + countChild + ")";
                    model.children = (countChild > 0) ? true : false;
                    model.node_type = "user";
                    items.Add(model);
                }
            }

            return items;
        }
    }
}