using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class EvaluationRepository
    {
        public EvaluationPageResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, string usertype, string capid)
        {
            EvaluationPageResult result = new EvaluationPageResult();
            List<EvaluationViewModel> list = new List<EvaluationViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    string perPage = initialPage.HasValue ? Convert.ToInt32(initialPage) == 0 ? "1" : initialPage.ToString() : "1";
                    var data = db.sp_Evaluation_List(pageSize.ToString(), perPage, sortBy, sortDir, usertype, capid, filter).ToList();

                    int i = 0;
                    foreach (var item in data)
                    {
                        EvaluationViewModel model = new EvaluationViewModel();
                        model.RowNumber = ++i;
                        model.UserID = item.UserID;
                        model.IDCard = item.IDCard;
                        model.FullNameTh = item.FullNameTh;
                        model.PoName = item.PoName;
                        model.recordsTotal = (int)item.recordsTotal;
                        model.recordsFiltered = (int)item.recordsFiltered;
                        list.Add(model);
                    }

                    result.draw = draw ?? 0;
                    result.recordsTotal = list.Count != 0 ? list[0].recordsTotal : 0;
                    result.recordsFiltered = list.Count != 0 ? list[0].recordsFiltered : 0;
                    result.data = list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public EvaluationViewModel EvaluationByUser(int userid, int capid)
        {
            try
            {
                EvaluationViewModel model = new EvaluationViewModel();
                using (SATEntities db = new SATEntities())
                {
                    var user = db.vw_Employee.Where(x => x.UserID == userid).FirstOrDefault();
                    model.UserID = user.UserID;
                    model.IDCard = user.IDCard;
                    model.FullNameTh = user.TiShortName + user.FullNameTh;

                    var cap = db.vw_Capability.Where(x => x.CapID == capid).FirstOrDefault();
                    model.CapYear = cap.CapYear;
                    model.CapTName = cap.CapTName;
                    model.CapGName = cap.CapGName;
                    model.CapGTName = cap.CapGTName;
                    model.EvaluationName = cap.CapTName + "/" + cap.CapGName + "/" + cap.CapGTName;

                    var evaluation = db.sp_Evaluation_GetByUser(userid, capid).Select(s => new EvaluationDetailViewModel
                    {
                        CapDID = s.CapDID,
                        CapDName = s.CapDName,
                        CapDDesc = s.CapDDesc,
                        CapScore1 = s.Score1,
                        CapScore2 = s.Score2,
                        UserScore1 = s.UserScore1,
                        UserScore2 = s.UserScore2
                    })
                    .ToList();
                    model.ListEvaluation = evaluation;

                    return model;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}