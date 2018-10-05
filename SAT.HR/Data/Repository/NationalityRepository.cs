using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class NationalityRepository
    {
        public NationalityResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Nationality.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.NatName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "NatName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.NatName).ToList() : data.OrderByDescending(x => x.NatName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new NationalityViewModel()
                {
                    RowNumber = ++i,
                    NatID = s.NatID,
                    NatName = s.NatName,
                    NatStatus = s.NatStatus,
                }).Skip(start * length).Take(length).ToList();

                NationalityResult result = new NationalityResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public NationalityViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Nationality.Where(x => x.NatID == id).FirstOrDefault();
                NationalityViewModel model = new Models.NationalityViewModel();
                model.NatID = data.NatID;
                model.NatName = data.NatName;
                model.NatStatus = data.NatStatus;
                return model;
            }
        }

        public ResponseData AddByEntity(NationalityViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Nationality model = new tb_Nationality();
                    model.NatID = data.NatID;
                    model.NatName = data.NatName;
                    model.NatStatus = data.NatStatus;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Nationality.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(NationalityViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Nationality.Single(x => x.NatID == newdata.NatID);
                    data.NatName = newdata.NatName;
                    data.NatStatus = newdata.NatStatus;
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
                    var obj = db.tb_Nationality.SingleOrDefault(c => c.NatID == id);
                    if (obj != null)
                    {
                        db.tb_Nationality.Remove(obj);
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