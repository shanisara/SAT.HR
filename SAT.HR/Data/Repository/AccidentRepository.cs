using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class AccidentRepository
    {
        public AccidentResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Accident.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.ActPlace.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "ActDate":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ActDate).ToList() : data.OrderByDescending(x => x.ActDate).ToList(); break;
                    case "ActPlace":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ActPlace).ToList() : data.OrderByDescending(x => x.ActPlace).ToList(); break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new AccidentViewModel()
                {
                    RowNumber = i + 1,
                    ActID = s.ActID,
                    UserID = s.UserID,
                    ActDate = s.ActDate,
                    ActPlace = s.ActPlace,
                    ActDesc = s.ActDesc,
                }).Skip(start * length).Take(length).ToList();

                AccidentResult result = new AccidentResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<AccidentViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Accident.Select(s => new AccidentViewModel()
                {
                    ActID = s.ActID,
                    UserID = s.UserID,
                    ActDate = s.ActDate,
                    ActPlace = s.ActPlace,
                    ActDesc = s.ActDesc,
                }).ToList();
                return list;
            }
        }

        public AccidentViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Accident.Where(x => x.ActID == id).FirstOrDefault();
                AccidentViewModel model = new Models.AccidentViewModel();
                model.ActID = data.ActID;
                model.UserID = data.UserID;
                model.ActDate = data.ActDate;
                model.ActPlace = data.ActPlace;
                return model;
            }
        }

        public ResponseData AddByEntity(AccidentViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Accident model = new tb_Accident();
                    model.UserID = data.UserID;
                    model.ActDate = data.ActDate;
                    model.ActPlace = data.ActPlace;
                    model.ActDesc = data.ActDesc;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Accident.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(AccidentViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Accident.Single(x => x.ActID == newdata.ActID);
                    data.UserID = newdata.UserID;
                    data.ActDate = newdata.ActDate;
                    data.ActPlace = newdata.ActPlace;
                    data.ActDesc = newdata.ActDesc;
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
                    var obj = db.tb_Accident.SingleOrDefault(c => c.ActID == id);
                    if (obj != null)
                    {
                        db.tb_Accident.Remove(obj);
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