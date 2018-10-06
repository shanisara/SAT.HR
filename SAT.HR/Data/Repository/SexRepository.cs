using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class SexRepository
    {
        public List<SexViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Sex.Select(s => new SexViewModel()
                {
                    SexID = s.SexID,
                    SexName = s.SexName
                }).OrderBy(x => x.SexName).ToList();
                return list;
            }
        }

    }
}