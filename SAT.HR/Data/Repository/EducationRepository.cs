using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class EducationRepository
    {
        public EducationResult GetEducation(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Education.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.EduName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "EduName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.EduName).ToList() : data.OrderByDescending(x => x.EduName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new EducationViewModel()
                {
                    RowNumber = ++i,
                    EduID = s.EduID,
                    EduName = s.EduName,
                    EduStatus = s.EduStatus,
                }).Skip(start * length).Take(length).ToList();

                EducationResult result = new EducationResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}