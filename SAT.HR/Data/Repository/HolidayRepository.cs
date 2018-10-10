using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class HolidayRepository
    {
        public HolidayResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Holiday.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.HolDescription.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "HolDescription":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.HolDescription).ToList() : data.OrderByDescending(x => x.HolDescription).ToList();
                        break;
                    case "HolDateText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.HolDate).ToList() : data.OrderByDescending(x => x.HolDate).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new HolidayViewModel()
                {
                    RowNumber = ++i,
                    HolID = s.HolID,
                    HolDate = s.HolDate,
                    HolDescription = s.HolDescription,
                    HolYear = s.HolDate.Value.Year,
                    HolDateText = s.HolDate.Value.ToString("dd/MM/yyyy"),
                }).Skip(start * length).Take(length).ToList();

                HolidayResult result = new HolidayResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<HolidayViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Holiday.Select(s => new HolidayViewModel()
                {
                    HolID = s.HolID,
                    HolDate = s.HolDate,
                    HolDescription = s.HolDescription,
                    HolYear = s.HolDate.Value.Year,
                    HolDateText = s.HolDate.Value.ToString("dd/MM/yyyy"),
                }).OrderBy(x => x.HolDescription).ToList();
                return list;
            }
        }

        public HolidayViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Holiday.Where(x => x.HolID == id).FirstOrDefault();
                HolidayViewModel model = new Models.HolidayViewModel();
                model.HolID = data.HolID;
                model.HolDate = data.HolDate;
                model.HolDescription = data.HolDescription;
                return model;
            }
        }

        public ResponseData AddByEntity(HolidayViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Holiday model = new tb_Holiday();
                    model.HolID = data.HolID;
                    model.HolDate = data.HolDate;
                    model.HolDescription = data.HolDescription;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Holiday.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(HolidayViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Holiday.Single(x => x.HolID == newdata.HolID);
                    data.HolDate = newdata.HolDate;
                    data.HolDescription = newdata.HolDescription;
                    data.ModifyBy = UtilityService.User.UserID;
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
                    var obj = db.tb_Holiday.SingleOrDefault(c => c.HolID == id);
                    if (obj != null)
                    {
                        db.tb_Holiday.Remove(obj);
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