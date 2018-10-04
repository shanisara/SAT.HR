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
        public LeaveTypeResult GetLeaveType(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
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
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevYear).ToList() : data.OrderByDescending(x => x.LevYear).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<LeaveTypeViewModel> list = new List<Models.LeaveTypeViewModel>();
                foreach (var m in data)
                {
                    LeaveTypeViewModel model = new Models.LeaveTypeViewModel();
                    model.LevID = m.LevID;
                    model.LevYear = m.LevYear;
                    model.LevStartDate = m.LevStartDate;
                    model.LevEndDate = m.LevEndDate;
                    model.LevMax = m.LevMax;
                    model.LevStatus = m.LevStatus;
                    list.Add(model);
                }

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