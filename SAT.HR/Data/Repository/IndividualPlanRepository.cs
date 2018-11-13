using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class IndividualPlanRepository
    {
        public IndividualPlanViewModel GetIndividualPlanByID(int userid)
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

                    //    var item = db.tb_IndividualPlan.Where(x => x.PlanID == id).FirstOrDefault();
                    //    if (item != null)
                    //    {
                    //        model.PlanID = item.PlanID;
                    //        model.UserID = item.UserID;

                    //        model.CreateDate = item.CreateDate;
                    //        model.CreateBy = item.CreateBy;
                    //        model.ModifyDate = item.ModifyDate;
                    //        model.ModifyBy = item.ModifyBy;
                    //    }
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IndividualPlanViewModel IndividualPlanByUser(int? userid)
        {
            try
            {
                IndividualPlanViewModel model = new IndividualPlanViewModel();

                using (SATEntities db = new SATEntities())
                {
                    List<IndividualPlanViewModel> lists = new List<IndividualPlanViewModel>();
                    var items = db.tb_IndividualPlan.Where(x => x.UserID == userid).ToList();
                    if (items.Count > 0)
                    {
                        int index = 0;
                        foreach (var item in items)
                        {
                            IndividualPlanViewModel obj = new IndividualPlanViewModel();
                            obj.RowNumber = ++index;
                            obj.PlanID = item.PlanID;
                            obj.UserID = item.UserID;
                            obj.PlanName = item.PlanName;
                            obj.PlanDesc = item.PlanDesc;
                            obj.StartDate = item.StartDate;
                            obj.EndDate = item.EndDate;
                            obj.Amount = item.Amount;
                            obj.CreateDate = item.CreateDate;
                            obj.CreateBy = item.CreateBy;
                            obj.ModifyDate = item.ModifyDate;
                            obj.ModifyBy = item.ModifyBy;
                            obj.StartDateText = item.StartDate.Value.ToString("dd/MM/yyyy");
                            obj.EndDateText = item.EndDate.Value.ToString("dd/MM/yyyy");
                            obj.CreateDateText = item.CreateDate.Value.ToString("dd/MM/yyyy");
                            lists.Add(obj);
                        }
                    }
                    model.ListIndividualPlan = lists;

                    return model;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IndividualPlanViewModel IndividualPlanDetailByID(int? id, int userid)
        {
            try
            {
                IndividualPlanViewModel model = new IndividualPlanViewModel();

                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_IndividualPlan.Where(x => x.PlanID == id).FirstOrDefault();
                    if (obj != null)
                    {
                        model.PlanID = obj.PlanID;
                        model.UserID = obj.UserID;
                        model.PlanName = obj.PlanName;
                        model.PlanDesc = obj.PlanDesc;
                        model.StartDate = obj.StartDate;
                        model.EndDate = obj.EndDate;
                        model.Amount = obj.Amount;
                        model.CreateDate = obj.CreateDate;
                        model.CreateBy = obj.CreateBy;
                        model.ModifyDate = obj.ModifyDate;
                        model.ModifyBy = obj.ModifyBy;
                    }
                    else
                    {
                        model.UserID = userid;
                    }
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseData AddIndividualPlanDetail(IndividualPlanViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_IndividualPlan model = new tb_IndividualPlan();
                    model.PlanID = data.PlanID;
                    model.UserID = data.UserID;
                    model.PlanName = data.PlanName;
                    model.PlanDesc = data.PlanDesc;
                    model.StartDate = data.StartDate;
                    model.EndDate = data.EndDate;
                    model.Amount = data.Amount;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_IndividualPlan.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateIndividualPlanDetail(IndividualPlanViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_IndividualPlan.Single(x => x.UserID == newdata.UserID && x.PlanID == newdata.PlanID);
                    model.PlanID = newdata.PlanID;
                    model.UserID = newdata.UserID;
                    model.PlanName = newdata.PlanName;
                    model.PlanDesc = newdata.PlanDesc;
                    model.StartDate = newdata.StartDate;
                    model.EndDate = newdata.EndDate;
                    model.Amount = newdata.Amount;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteIndividualPlanDetail(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_IndividualPlan.SingleOrDefault(x => x.PlanID == id);
                    if (model != null)
                    {
                        db.tb_IndividualPlan.Remove(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }
    }
}