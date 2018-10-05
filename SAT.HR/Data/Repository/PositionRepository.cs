using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class PositionRepository
    {
        public PositionResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Position.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.PoCode.Contains(filter) || x.PoName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "PoCode":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.PoCode).ToList() : data.OrderByDescending(x => x.PoCode).ToList();
                        break;
                    case "PoName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.PoName).ToList() : data.OrderByDescending(x => x.PoName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new PositionViewModel()
                {
                    RowNumber = i + 1,
                    PoID = s.PoID,
                    PoCode = s.PoCode,
                    PoName = s.PoName,
                    PoStatus = s.PoStatus
                }).Skip(start * length).Take(length).ToList();

                PositionResult result = new PositionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}