using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class CapabilityRepository
    {
        public CapabilityResult GetCapability(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Capability.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.CapYear.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "CapYear":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CapYear).ToList() : data.OrderByDescending(x => x.CapYear).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<CapabilityViewModel> list = new List<Models.CapabilityViewModel>();
                foreach (var m in data)
                {
                    CapabilityViewModel model = new Models.CapabilityViewModel();
                    model.CapID = m.CapID;
                    model.CapYear = m.CapYear;
                    model.CapTID = m.CapTID;
                    model.MenuID = m.MenuID;
                    model.CapGroupID = m.CapGroupID;
                    list.Add(model);
                }

                CapabilityResult result = new CapabilityResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}