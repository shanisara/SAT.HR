using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class TrainingRepository
    {
        public List<TrainingTypeViewModel> GetTrainingType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Training_Type.Select(s => new TrainingTypeViewModel()
                {
                    TrainingTypeID = s.TtID,
                    TrainingTypeName = s.TtName
                }).ToList();
                return list;
            }
        }

    }
}