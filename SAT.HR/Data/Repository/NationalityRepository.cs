using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class NationalityRepository
    {
        public NationalityResult GetNationality(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Nationality.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.NatName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "NatName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.NatName).ToList() : data.OrderByDescending(x => x.NatName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new NationalityViewModel()
                {
                    RowNumber = ++i,
                    NatID = s.NatID,
                    NatName = s.NatName,
                    NatStatus = s.NatStatus,
                }).Skip(start * length).Take(length).ToList();

                NationalityResult result = new NationalityResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}