using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class DegreeRepository
    {
        public DegreeResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Degree.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.DegName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "DegName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DegName).ToList() : data.OrderByDescending(x => x.DegName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new DegreeViewModel()
                {
                    RowNumber = ++i,
                    DegID = s.DegID,
                    DegName = s.DegName,
                    DegStatus = s.DegStatus,
                }).Skip(start * length).Take(length).ToList();

                DegreeResult result = new DegreeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<DegreeViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Degree.Select(s => new DegreeViewModel()
                {
                    DegID = s.DegID,
                    DegName = s.DegName,
                    DegStatus = s.DegStatus,
                }).OrderBy(x => x.DegName).ToList();
                return list;
            }
        }

        public DegreeViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Degree.Where(x => x.DegID == id).FirstOrDefault();
                DegreeViewModel model = new Models.DegreeViewModel();
                model.DegID = data.DegID;
                model.DegName = data.DegName;
                model.DegStatus = data.DegStatus;
                return model;
            }
        }

        public ResponseData AddByEntity(DegreeViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Degree model = new tb_Degree();
                    model.DegID = data.DegID;
                    model.DegName = data.DegName;
                    model.DegStatus = data.DegStatus;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Degree.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(DegreeViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Degree.Single(x => x.DegID == newdata.DegID);
                    data.DegName = newdata.DegName;
                    data.DegStatus = newdata.DegStatus;
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
                    var obj = db.tb_Degree.SingleOrDefault(c => c.DegID == id);
                    if (obj != null)
                    {
                        db.tb_Degree.Remove(obj);
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