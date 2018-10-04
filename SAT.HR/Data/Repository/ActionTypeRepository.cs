using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class ActionTypeRepository
    {
        public ActionTypeResult GetActionType(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_ActionType.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.ActName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "ActName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ActName).ToList() : data.OrderByDescending(x => x.ActName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new ActionTypeViewModel()
                {
                    RowNumber = ++i,
                    ActID = s.ActID,
                    ActName = s.ActName,
                    ActType = s.ActType
                }).Skip(start * length).Take(length).ToList();


                ActionTypeResult result = new ActionTypeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}