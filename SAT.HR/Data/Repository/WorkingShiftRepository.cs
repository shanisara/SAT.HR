using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data
{
    public class WorkingShiftRepository
    {
        public WorkingShiftPageViewModel GetWorkingShiftByUser(int userid, string datefrom, string dateto)
        {
            var data = new WorkingShiftPageViewModel();
            var list = new List<WorkingShiftPageViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;

                    int fromdate = Convert.ToInt32(UtilityService.ConvertDateThai2Eng(Convert.ToDateTime(datefrom)).ToString("yyyyMMdd"));
                    int todate = Convert.ToInt32(UtilityService.ConvertDateThai2Eng(Convert.ToDateTime(dateto)).ToString("yyyyMMdd"));
                    var workingshift = db.sp_Working_Shift_User(userid, fromdate, todate).ToList();

                    foreach (var item in workingshift)
                    {
                        WorkingShiftPageViewModel model = new WorkingShiftPageViewModel();
                        model.RowNumber = index++;
                        model.WsID = item.WsID;
                        model.UserID = item.UserID;
                        model.WsYear = item.WsYear;
                        model.WsMonth = item.WsMonthName;
                        model.ActTimeIn = item.Act_TimeIn.Value.ToString("dd/MM/yyyy HH:mm");
                        model.ActTimeOut = item.Act_TimeOut.Value.ToString("dd/MM/yyyy HH:mm");
                        //model.AdjTimeIn = item.Adj_TimeIn.Value.ToString("dd/MM/yyyy HH:mm");
                        //model.AdjTimeOut = item.Adj_TimeOut.Value.ToString("dd/MM/yyyy HH:mm");
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListWorkingShift = list;
            return data;
        }

        public WorkingShiftViewModel GetWorkingShiftByID(int? id, int userid)
        {
            WorkingShiftViewModel data = new WorkingShiftViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var item = db.tb_Working_Shift.Where(x => x.WsID == id).FirstOrDefault();

                    WorkingShiftViewModel model = new WorkingShiftViewModel();
                    model.UserID = userid;

                    if (item != null)
                    {
                        model.WsID = item.WsID;
                        model.UserID = item.UserID;
                        model.WsYear = item.WsYear;
                        model.WsMonth = item.WsMonth;
                        model.Act_TimeIn = item.Act_TimeIn;
                        model.Act_TimeOut = item.Act_TimeOut;
                        model.Adj_TimeIn = item.Adj_TimeIn;
                        model.Adj_TimeOut = item.Adj_TimeOut;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                    }
                    else
                    {
                        model.WsYear = DateTime.Now.Year + 543;
                        model.WsMonth = DateTime.Now.Month;
                    }

                    data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddByEntity(WorkingShiftViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Working_Shift model = new tb_Working_Shift();
                    model.WsID = data.WsID;
                    model.UserID = (int)data.UserID;
                    model.WsYear = data.WsYear;
                    model.WsMonth = data.WsMonth;
                    model.Act_TimeIn = data.Act_TimeIn;
                    model.Act_TimeOut = data.Act_TimeOut;
                    model.Adj_TimeIn = data.Adj_TimeIn;
                    model.Adj_TimeOut = data.Adj_TimeOut;
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    db.tb_Working_Shift.Add(model);
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

        public ResponseData UpdateByEntity(WorkingShiftViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Working_Shift.Single(x => x.UserID == newdata.UserID && x.WsID == newdata.WsID);
                    model.WsYear = newdata.WsYear;
                    model.WsMonth = newdata.WsMonth;
                    model.Act_TimeIn = newdata.Act_TimeIn;
                    model.Act_TimeOut = newdata.Act_TimeOut;
                    model.Adj_TimeIn = newdata.Adj_TimeIn;
                    model.Adj_TimeOut = newdata.Adj_TimeOut;
                    model.ModifyDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
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

        public ResponseData DeleteByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Working_Shift.SingleOrDefault(x => x.WsID == id);
                    if (model != null)
                    {
                        db.tb_Working_Shift.Remove(model);
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