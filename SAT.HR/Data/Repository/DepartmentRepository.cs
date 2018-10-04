using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class DepartmentRepository
    {
        public DepartmentResult GetDepartment(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Department.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.DepName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "DepName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DepName).ToList() : data.OrderByDescending(x => x.DepName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<DepartmentViewModel> list = new List<Models.DepartmentViewModel>();
                foreach (var m in data)
                {
                    DepartmentViewModel model = new Models.DepartmentViewModel();
                    model.DepID = m.DepID;
                    model.DivID = m.DivID;
                    //model.DepCode = m.DepCode;
                    model.DepName = m.DepName;
                    list.Add(model);
                }

                DepartmentResult result = new DepartmentResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}