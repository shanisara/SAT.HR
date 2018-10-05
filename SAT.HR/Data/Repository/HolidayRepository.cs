using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class HolidayRepository
    {
        public HolidayResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Holiday.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.HolDescription.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "HolDescription":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.HolDescription).ToList() : data.OrderByDescending(x => x.HolDescription).ToList();
                        break;
                    case "HolDateText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.HolDate).ToList() : data.OrderByDescending(x => x.HolDate).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new HolidayViewModel()
                {
                    RowNumber = ++i,
                    HolID = s.HolID,
                    HolDate = s.HolDate,
                    HolDescription = s.HolDescription,
                    HolYear = s.HolDate.Value.Year,
                    HolDateText = s.HolDate.Value.ToString("dd/MM/yyyy"),
                }).Skip(start * length).Take(length).ToList();

                HolidayResult result = new HolidayResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}