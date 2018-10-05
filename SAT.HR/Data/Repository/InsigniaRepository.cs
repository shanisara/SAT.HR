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

    }
}