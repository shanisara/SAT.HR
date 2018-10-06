using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class EducationRepository
    {
        public EducationResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Education.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.EduName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "EduCode":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.EduCode).ToList() : data.OrderByDescending(x => x.EduCode).ToList();
                        break;
                    case "EduName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.EduName).ToList() : data.OrderByDescending(x => x.EduName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new EducationViewModel()
                {
                    RowNumber = ++i,
                    EduID = s.EduID,
                    EduCode = s.EduCode,
                    EduName = s.EduName,
                    EduStatus = s.EduStatus,
                }).Skip(start * length).Take(length).ToList();

                EducationResult result = new EducationResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<EducationViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Education.Select(s => new EducationViewModel()
                {
                    EduID = s.EduID,
                    EduCode = s.EduCode,
                    EduName = s.EduName,
                    EduStatus = s.EduStatus,
                }).OrderBy(x => x.EduName).ToList();
                return list;
            }
        }

        public EducationViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Education.Where(x => x.EduID == id).FirstOrDefault();
                EducationViewModel model = new Models.EducationViewModel();
                model.EduID = data.EduID;
                model.EduCode = data.EduCode;
                model.EduName = data.EduName;
                model.EduStatus = data.EduStatus;
                return model;
            }
        }

        public ResponseData AddByEntity(EducationViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Education model = new tb_Education();
                    model.EduID = data.EduID;
                    model.EduCode = data.EduCode;
                    model.EduName = data.EduName;
                    model.EduStatus = data.EduStatus;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Education.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(EducationViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Education.Single(x => x.EduID == newdata.EduID);
                    data.EduCode = newdata.EduCode;
                    data.EduName = newdata.EduName;
                    data.EduStatus = newdata.EduStatus;
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
                    var obj = db.tb_Education.SingleOrDefault(c => c.EduID == id);
                    if (obj != null)
                    {
                        db.tb_Education.Remove(obj);
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