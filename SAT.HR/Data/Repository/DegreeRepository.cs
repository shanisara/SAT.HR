using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class DegreeRepository
    {
        public DegreeResult GetDegree(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Degree.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.DegName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "DegName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DegName).ToList() : data.OrderByDescending(x => x.DegName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<DegreeViewModel> list = new List<Models.DegreeViewModel>();
                foreach (var m in data)
                {
                    DegreeViewModel model = new Models.DegreeViewModel();
                    model.DegID = m.DegID;
                    model.DegName = m.DegName;
                    model.DegStatus = m.DegStatus;
                    list.Add(model);
                }

                DegreeResult result = new DegreeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}