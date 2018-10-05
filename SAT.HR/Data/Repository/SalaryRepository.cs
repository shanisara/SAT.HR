using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class SalaryRepository
    {
        public SalaryResult GetSalary(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_SalaryRate.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.SaLevel.ToString().Contains(filter) || x.SaStep.ToString().Contains(filter) || x.SaRate.ToString().Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "SaLevel":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaLevel).ToList() : data.OrderByDescending(x => x.SaLevel).ToList();
                        break;
                    case "SaStep":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaStep).ToList() : data.OrderByDescending(x => x.SaStep).ToList();
                        break;
                    case "saRate":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaRate).ToList() : data.OrderByDescending(x => x.SaRate).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new SalaryViewModel()
                {
                    RowNumber = i + 1,
                    SaID = s.SaID,
                    SaLevel = s.SaLevel,
                    SaStep = s.SaStep,
                    SaRate = s.SaRate
                }).Skip(start * length).Take(length).ToList();


                SalaryResult result = new SalaryResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}