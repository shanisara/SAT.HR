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
        public InsigniaResult GetInsignia(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
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

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<InsigniaViewModel> list = new List<Models.InsigniaViewModel>();
                foreach (var m in data)
                {
                    InsigniaViewModel model = new Models.InsigniaViewModel();
                    model.InsID = m.InsID;
                    model.InsFullName = m.InsFullName;
                    model.InsShortName = m.InsShortName;
                    model.InsStatus = m.InsStatus;
                    list.Add(model);
                }

                InsigniaResult result = new InsigniaResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}