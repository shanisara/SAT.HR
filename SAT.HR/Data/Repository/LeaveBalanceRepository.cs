using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data
{
    public class LeaveBalanceRepository
    {
        public LeaveBalanceViewModel GetByUser(int userid, int? year)
        {
            var data = new LeaveBalanceViewModel();
            var list = new List<LeaveBalanceViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var user = db.tb_User.Where(x => x.UserID == userid).FirstOrDefault();
                    data.UserID = user.UserID;
                    data.IDCard = user.IDCard;
                    data.FullNameTh = user.FirstNameTh +" " + user.LastNameTh;
                    
                    year = (year == null) ? DateTime.Now.Year + 543 : year;
                    data.LeaveYear = year;

                    var leavebalance = db.sp_Leave_Balance_User(userid, year).ToList();

                    int index = 1;
                    foreach (var item in leavebalance)
                    {
                        LeaveBalanceViewModel model = new LeaveBalanceViewModel();
                        model.RowNumber = index++;
                        model.UserID = data.UserID;
                        model.LevID = item.LevID;
                        model.LevYear = item.LevYear;
                        model.LevName = item.LevName;
                        //model.LevMax = item.LevMax;
                        model.LevStandard = item.LevStandard;
                        model.LevUsed = item.LevUsed;
                        model.LevBalance = item.LevBalance;
                        model.LevPending = item.LevPending;
                        list.Add(model);
                    }
                    data.ListLeaveBalance = list;
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseData SaveByEntity(LeaveBalanceViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        foreach (var item in data.ListLeaveBalance)
                        {
                            var leavebalance = db.tb_Leave_Balance.Where(x => x.UserID == item.UserID && x.LevYear == item.LevYear && x.LevID == item.LevID).FirstOrDefault();
                            if (leavebalance != null)
                            {
                                leavebalance.LevStandard = item.LevStandard;
                                //leavebalance.LevMax = item.LevMax;
                                leavebalance.LevUsed = item.LevUsed;
                                leavebalance.ModifyBy = UtilityService.User.UserID;
                                leavebalance.ModifyDate = DateTime.Now;
                                db.SaveChanges();
                            }
                            else
                            {
                                tb_Leave_Balance model = new tb_Leave_Balance();
                                model.LevYear = item.LevYear;
                                model.LevID = item.LevID;
                                model.UserID = (int)item.UserID;
                                model.LevStandard = item.LevStandard;
                                //model.LevMax = item.LevMax;
                                model.LevUsed = item.LevUsed;
                                model.CreateBy = UtilityService.User.UserID;
                                model.CreateDate = DateTime.Now;
                                model.ModifyBy = UtilityService.User.UserID;
                                model.ModifyDate = DateTime.Now;
                                db.tb_Leave_Balance.Add(model);
                                db.SaveChanges();
                            }
                        }

                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }
                    return result;
                }
            }
        }

        public ResponseData UpdateLeaveBalance(int userid, int year, int leaveid, decimal day)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var leavebalance = db.tb_Leave_Balance.Where(x => x.UserID == userid && x.LevYear == year && x.LevID == leaveid).FirstOrDefault();
                    if(leavebalance == null)
                    {
                        var leave = db.sp_Leave_Balance_User(userid, year).ToList();
                        foreach (var item in leave)
                        {
                            tb_Leave_Balance model = new tb_Leave_Balance();
                            model.UserID = userid;
                            model.LevYear = year;
                            model.LevID = item.LevID;
                            model.LevStandard = item.LevStandard;
                            //model.LevMax = item.LevMax;
                            model.LevUsed = item.LevUsed;
                            model.CreateBy = UtilityService.User.UserID;
                            model.CreateDate = DateTime.Now;
                            model.ModifyBy = UtilityService.User.UserID;
                            model.ModifyDate = DateTime.Now;
                            db.tb_Leave_Balance.Add(model);
                            db.SaveChanges();
                        }
                    }
                    leavebalance.LevUsed = leavebalance.LevUsed + day;
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

        public LeaveBalanceViewModel LeaveBalanceByUser(int userid, int leaveid)
        {
            using (SATEntities db = new SATEntities())
            {
                int year = DateTime.Now.Year + 543;
                var data = db.tb_Leave_Balance.Where(m => m.LevYear == year && m.LevID == leaveid && m.UserID == userid).FirstOrDefault();

                LeaveBalanceViewModel model = new LeaveBalanceViewModel();
                if (data != null)
                {
                    model.LevBalance = data.LevStandard - data.LevUsed;
                    //model.LevMax = data.LevMax;
                    //model.LevStandard = data.LevStandard;
                    //model.LevUsed = data.LevUsed;
                    //model.LevPending = data.LevPending;
                }
                else
                {
                    DateTime curDate = DateTime.Now;
                    var leavetype = db.tb_Leave_Type.Where(m => (m.LevStartDate.Value.Year <= curDate.Year && m.LevStartDate.Value.Month <= curDate.Month && m.LevStartDate.Value.Day <= curDate.Day)
                                    && (m.LevEndDate.Value.Year >= curDate.Year && m.LevEndDate.Value.Month >= curDate.Month && m.LevEndDate.Value.Day >= curDate.Day)
                                    && m.LevID == leaveid).FirstOrDefault();
                    model.LevBalance = leavetype.LevMax;
                    //model.LevMax = leavetype.LevMax;
                    //model.LevStandard = leavetype.LevMax;
                    //model.LevUsed = 0;
                    //model.LevPending = data.LevPending;
                }
                return model;
            }
        }

    }
}