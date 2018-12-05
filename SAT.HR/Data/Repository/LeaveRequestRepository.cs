using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SAT.HR.Helpers.EnumType;

namespace SAT.HR.Data
{
    public class LeaveRequestRepository
    {
        public LeaveRequestPageResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, string year, string status)
        {
            LeaveRequestPageResult result = new LeaveRequestPageResult();
            List<LeaveRequestViewModel> list = new List<LeaveRequestViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    sortBy = (sortBy == "RowNumber") ? "LeaveNo" : sortBy;
                    string perPage = initialPage.HasValue ? Convert.ToInt32(initialPage) == 0 ? "1" : (Convert.ToInt32(initialPage.ToString().Substring(0, initialPage.ToString().Length - 1)) + 1).ToString() : "1";
                    //var data = db.sp_LeaveRequest_List(pageSize.ToString(), perPage, sortBy, sortDir, year, status, filter).ToList();

                    int i = 0;
                    //foreach (var item in data)
                    //{
                    //    LeaveRequestViewModel model = new LeaveRequestViewModel();
                    //    model.RowNumber = ++i;
                    //    model.UserID = item.UserID;
                    //    //model.LeaveYear = item.LeaveYear;
                    //    //model.RequestName = item.TiShortName + item.FirstNameTh + " " + item.LastNameTh;
                    //    //model.LeaveNo = item.LeaveNo;
                    //    //model.LeaveTypeName = item.LeaveTypeName;
                    //    //model.CreateDateText = (item.CreateDateText.HasValue) ? item.CreateDateText.Value.ToString("dd/MM/yyyy") : string.Empty;
                    //    //model.StatusName = item.StatusName;
                    //    //model.recordsTotal = (int)item.recordsTotal;
                    //    //model.recordsFiltered = (int)item.recordsFiltered;
                    //    list.Add(model);
                    //}

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

        public List<YearViewModel> GetLeaveYear()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = new List<YearViewModel>();
                lists = db.tb_Leave_Request.GroupBy(g => g.StartDate.Value.Year)
                            .Select(group => new YearViewModel()
                            {
                                Year = group.Key
                            }).OrderByDescending(x => x.Year)
                            .ToList();
                return lists;
            }
        }

        public List<LeaveStatusViewModel> GetLeaveStatus()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = db.tb_Leave_Status
                            .Select(s => new LeaveStatusViewModel()
                            {
                                StatusID = s.StatusID,
                                StatusName = s.StatusName
                            }).ToList();
                return lists;
            }
        }

        public LeaveRequestViewModel GetByID(int? id)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    LeaveRequestViewModel model = new LeaveRequestViewModel();

                    var item = db.tb_Leave_Request.Where(x => x.LeaveID == id).FirstOrDefault();
                    if (item != null)
                    {
                        
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                    }
                    else
                    {
                        model.UserID = UtilityService.User.UserID;
                        model.DayTime = 0;
                        model.StartDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH")));
                        model.EndDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH")));
                        model.TotalDay = 1;
                    }

                    return model;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseData AddByEntity(LeaveRequestViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Leave_Request model = new tb_Leave_Request();

                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Leave_Request.Add(model);
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

        public ResponseData UpdateByEntity(LeaveRequestViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Leave_Request.Single(x => x.LeaveID == newdata.LeaveID);

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

        public ResponseData ConfirmCancelByID(int id, string reason)
        {
            ResponseData result = new ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    int status = (int)LeaveStatus.Canceled;

                    var model = db.tb_Leave_Request.SingleOrDefault(x => x.LeaveID == id);
                    if (model != null)
                    {
                        model.Status = status;
                        model.CancelReason = reason;
                        model.ModifyBy = UtilityService.User.UserID;
                        model.ModifyDate = DateTime.Now;
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


        //public LeaveRequestViewModel GetByUser(int userid)
        //{
        //    var data = new LeaveRequestViewModel();
        //    var list = new List<LeaveRequestViewModel>();
        //    try
        //    {
        //        using (SATEntities db = new SATEntities())
        //        {
        //            int index = 1;
        //            var remuneration = db.tb_Benefit_Remuneration.Where(x => x.UserID == userid).OrderByDescending(o => o.BrID).ToList();

        //            foreach (var item in remuneration)
        //            {
        //                LeaveRequestViewModel model = new LeaveRequestViewModel();
        //                model.RowNumber = index++;
                        
        //                model.CreateDate = item.CreateDate;
        //                model.CreateBy = item.CreateBy;
        //                model.ModifyDate = item.ModifyDate;
        //                model.ModifyBy = item.ModifyBy;
        //                list.Add(model);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    data.UserID = userid;
        //    data.ListLeave = list;
        //    return data;
        //}

    }
}