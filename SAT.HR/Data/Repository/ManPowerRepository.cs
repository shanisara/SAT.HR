using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class ManPowerRepository
    {
        public List<ManPowerViewModel> GetPositionByDep(int? typeid, int? depid)
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.vw_Man_Power.Where(m => m.TypeID == typeid && m.DepID == depid)
                    .Select(s => new ManPowerViewModel()
                    {
                        MpID = s.MpID,
                        MpCode = s.MpCode,
                        PoID = s.PoID,
                        PoName = s.PoName,
                        FullNameTh = s.FullNameTh
                    }).OrderBy(x => x.MpCode).ToList();
                return list;
            }
        }


    }
}