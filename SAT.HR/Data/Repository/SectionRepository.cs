using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class SectionRepository
    {
        public SectionResult GetSection(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Section.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.SecName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "SecName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SecName).ToList() : data.OrderByDescending(x => x.SecName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<SectionViewModel> list = new List<Models.SectionViewModel>();
                foreach (var m in data)
                {
                    SectionViewModel model = new Models.SectionViewModel();
                    model.SecID = m.SecID;
                    //model.SecCode = m.SecCode;
                    model.SecName = m.SecName;
                    model.SecStatus = m.SecStatus;
                    list.Add(model);
                }

                SectionResult result = new SectionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}