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
                var data = db.tb_Salary.ToList();

                int recordsTotal = data.Count();

                //if (!string.IsNullOrEmpty(filter))
                //{
                //    data = data.Where(x => x.SaStep.Contains(filter)).ToList();
                //}

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "SaLevel":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaLevel).ToList() : data.OrderByDescending(x => x.SaLevel).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<SalaryViewModel> list = new List<Models.SalaryViewModel>();
                foreach (var m in data)
                {
                    SalaryViewModel model = new Models.SalaryViewModel();
                    model.SaID = m.SaID;
                    model.SaLevel = m.SaLevel;
                    model.SaStep = m.SaStep;
                    model.SaStatus = m.SaStatus;
                    list.Add(model);
                }

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