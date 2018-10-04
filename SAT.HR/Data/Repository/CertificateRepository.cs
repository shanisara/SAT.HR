using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class CertificateRepository
    {
        public CertificateResult GetCertificate(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Certificate.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.CerName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "CerName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CerName).ToList() : data.OrderByDescending(x => x.CerName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / 10 : 0;
                int length = pageSize ?? 10;
                data = data.Skip(start * length).Take(length).ToList();

                List<CertificateViewModel> list = new List<Models.CertificateViewModel>();
                foreach (var m in data)
                {
                    CertificateViewModel model = new Models.CertificateViewModel();
                    model.CerId = m.CerId;
                    model.CerCode = m.CerCode;
                    model.CerName = m.CerName;
                    list.Add(model);
                }

                CertificateResult result = new CertificateResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

    }
}