using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data
{
    public class TimeAttendanceRepository
    {
        public TimeAttendancePageViewModel GetTimeAttendanceByUser(int userid, string datefrom, string dateto, int? type)
        {
            var data = new TimeAttendancePageViewModel();
            var list = new List<TimeAttendancePageViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;

                    int fromdate = Convert.ToInt32(UtilityService.ConvertDateThai2Eng(Convert.ToDateTime(datefrom)).ToString("yyyyMMdd"));
                    int todate = Convert.ToInt32(UtilityService.ConvertDateThai2Eng(Convert.ToDateTime(dateto)).ToString("yyyyMMdd"));
                    var timeattendance = db.sp_Time_Attendance_User(userid, type, fromdate, todate).ToList();

                    foreach (var item in timeattendance)
                    {
                        TimeAttendancePageViewModel model = new TimeAttendancePageViewModel();
                        model.RowNumber = index++;
                        model.TaID = item.TaID;
                        model.UserID = item.UserID;
                        model.TaTName = item.TaTName;
                        model.ActTimeIn = item.Act_TimeIn.Value.ToString("dd/MM/yyyy HH:mm");
                        model.StdTimeIn = item.Std_TimeIn.Value.ToString("dd/MM/yyyy HH:mm");
                        model.ActTimeOut = item.Act_TimeOut.Value.ToString("dd/MM/yyyy HH:mm");
                        model.StdTimeOut = item.Std_TimeOut.Value.ToString("dd/MM/yyyy HH:mm");
                        model.AdjTimeIn = item.Adj_TimeIn.Value.ToString("dd/MM/yyyy HH:mm");
                        model.AdjTimeOut = item.Adj_TimeOut.Value.ToString("dd/MM/yyyy HH:mm");
                        model.Remark = item.Remark;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListTimeAttendance = list;
            return data;
        }

        public TimeAttendanceViewModel GetTimeAttendanceByID(int? id, int userid)
        {
            TimeAttendanceViewModel data = new TimeAttendanceViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var user = db.tb_User.Where(x => x.UserID == userid).FirstOrDefault();
                    var wtid = user.WorkingTypeID;

                    string currday = DateTime.Now.ToString("dddd"); 
                    var workingshift = db.tb_Working_Time.Where(x => x.WID == wtid && x.WorkDay == currday).FirstOrDefault();

                    var item = db.tb_Time_Attendance.Where(x => x.TaID == id).FirstOrDefault();
                    TimeAttendanceViewModel model = new TimeAttendanceViewModel();
                    model.UserID = userid;
                    if (item != null)
                    {
                        model.TaID = item.TaID;
                        model.UserID = item.UserID;
                        model.TaTID = item.TaTID;
                        model.Act_TimeIn = item.Act_TimeIn;
                        model.Act_TimeOut = item.Act_TimeOut;
                        model.Adj_TimeIn = item.Adj_TimeIn;
                        model.Adj_TimeOut = item.Adj_TimeOut;
                        model.Std_TimeIn = item.Std_TimeIn;
                        model.Std_TimeOut = item.Std_TimeOut;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                    }
                    else
                    {
                        model.Std_TimeIn = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") +" "+ workingshift.StartTime);
                        model.Std_TimeOut = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + workingshift.EndTime);
                    }
                    
                    data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddByEntity(TimeAttendanceViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Time_Attendance model = new tb_Time_Attendance();
                    model.TaID = data.TaID;
                    model.UserID = data.UserID;
                    model.TaTID = data.TaTID;
                    model.Act_TimeIn = data.Act_TimeIn;
                    model.Act_TimeOut = data.Act_TimeOut;
                    model.Adj_TimeIn = data.Adj_TimeIn;
                    model.Adj_TimeOut = data.Adj_TimeOut;
                    model.Std_TimeIn = data.Std_TimeIn;
                    model.Std_TimeOut = data.Std_TimeOut;
                    model.CreateDate = DateTime.Now;
                    model.CreateBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    db.tb_Time_Attendance.Add(model);
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

        public ResponseData UpdateByEntity(TimeAttendanceViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Time_Attendance.Single(x => x.UserID == newdata.UserID && x.TaID == newdata.TaID);
                    model.TaID = newdata.TaID;
                    model.UserID = newdata.UserID;
                    model.TaTID = newdata.TaTID;
                    model.Act_TimeIn = newdata.Act_TimeIn;
                    model.Act_TimeOut = newdata.Act_TimeOut;
                    model.Adj_TimeIn = newdata.Adj_TimeIn;
                    model.Adj_TimeOut = newdata.Adj_TimeOut;
                    //model.Std_TimeIn = newdata.Std_TimeIn;
                    //model.Std_TimeOut = newdata.Std_TimeOut;
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
                    var model = db.tb_Time_Attendance.SingleOrDefault(x => x.TaID == id);
                    if (model != null)
                    {
                        db.tb_Time_Attendance.Remove(model);
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