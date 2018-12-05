using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

                    string requestid = UtilityService.User.UserID.ToString();
                    var data = db.sp_Leave_Request_List(pageSize.ToString(), perPage, sortBy, sortDir, "", year, status, filter).ToList();

                    int i = 0;
                    foreach (var item in data)
                    {
                        LeaveRequestViewModel model = new LeaveRequestViewModel();
                        model.RowNumber = ++i;
                        model.LeaveID = item.LeaveID;
                        model.RequestID = item.RequestID;
                        model.LeaveYear = item.LeaveYear;
                        model.RequestName = item.RequestName;
                        model.LeaveNo = item.LeaveNo;
                        model.LeaveTypeName = item.LeaveTypeName;
                        model.CreateByName = item.CreateByName;
                        model.CreateDateText = item.CreateDate.Value.ToString("dd/MM/yyyy");
                        model.StatusName = item.StatusName;
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

        public List<YearViewModel> GetLeaveYear()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = new List<YearViewModel>();
                lists = db.tb_Leave_Request.GroupBy(g => g.LeaveYear)
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
                        model.LeaveID = item.LeaveID;
                        model.LeaveNo = item.LeaveNo;
                        model.LeaveYear = item.LeaveYear;
                        model.LeaveType = item.LeaveType;
                        model.RequestID = item.RequestID;
                        model.StartDate = item.StartDate;
                        model.EndDate = item.EndDate;
                        model.DayTime = item.DayTime;
                        model.TotalDay = item.TotalDay;
                        model.LeaveReason = item.LeaveReason;
                        model.CancelReason = item.CancelReason;
                        model.Remark = item.Remark;
                        model.Status = item.Status;
                        model.PathFile = item.PathFile;
                        model.CreateDate = item.CreateDate;
                        model.CreateBy = item.CreateBy;
                        model.ModifyDate = item.ModifyDate;
                        model.ModifyBy = item.ModifyBy;
                        model.LeaveTotalDay = item.TotalDay;

                        var leaveBalance = new LeaveBalanceRepository().LeaveBalanceByUser((int)model.RequestID, (int)model.LeaveType);
                        model.LeaveMax = leaveBalance.LevMax;
                        model.LeaveStandard = leaveBalance.LevStandard;
                        model.LeaveUsed = leaveBalance.LevUsed;
                        model.LeaveBalance = leaveBalance.LevBalance;
                    }
                    else
                    {
                        model.RequestID = UtilityService.User.UserID;
                        model.DayTime = 1;
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
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        tb_Leave_Request model = new tb_Leave_Request();
                        model.PathFile = data.PathFile;

                        if (data.LeaveFile != null && data.LeaveFile.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(data.LeaveFile.FileName);
                            var fileExt = System.IO.Path.GetExtension(data.LeaveFile.FileName).Substring(1);

                            string directory = SysConfig.PathUploadLeaveFile;
                            bool isExists = System.IO.Directory.Exists(directory);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(directory);

                            string newFileName = data.RequestID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                            string fileLocation = Path.Combine(directory, newFileName);

                            data.LeaveFile.SaveAs(fileLocation);
                            model.PathFile = newFileName;
                        }

                        model.LeaveID = data.LeaveID;
                        model.LeaveNo = DocumentNumberRepository.GetNextNumber("LEAVE"); 
                        model.LeaveYear = DateTime.Now.Year;
                        model.LeaveType = data.LeaveType;
                        model.RequestID = data.RequestID;
                        model.StartDate = data.StartDate;
                        model.EndDate = data.EndDate;
                        model.DayTime = data.DayTime;
                        model.TotalDay = Convert.ToDecimal((data.DayTime == 1) ? 1 : 0.5);
                        model.LeaveReason = data.LeaveReason;
                        model.Status = (int)EnumType.LeaveStatus.Waiting;
                        model.CreateBy = UtilityService.User.UserID;
                        model.CreateDate = DateTime.Now;
                        model.ModifyBy = UtilityService.User.UserID;
                        model.ModifyDate = DateTime.Now;
                        db.tb_Leave_Request.Add(model);
                        db.SaveChanges();
                        
                        transection.Commit();
                        result.ID = model.LeaveID;
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

        public ResponseData UpdateByEntity(LeaveRequestViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        var model = db.tb_Leave_Request.Single(x => x.LeaveID == newdata.LeaveID);
                        model.PathFile = newdata.PathFile;

                        if (newdata.LeaveFile != null && newdata.LeaveFile.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(newdata.LeaveFile.FileName);
                            var fileExt = System.IO.Path.GetExtension(newdata.LeaveFile.FileName).Substring(1);

                            string directory = SysConfig.PathUploadLeaveFile;
                            bool isExists = System.IO.Directory.Exists(directory);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(directory);

                            string newFileName = newdata.RequestID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                            string fileLocation = Path.Combine(directory, newFileName);

                            newdata.LeaveFile.SaveAs(fileLocation);
                            model.PathFile = newFileName;
                        }

                        model.LeaveType = newdata.LeaveType;
                        model.RequestID = newdata.RequestID;
                        model.StartDate = newdata.StartDate;
                        model.EndDate = newdata.EndDate;
                        model.DayTime = newdata.DayTime;
                        model.TotalDay = newdata.LeaveTotalDay;
                        model.LeaveReason = newdata.LeaveReason;
                        model.ModifyBy = UtilityService.User.UserID;
                        model.ModifyDate = DateTime.Now;
                        db.SaveChanges();

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

        public ResponseData ConfirmCancelByID(int id, string reason)
        {
            ResponseData result = new ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Leave_Request.SingleOrDefault(x => x.LeaveID == id);
                    if (model != null)
                    {
                        model.Status = (int)EnumType.LeaveStatus.Canceled;
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

        public FileViewModel DownloadFileAttach(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Leave_Request.Where(x => x.LeaveID == id).FirstOrDefault();
                    string filename = data.PathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadLeaveFile;

                    model.FileName = filename;
                    model.FilePath = filepath;
                    model.ContentType = Contenttype;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public string CalculateTotalDay(string daytime, string startdate, string entdate)
        {
            string totalDays = "";

            /////ต้องเช็คเพิ่มนะ โดย ไม่คิด วันหยุดทำงาน และ holiday ////

            totalDays = daytime == "1" ? "1" : "0.5";

            return totalDays;
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