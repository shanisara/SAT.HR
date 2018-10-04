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
        public ReligionResult GetReligion(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
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

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<ReligionViewModel> list = new List<Models.ReligionViewModel>();
                foreach (var m in data)
                {
                    ReligionViewModel model = new Models.ReligionViewModel();
                    model.RelD = m.RelD;
                    model.RelName = m.RelName;
                    model.RelStatus = m.RelStatus;
                    list.Add(model);
                }

                ReligionResult result = new ReligionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}