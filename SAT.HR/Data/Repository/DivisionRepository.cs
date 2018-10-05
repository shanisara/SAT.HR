using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class DivisionRepository : RepositoryBase
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
                    }

                    int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                    int length = pageSize ?? 10;

                    var list = data.Select((s, i) => new DivisionViewModel()
                    {
                        RowNumber = i + 1,
                        DivID = s.DivID,
                        DivName = s.DivName,
                        DivStatus = s.DivStatus
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
                return model;
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
                    model.DivStatus = data.DivStatus;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
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
                    data.DivStatus = newdata.DivStatus;
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

        //public List<sp_GetXxxxxx_Result> GetLastCases(int x, int xx = 3)
        //{ 
        //    using (SATEntities db = new SATEntities())
        //    { 
        //        var ps = new[] {                    
        //            this.GenObjectParam("x", p_ctaID, ParamStyle.General),
        //            this.GenObjectParam("xx", p_top, ParamStyle.General)
        //        };

        //        var result = this.ExecStoredProcedure<sp_GetLastCases_Result>("sp_GetXxxxx", ps);
        //        return result;

        //    }        
        //}

    }
}