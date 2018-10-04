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
        public HolidayResult GetHoliday(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Holiday.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.HolYear.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "HolYear":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.HolYear).ToList() : data.OrderByDescending(x => x.HolYear).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<HolidayViewModel> list = new List<Models.HolidayViewModel>();
                foreach (var m in data)
                {
                    HolidayViewModel model = new Models.HolidayViewModel();
                    model.HolID = m.HolID;
                    model.HolYear = m.HolYear;
                    model.HolDate = m.HolDate;
                    model.HolDesc = m.HolDescription;
                    list.Add(model);
                }

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