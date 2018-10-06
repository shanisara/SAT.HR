using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class CapabilityRepository
    {
        public CapabilityResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Capability.ToList();

                int recordsTotal = data.Count();

                //if (!string.IsNullOrEmpty(filter))
                //{
                //    data = data.Where(x => x.CapYear.Contains(filter)).ToList();
                //}

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "CapYear":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CapYear).ToList() : data.OrderByDescending(x => x.CapYear).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new CapabilityViewModel()
                {
                    RowNumber = ++i,
                    CapID = s.CapID,
                    CapYear = s.CapYear,
                    CapTID = s.CapTID,
                    MenuID = s.MenuID,
                    CapGroupID = s.CapGroupID,
                }).Skip(start * length).Take(length).ToList();

                CapabilityResult result = new CapabilityResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public CapabilityViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Capability.Where(x => x.CapID == id).FirstOrDefault();
                CapabilityViewModel model = new Models.CapabilityViewModel();
                model.CapID = data.CapID;
                model.CapYear = data.CapYear;
                model.CapTID = data.CapTID;
                model.MenuID = data.MenuID;
                model.CapGroupID = data.CapGroupID;
                return model;
            }
        }

        public List<CapabilityViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Capability.Select(s => new CapabilityViewModel()
                {
                    CapID = s.CapID,
                    CapYear = s.CapYear,
                    CapTID = s.CapTID,
                    MenuID = s.MenuID,
                    CapGroupID = s.CapGroupID,
                }).OrderBy(x => x.CapYear).ToList();
                return list;
            }
        }

        public ResponseData AddByEntity(CapabilityViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Capability model = new tb_Capability();
                    model.CapID = data.CapID;
                    model.CapYear = data.CapYear;
                    model.CapTID = data.CapTID;
                    model.MenuID = data.MenuID;
                    model.CapGroupID = data.CapGroupID;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Capability.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(CapabilityViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Capability.Single(x => x.CapID == newdata.CapID);
                    data.CapYear = newdata.CapYear;
                    data.CapTID = newdata.CapTID;
                    data.MenuID = newdata.MenuID;
                    data.CapGroupID = newdata.CapGroupID;
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

        public ResponseData RemoveByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_Capability.SingleOrDefault(c => c.CapID == id);
                    if (obj != null)
                    {
                        db.tb_Capability.Remove(obj);
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
    }
}