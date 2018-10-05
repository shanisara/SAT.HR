using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class LeaveTypeRepository
    {
        public LeaveTypeResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_LeaveType.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.LevYear.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "LevYear":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevYear).ToList() : data.OrderByDescending(x => x.LevYear).ToList(); break;
                    case "LevName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevName).ToList() : data.OrderByDescending(x => x.LevName).ToList(); break;
                    case "LevStartDateText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevStartDate).ToList() : data.OrderByDescending(x => x.LevStartDate).ToList(); break;
                    case "LevEndDateText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevEndDate).ToList() : data.OrderByDescending(x => x.LevEndDate).ToList(); break;
                    case "LevMax":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevMax).ToList() : data.OrderByDescending(x => x.LevMax).ToList(); break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new LeaveTypeViewModel()
                {
                    RowNumber = ++i,
                    LevID = s.LevID,
                    LevYear = s.LevYear,
                    LevName = s.LevName,
                    LevStartDateText = s.LevStartDate.Value.ToString("dd/MM/yyyy"),
                    LevEndDateText = s.LevEndDate.Value.ToString("dd/MM/yyyy"),
                    LevMax = s.LevMax,
                    LevStatus = s.LevStatus,
                }).Skip(start * length).Take(length).ToList();

                LeaveTypeResult result = new LeaveTypeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}