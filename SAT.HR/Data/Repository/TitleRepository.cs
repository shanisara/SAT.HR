using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class TitleRepository
    {
        public TitleResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Title.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.TiFullName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "TiFullName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.TiFullName).ToList() : data.OrderByDescending(x => x.TiFullName).ToList(); break;
                    case "TiShortName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.TiShortName).ToList() : data.OrderByDescending(x => x.TiShortName).ToList(); break;
                    case "SexName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SexName).ToList() : data.OrderByDescending(x => x.SexName).ToList(); break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new TitleViewModel()
                {
                    RowNumber = i + 1,
                    TiID = s.TiID,
                    TiFullName = s.TiFullName,
                    TiShortName = s.TiShortName,
                    TiStatus = s.TiStatus,
                    SexID = s.SexID,
                    SexName = s.SexName,
                }).Skip(start * length).Take(length).ToList();

                TitleResult result = new TitleResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<TitleViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Title.Select(s => new TitleViewModel()
                {
                    TiID = s.TiID,
                    TiFullName = s.TiFullName,
                    TiShortName = s.TiShortName,
                    TiStatus = s.TiStatus,
                    SexID = s.SexID,
                }).OrderBy(x => x.TiFullName).ToList();
                return list;
            }
        }

        public TitleViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Title.Where(x => x.TiID == id).FirstOrDefault();
                TitleViewModel model = new Models.TitleViewModel();
                model.TiID = data.TiID;
                model.TiFullName = data.TiFullName;
                model.TiShortName = data.TiShortName;
                model.TiStatus = data.TiStatus;
                model.SexID = data.SexID;
                return model;
            }
        }

        public ResponseData AddByEntity(TitleViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Title model = new tb_Title();
                    model.TiID = data.TiID;
                    model.TiFullName = data.TiFullName;
                    model.TiShortName = data.TiShortName;
                    model.TiStatus = data.TiStatus;
                    model.SexID = data.SexID;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Title.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(TitleViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Title.Single(x => x.TiID == newdata.TiID);
                    data.TiFullName = newdata.TiFullName;
                    data.TiShortName = newdata.TiShortName;
                    data.TiStatus = newdata.TiStatus;
                    data.SexID = newdata.SexID;
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
                    var obj = db.tb_Title.SingleOrDefault(c => c.TiID == id);
                    if (obj != null)
                    {
                        db.tb_Title.Remove(obj);
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