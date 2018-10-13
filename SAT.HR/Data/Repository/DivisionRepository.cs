using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class DivisionRepository
    {
        public DivisionResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                DivisionResult result = new DivisionResult();

                try
                {
                    var data = db.tb_Division.ToList();

                    int recordsTotal = data.Count();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        data = data.Where(x => x.DivName.Contains(filter)).ToList();
                    }

                    int recordsFiltered = data.Count();

                    switch (sortBy)
                    {
                        case "DivName":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.DivName).ToList() : data.OrderByDescending(x => x.DivName).ToList();
                         break;
                        case "Status":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.DivStatus).ToList() : data.OrderByDescending(x => x.DivStatus).ToList();
                            break;
                    }

                    int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                    int length = pageSize ?? 10;

                    var list = data.Select((s, i) => new DivisionViewModel()
                    {
                        RowNumber = i + 1,
                        DivID = s.DivID,
                        DivName = s.DivName,
                        DivStatus = s.DivStatus,
                        Status = s.DivStatus == true ? EnumType.StatusNameActive : EnumType.StatusNameNotActive
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

        public DivisionViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Division.Where(x => x.DivID == id).FirstOrDefault();
                DivisionViewModel model = new Models.DivisionViewModel();
                model.DivID = data.DivID;
                model.DivName = data.DivName;
                model.DivStatus = data.DivStatus;
                model.Status = data.DivStatus == true ? EnumType.StatusNameActive : EnumType.StatusNameNotActive;
                return model;
            }
        }

        public List<DivisionViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Division.Select(s => new DivisionViewModel()
                {
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DivStatus = s.DivStatus
                }).OrderBy(x => x.DivName).ToList();
                return list;
            }
        }

        public ResponseData AddByEntity(DivisionViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Division model = new tb_Division();
                    model.DivID = data.DivID;
                    model.DivName = data.DivName;
                    model.DivStatus = (data.Status == "1") ? true : false;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Division.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(DivisionViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Division.Single(x => x.DivID == newdata.DivID);
                    data.DivName = newdata.DivName;
                    data.DivStatus = (newdata.Status == "1") ? true : false;
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
                    var obj = db.tb_Division.SingleOrDefault(c => c.DivID == id);
                    if (obj != null)
                    {
                        db.tb_Division.Remove(obj);
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