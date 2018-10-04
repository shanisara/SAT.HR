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
                data = data.Skip(start * length).Take(length).ToList();

                List<ActionTypeViewModel> list = new List<Models.ActionTypeViewModel>();
                foreach (var m in data)
                {
                    ActionTypeViewModel model = new Models.ActionTypeViewModel();
                    model.ActID = m.ActID;
                    model.ActCode = m.ActCode;
                    model.ActName = m.ActName;
                    model.ActType = m.ActType;
                    list.Add(model);
                }

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