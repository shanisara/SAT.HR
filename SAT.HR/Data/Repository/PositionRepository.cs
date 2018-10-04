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
        public PositionResult GetPosition(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Position.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.PoName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "PoName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.PoName).ToList() : data.OrderByDescending(x => x.PoName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<PositionViewModel> list = new List<Models.PositionViewModel>();
                foreach (var m in data)
                {
                    PositionViewModel model = new Models.PositionViewModel();
                    model.PoID = m.PoID;
                    model.PoCode = m.PoCode;
                    model.PoName = m.PoName;
                    model.PoGroup = m.PoGroup;
                    list.Add(model);
                }

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