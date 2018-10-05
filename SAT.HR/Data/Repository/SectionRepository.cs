﻿using System;
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
                var data = db.vw_Section.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.DivName.Contains(filter) || x.DepName.Contains(filter) || x.SecName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "DivName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DivName).ToList() : data.OrderByDescending(x => x.DivName).ToList();
                        break;
                    case "DepName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DepName).ToList() : data.OrderByDescending(x => x.DepName).ToList();
                        break;
                    case "SecName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SecName).ToList() : data.OrderByDescending(x => x.SecName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((m, i) => new SectionViewModel()
                {
                    RowNumber = i + 1,
                    SecID = m.SecID,
                    SecName = m.SecName,
                    SecStatus = m.SecStatus,
                    DivID = m.DivID,
                    DivName = m.DivName,
                    DepID = m.DepID,
                    DepName = m.DepName,
                }).Skip(start * length).Take(length).ToList();

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