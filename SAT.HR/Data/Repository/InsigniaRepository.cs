using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class InsigniaRepository
    {
        public InsigniaResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Insignia.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.InsFullName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "InsFullName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.InsFullName).ToList() : data.OrderByDescending(x => x.InsFullName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new InsigniaViewModel()
                {
                    RowNumber = ++i,
                    InsID = s.InsID,
                    InsFullName = s.InsFullName,
                    InsShortName = s.InsShortName,
                }).Skip(start * length).Take(length).ToList();


                InsigniaResult result = new InsigniaResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<InsigniaViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Insignia.Select(s => new InsigniaViewModel()
                {
                    InsID = s.InsID,
                    InsFullName = s.InsFullName,
                    InsShortName = s.InsShortName,
                }).OrderBy(x => x.InsFullName).ToList();
                return list;
            }
        }

        public InsigniaViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Insignia.Where(x => x.InsID == id).FirstOrDefault();
                InsigniaViewModel model = new Models.InsigniaViewModel();
                model.InsID = data.InsID;
                model.InsFullName = data.InsFullName;
                model.InsShortName = data.InsShortName;
                return model;
            }
        }

        public ResponseData AddByEntity(InsigniaViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Insignia model = new tb_Insignia();
                    model.InsID = data.InsID;
                    model.InsFullName = data.InsFullName;
                    model.InsShortName = data.InsShortName;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Insignia.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(InsigniaViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Insignia.Single(x => x.InsID == newdata.InsID);
                    data.InsFullName = newdata.InsFullName;
                    data.InsShortName = newdata.InsShortName;
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
                    var obj = db.tb_Insignia.SingleOrDefault(c => c.InsID == id);
                    if (obj != null)
                    {
                        db.tb_Insignia.Remove(obj);
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