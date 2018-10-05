using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class ReligionRepository
    {
        public ReligionResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Religion.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.RelName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "RelName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.RelName).ToList() : data.OrderByDescending(x => x.RelName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new ReligionViewModel()
                {
                    RowNumber = ++i,
                    RelD = s.RelD,
                    RelName = s.RelName,
                    RelStatus = s.RelStatus,
                }).Skip(start * length).Take(length).ToList();

                ReligionResult result = new ReligionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public ReligionViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Religion.Where(x => x.RelD == id).FirstOrDefault();
                ReligionViewModel model = new Models.ReligionViewModel();
                model.RelD = data.RelD;
                model.RelName = data.RelName;
                model.RelStatus = data.RelStatus;
                return model;
            }
        }

        public ResponseData AddByEntity(ReligionViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Religion model = new tb_Religion();
                    model.RelD = data.RelD;
                    model.RelName = data.RelName;
                    model.RelStatus = data.RelStatus;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Religion.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(ReligionViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Religion.Single(x => x.RelD == newdata.RelD);
                    data.RelName = newdata.RelName;
                    data.RelStatus = newdata.RelStatus;
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
                    var obj = db.tb_Religion.SingleOrDefault(c => c.RelD == id);
                    if (obj != null)
                    {
                        db.tb_Religion.Remove(obj);
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