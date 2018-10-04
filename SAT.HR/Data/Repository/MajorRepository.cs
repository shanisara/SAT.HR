using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class MajorRepository
    {
        public MajorResult GetMajor(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Major.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.MajName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "MajName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.MajName).ToList() : data.OrderByDescending(x => x.MajName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<MajorViewModel> list = new List<Models.MajorViewModel>();
                foreach (var m in data)
                {
                    MajorViewModel model = new Models.MajorViewModel();
                    model.MajID = m.MajID;
                    model.MajName = m.MajName;
                    model.MajStatus = m.MajStatus;
                    list.Add(model);
                }

                MajorResult result = new MajorResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}