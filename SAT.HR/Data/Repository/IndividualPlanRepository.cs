using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class IndividualPlanRepository
    {
        public IndividualPlanViewModel IndividualPlanByUser(int? userid)
        {
            try
            {
                IndividualPlanViewModel model = new IndividualPlanViewModel();
                using (SATEntities db = new SATEntities())
                {
                    var user = db.vw_Employee.Where(x => x.UserID == userid).FirstOrDefault();
                    model.UserID = user.UserID;
                    model.IDCard = user.IDCard;
                    model.FullNameTh = user.TiShortName + user.FullNameTh;

                    //var cap = db.vw_Capability.Where(x => x.CapID == capid).FirstOrDefault();
                    //model.CapYear = cap.CapYear;
                    //model.CapTName = cap.CapTName;
                    //model.CapGName = cap.CapGName;
                    //model.CapGTName = cap.CapGTName;
                    //model.EvaluationName = cap.CapTName + " / " + cap.CapGName + " / " + cap.CapGTName;

                    //List<EvaluationDetailViewModel> lists = new List<EvaluationDetailViewModel>();
                    //var evaluation = db.sp_Evaluation_GetByUser(userid, capid).ToList();
                    //foreach (var item in evaluation)
                    //{
                    //    EvaluationDetailViewModel eval = new EvaluationDetailViewModel();
                    //    eval.UserID = userid;
                    //    eval.CapDID = item.CapDID;
                    //    eval.CapDName = item.CapDName;
                    //    eval.CapDDesc = item.CapDDesc;
                    //    eval.CapScore1 = item.Score1;
                    //    eval.CapScore2 = item.Score2;
                    //    eval.UserScore1 = item.UserScore1;
                    //    eval.UserScore2 = item.UserScore2;
                    //    lists.Add(eval);
                    //}
                    //model.ListIndividualPlan = lists;

                    return model;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}