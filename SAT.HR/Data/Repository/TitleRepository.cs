using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class TitleRepository
    {
        public TitleResult GetTitle(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Title.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.TiFullName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "TiFullName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.TiFullName).ToList() : data.OrderByDescending(x => x.TiFullName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<TitleViewModel> list = new List<Models.TitleViewModel>();
                foreach (var m in data)
                {
                    TitleViewModel model = new Models.TitleViewModel();
                    model.TiID = m.TiID;
                    model.TiFullName = m.TiFullName;
                    model.TiShortName = m.TiShortName;
                    model.TiStatus = m.TiStatus;
                    list.Add(model);
                }

                TitleResult result = new TitleResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}