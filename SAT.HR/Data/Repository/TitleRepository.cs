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
        public TitleResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Title.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.TiFullName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "TiFullName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.TiFullName).ToList() : data.OrderByDescending(x => x.TiFullName).ToList(); break;
                    case "TiShortName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.TiShortName).ToList() : data.OrderByDescending(x => x.TiShortName).ToList(); break;
                    case "SexName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SexName).ToList() : data.OrderByDescending(x => x.SexName).ToList(); break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new TitleViewModel()
                {
                    RowNumber = i + 1,
                    TiID = s.TiID,
                    TiFullName = s.TiFullName,
                    TiShortName = s.TiShortName,
                    TiStatus = s.TiStatus,
                    SexID = s.SexID,
                    SexName = s.SexName,
                }).Skip(start * length).Take(length).ToList();

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