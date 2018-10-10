using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class DisciplineRepository : RepositoryBase
    {
        public DisciplineResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                DisciplineResult result = new DisciplineResult();

                try
                {
                    var data = db.tb_Discipline.ToList();

                    int recordsTotal = data.Count();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        data = data.Where(x => x.DisName.Contains(filter)).ToList();
                    }

                    int recordsFiltered = data.Count();

                    switch (sortBy)
                    {
                        case "DisName":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.DisName).ToList() : data.OrderByDescending(x => x.DisName).ToList();
                            break;
                    }

                    int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                    int length = pageSize ?? 10;

                    var list = data.Select((s, i) => new DisciplineViewModel()
                    {
                        RowNumber = i + 1,
                        DisID = s.DisID,
                        DisName = s.DisName,
                    }).Skip(start * length).Take(length).ToList();

                    result.draw = draw ?? 0;
                    result.recordsTotal = recordsTotal;
                    result.recordsFiltered = recordsFiltered;
                    result.data = list;
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public List<DisciplineViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Discipline.Select(s => new DisciplineViewModel()
                {
                    DisID = s.DisID,
                    DisName = s.DisName,
                }).OrderBy(x => x.DisName).ToList();
                return list;
            }
        }

        public DisciplineViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Discipline.Where(x => x.DisID == id).FirstOrDefault();
                DisciplineViewModel model = new Models.DisciplineViewModel();
                model.DisID = data.DisID;
                model.DisName = data.DisName;
                return model;
            }
        }

        public ResponseData AddByEntity(DisciplineViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Discipline model = new tb_Discipline();
                    model.DisID = data.DisID;
                    model.DisName = data.DisName;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Discipline.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(DisciplineViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Discipline.Single(x => x.DisID == newdata.DisID);
                    data.DisName = newdata.DisName;
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
                    var obj = db.tb_Discipline.SingleOrDefault(c => c.DisID == id);
                    if (obj != null)
                    {
                        db.tb_Discipline.Remove(obj);
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