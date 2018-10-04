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
        public DivisionViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Division.Where(x => x.DivID == id);

            return null;
            }
        }

        public DivisionResult GetDivision(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
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

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<DivisionViewModel> list = new List<Models.DivisionViewModel>();
                foreach (var m in data)
                {
                    DivisionViewModel model = new Models.DivisionViewModel();
                    model.DivID = m.DivID;
                    model.DivCode = m.DivCode;
                    model.DivName = m.DivName;
                    model.DivStatus = m.DivStatus;
                    list.Add(model);
                }

                DivisionResult result = new DivisionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public void AddByEntity(DivisionViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                tb_Division model = new tb_Division();
                model.DivID = data.DivID;
                model.DivCode = data.DivCode;
                model.DivName = data.DivName;
                model.DivStatus = data.DivStatus;
                model.CreateBy = data.ModifyBy;
                model.CreateDate = DateTime.Now;
                model.ModifyBy = data.ModifyBy;
                model.ModifyDate = DateTime.Now;
                db.tb_Division.Add(model);
                db.SaveChanges();
            }
        }

        public void UpdateByEntity(DivisionViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Division.Single(x => x.DivID == newdata.DivID);
                data.DivCode = newdata.DivCode;
                data.DivName = newdata.DivName;
                data.DivStatus = newdata.DivStatus;
                data.ModifyBy = newdata.ModifyBy;
                data.ModifyDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public int RemoveByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var obj = db.tb_Division.SingleOrDefault(c => c.DivID == id);
                if (obj != null)
                {
                    db.tb_Division.Remove(obj);
                    return db.SaveChanges();
                }
                return 0;
            }
        }

        //public List<sp_GetLastCases_Result> GetLastCases(int p_ctaID, int p_top = 3)
        //{ 
        //    using (SATEntities db = new SATEntities())
        //    { 
        //        var ps = new[] {                    
        //            this.GenObjectParam("p_ctaID", p_ctaID, ParamStyle.General),
        //            this.GenObjectParam("p_top", p_top, ParamStyle.General)
        //        };

        //        var result = this.ExecStoredProcedure<sp_GetLastCases_Result>("sp_GetLastCases", ps);
        //        return result;
   
        //    }        
        //}

    }
}