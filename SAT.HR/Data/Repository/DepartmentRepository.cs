using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class DepartmentRepository
    {
        public DepartmentResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Department.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.DivName.Contains(filter) || x.DepName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "DivName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DivName).ToList() : data.OrderByDescending(x => x.DivName).ToList();
                        break;
                    case "DepName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DepName).ToList() : data.OrderByDescending(x => x.DepName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new DepartmentViewModel()
                {
                    RowNumber = i + 1,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepStatus = (bool)s.DepStatus
                }).Skip(start * length).Take(length).ToList();


                DepartmentResult result = new DepartmentResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<DepartmentViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Department.Select(s => new DepartmentViewModel()
                {
                    DepID = s.DepID,
                    DepName = s.DepName,
                    DepStatus = (bool)s.DepStatus
                }).OrderBy(x => x.DepName).ToList();
                return list;
            }
        }

        public DepartmentViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Department.Where(x => x.DepID == id).FirstOrDefault();
                DepartmentViewModel model = new Models.DepartmentViewModel();
                model.DepID = data.DepID;
                model.DepName = data.DepName;
                model.DepStatus = (bool)data.DepStatus;
                return model;
            }
        }

        public ResponseData AddByEntity(DepartmentViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Department model = new tb_Department();
                    model.DepID = data.DepID;
                    model.DepName = data.DepName;
                    model.DepStatus = data.DepStatus;
                    model.DivID = (int)data.DivID;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Department.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(DepartmentViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Department.Single(x => x.DepID == newdata.DivID);
                    data.DepName = newdata.DepName;
                    data.DepStatus = newdata.DepStatus;
                    data.DivID = (int)newdata.DivID;
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
                    var obj = db.tb_Department.SingleOrDefault(c => c.DepID == id);
                    if (obj != null)
                    {
                        db.tb_Department.Remove(obj);
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