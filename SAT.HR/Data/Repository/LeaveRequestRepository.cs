using SAT.HR.Data.Entities;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity.Core.Objects;
using static SAT.HR.Helpers.EnumType;
using System.Net.Mail;

namespace SAT.HR.Data
{
    public class LeaveRequestRepository
    {
        public LeaveRequestPageResult GetLeaveRequest(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, string year, string status)
        {
            LeaveRequestPageResult result = new LeaveRequestPageResult();
            List<LeaveRequestViewModel> list = new List<LeaveRequestViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    sortBy = (sortBy == "RowNumber") ? "DocNo" : sortBy;
                    string perPage = initialPage.HasValue ? Convert.ToInt32(initialPage) == 0 ? "1" : (Convert.ToInt32(initialPage.ToString().Substring(0, initialPage.ToString().Length - 1)) + 1).ToString() : "1";

                    string requestid = UtilityService.User.UserID.ToString();
                    var data = db.sp_Leave_Request_List(pageSize.ToString(), perPage, sortBy, sortDir, requestid, year, status, filter).ToList();

                    int i = 0;
                    foreach (var item in data)
                    {
                        LeaveRequestViewModel model = new LeaveRequestViewModel();
                        model.RowNumber = ++i;
                        model.FormID = item.FormID;
                        model.RequestID = item.RequestUserID;
                        model.LeaveYear = item.LeaveYear;
                        model.RequestName = item.RequestUserName;
                        model.DocNo = item.DocNo;
                        model.Action = item.Action;
                        model.LeaveTypeName = item.LeaveTypeName;
                        model.StartDateText = item.StartDate.Value.ToString("dd/MM/yyyy");
                        model.EndDateText = item.EndDate.Value.ToString("dd/MM/yyyy");
                        model.CreateDateText = item.CreateDate.Value.ToString("dd/MM/yyyy");
                        model.StatusName = item.Status;
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

        public LeaveRequestPageResult GetLeaveWaiting(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, string year, string status)
        {
            LeaveRequestPageResult result = new LeaveRequestPageResult();
            List<LeaveRequestViewModel> list = new List<LeaveRequestViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    sortBy = (sortBy == "RowNumber") ? "DocNo" : sortBy;
                    string perPage = initialPage.HasValue ? Convert.ToInt32(initialPage) == 0 ? "1" : (Convert.ToInt32(initialPage.ToString().Substring(0, initialPage.ToString().Length - 1)) + 1).ToString() : "1";

                    string loginUserID = UtilityService.User.UserID.ToString();
                    string loginMpID = UtilityService.User.MpID.ToString();
                    var data = db.sp_Leave_Request_Waiting(pageSize.ToString(), perPage, sortBy, sortDir, loginUserID, filter).ToList();

                    int i = 0;
                    foreach (var item in data)
                    {
                        LeaveRequestViewModel model = new LeaveRequestViewModel();
                        model.RowNumber = ++i;
                        model.FormID = item.FormID;
                        model.FormHeaderID = item.FormHeaderID;
                        model.TransCurrentStepID = item.TransCurrentStepID;
                        model.RequestID = item.RequestUserID;
                        model.LeaveYear = item.LeaveYear;
                        model.RequestName = item.RequestUserName;
                        model.DocNo = item.DocNo;
                        model.LeaveTypeName = item.LeaveTypeName;
                        model.StartDateText = item.StartDate.Value.ToString("dd/MM/yyyy");
                        model.EndDateText = item.EndDate.Value.ToString("dd/MM/yyyy");
                        model.CreateDateText = item.CreateDate.Value.ToString("dd/MM/yyyy");
                        model.StatusName = item.Status;
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

                    var item = db.vw_Leave_Request.Where(x => x.FormID == id).FirstOrDefault();
                    if (item != null)
                    {
                        model.FormID = item.FormID;
                        model.DocNo = item.DocNo;
                        model.LeaveYear = item.LeaveYear;
                        model.LeaveType = item.LeaveType;
                        model.RequestID = item.RequestUserID;
                        model.RequestName = item.RequestUserName;
                        model.StartDate = item.StartDate;
                        model.EndDate = item.EndDate;
                        model.DayTime = item.DayTime;
                        model.TotalDay = item.TotalDay;
                        model.LeaveReason = item.LeaveReason;
                        model.CancelReason = item.CancelReason;
                        model.Action = item.Action;
                        model.Approver = item.LastApproverName;
                        model.ApproverComment = item.LastApproverComment;
                        model.StatusName = item.Status;

                        model.PathFile = item.LeaveFile;
                        model.CreateDate = item.CreateDate;
                        model.LeaveTotalDay = item.TotalDay;

                        var leaveBalance = new LeaveBalanceRepository().LeaveBalanceByUser((int)model.RequestID, (int)model.LeaveType);
                        model.LeaveMax = leaveBalance.LevMax;
                        model.LeaveStandard = leaveBalance.LevStandard;
                        model.LeaveUsed = leaveBalance.LevUsed;
                        model.LeaveBalance = leaveBalance.LevBalance;
                        model.LeavePending = leaveBalance.LevPending;
                    }
                    else
                    {
                        model.RequestID = UtilityService.User.UserID;
                        model.RequestName = UtilityService.User.FullNameTh;
                        model.DayTime = 1;
                        model.StartDate = DateTime.Now; //Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH")));
                        model.EndDate = DateTime.Now; //Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy", new System.Globalization.CultureInfo("th-TH")));
                        model.TotalDay = CalculateTotalDay(1, DateTime.Now, DateTime.Now);
                    }

                    return model;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LeaveRequestViewModel GetDetail(int? id)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    LeaveRequestViewModel model = new LeaveRequestViewModel();

                    var item = db.vw_Leave_Request.Where(x => x.FormID == id).FirstOrDefault();
                    if (item != null)
                    {
                        model.FormID = item.FormID;
                        model.FormHeaderID = item.FormHeaderID;
                        model.FormMasterID = (int)item.FormMasterID;
                        model.DocNo = item.DocNo;
                        model.LeaveYear = item.LeaveYear;
                        model.LeaveType = item.LeaveType;
                        model.LeaveTypeName = item.LeaveTypeName;
                        model.RequestID = item.RequestUserID;
                        model.RequestName = item.RequestUserName;
                        model.StartDate = item.StartDate;
                        model.EndDate = item.EndDate;
                        model.DayTime = item.DayTime;
                        model.TotalDay = item.TotalDay;
                        model.LeaveReason = item.LeaveReason;
                        model.CancelReason = item.CancelReason;
                        model.ApproverComment = item.ApproverComment;
                        model.PathFile = item.LeaveFile;
                        model.LeaveTotalDay = item.TotalDay;
                        if (model.DayTime == 1)
                            model.DayTimeName = "ทั้งวัน";
                        if (model.DayTime == 2)
                            model.DayTimeName = "ครึ่งวันเช้า";
                        if (model.DayTime == 3)
                            model.DayTimeName = "ครึ่งวันบ่าย";
                        model.StartDateText = item.StartDate.Value.ToString("dd/MM/yyyy");
                        model.EndDateText = item.EndDate.Value.ToString("dd/MM/yyyy");
                        model.TotalDay = item.TotalDay;
                        model.TransCurrentStepID = item.TransCurrentStepID;
                        model.StepNo = item.CurrentStepNo;
                    }

                    return model;
                }
            }
            catch (Exception)
            {
                throw;
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
                        var model = db.tb_Leave_Request.Single(x => x.FormID == newdata.FormID);
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

        public ResponseData AddByEntity(LeaveRequestViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    int formheaderid = 0;
                    try
                    {
                        var requestUserID = UtilityService.User.UserID;
                        var requestMpID = UtilityService.User.MpID;

                        tb_Leave_Request model = new tb_Leave_Request();
                        model.PathFile = data.PathFile;

                        #region LeaveFile

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

                        #endregion

                        model.DocNo = DocumentNumberRepository.GetNextNumber("LEAVE");
                        model.LeaveYear = DateTime.Now.Year;
                        model.LeaveType = data.LeaveType;
                        model.RequestID = data.RequestID;
                        model.StartDate = data.StartDate;
                        model.EndDate = data.EndDate;
                        model.DayTime = data.DayTime;
                        model.TotalDay = Convert.ToDecimal((data.DayTime == 1) ? 1 : 0.5);
                        model.LeaveReason = data.LeaveReason;
                        model.CreateBy = UtilityService.User.UserID;
                        model.CreateDate = DateTime.Now;
                        model.ModifyBy = UtilityService.User.UserID;
                        model.ModifyDate = DateTime.Now;
                        db.tb_Leave_Request.Add(model);
                        db.SaveChanges();

                        ObjectParameter formHeaderID = new ObjectParameter("FormHeaderID", typeof(int));
                        db.sp_WorkFlow_Create(model.FormID, 1, model.RequestID, requestMpID, formHeaderID);
                        formheaderid = Convert.ToInt32(formHeaderID.Value);


                        int formid = model.FormID;
                        result.ID = formid;
                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }

                    SendEmail(Convert.ToInt32(formheaderid), 0, null);
                    return result;
                }
            }
        }

        public ResponseData Cancel(LeaveRequestViewModel data)
        {
            ResponseData result = new ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_Leave_Request.Single(x => x.FormID == data.FormID);
                    model.CancelReason = data.CancelReason;
                    model.CancelDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();

                    int formheaderid = data.FormHeaderID;
                    int stepno = data.StepNo.HasValue ? (int)data.StepNo : 1;
                    string reason = data.CancelReason;
                    int userid = UtilityService.User.UserID;

                    //[sp_WorkFlow_Cancel] 43, 292, 0, 0,'ยกเลิก'
                    db.sp_WorkFlow_Cancel(formheaderid, userid, stepno, 0, reason);
                    db.SaveChanges();

                    int yearTH = DateTime.Now.Year + 543;
                    var leavebalance = db.tb_Leave_Balance.Where(x => x.UserID == userid && x.LevYear == yearTH && x.LevID == data.LeaveType).FirstOrDefault();
                    if (leavebalance == null)
                    {
                        var insertLeaveBalance = db.sp_Leave_Balance_User(userid, yearTH).ToList();
                        foreach (var item in insertLeaveBalance)
                        {
                            tb_Leave_Balance lb = new tb_Leave_Balance();
                            lb.LevYear = item.LevYear;
                            lb.LevID = item.LevID;
                            lb.UserID = userid;
                            lb.LevStandard = item.LevStandard;
                            //lb.LevMax = item.LevMax;
                            lb.LevUsed = (data.LeaveType == item.LevID) ? item.LevUsed : item.LevUsed;
                            lb.CreateBy = UtilityService.User.UserID;
                            lb.CreateDate = DateTime.Now;
                            lb.ModifyBy = UtilityService.User.UserID;
                            lb.ModifyDate = DateTime.Now;
                            db.tb_Leave_Balance.Add(lb);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        leavebalance.LevUsed = leavebalance.LevUsed + data.TotalDay;
                        db.SaveChanges();
                    }

                    SendEmail(formheaderid, 0, -1);
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData Approve(LeaveRequestViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    //exec [sp_WorkFlow_ApproveStep] 49,263,1,'comment',291,1
                    int userid = UtilityService.User.UserID;
                    db.sp_WorkFlow_ApproveStep(data.FormHeaderID, data.TransCurrentStepID, data.Accept, data.ApproverComment, userid, data.StepNo);
                    db.SaveChanges();

                    if(data.Accept == 1)
                    {
                        int yearTH = DateTime.Now.Year + 543;
                        decimal dayUse = (decimal)data.TotalDay;
                        result = new LeaveBalanceRepository().UpdateLeaveBalance(userid, yearTH, (int)data.LeaveType, dayUse);
                    }

                    SendEmail(data.FormHeaderID, 1, data.Accept);
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
                    var data = db.tb_Leave_Request.Where(x => x.FormID == id).FirstOrDefault();
                    string filename = data.PathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathUploadLeaveFile;

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

        public decimal CalculateTotalDay(int daytime, DateTime startdate, DateTime entdate)
        {
            using (SATEntities db = new SATEntities())
            {
                decimal totalDays = 0;
                if (daytime == 1)
                {
                    DateTime from = startdate; // UtilityService.ConvertDateThai2Eng(Convert.ToDateTime(startdate));
                    DateTime to = entdate; // UtilityService.ConvertDateThai2Eng(Convert.ToDateTime(entdate));

                    from = from.Date;
                    to = to.Date;
                    if (from < to)
                    {

                        var dayDifference = (int)to.Subtract(from).TotalDays;
                        totalDays = Enumerable
                                    .Range(1, dayDifference)
                                    .Select(x => from.AddDays(x))
                                    .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);

                        int year = DateTime.Now.Year;
                        var holidays = db.tb_Holiday.Where(m => m.HolDate.Value.Year == year).ToList();
                        foreach (var item in holidays)
                        {
                            DateTime holDate = Convert.ToDateTime(item.HolDate);
                            if (from <= holDate && holDate <= to)
                                --totalDays;
                        }
                    }
                }
                else
                {
                    totalDays = (decimal)0.5;
                }

                return totalDays;
            }
        }


        #region Send Mail

        private void SendEmail(int formheaderid, int stepno, int? action)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var mailtemplate = new MailTemplateViewModel();
                    int templateid = 0;
                    string mailto = string.Empty;
                    string mailname = string.Empty;

                    var step = db.vw_Trans_Step_Route.Where(m => m.FormHeaderID == formheaderid && m.StepNo == stepno).FirstOrDefault();

                    if (action.HasValue)
                    {
                        if (action == 1)
                        {
                            if (step.IsNotifyAcceptRequestor.HasValue ? (bool)step.IsNotifyAcceptRequestor : false)
                            {
                                templateid = (int)step.NotifyAcceptRequestorTemplateID;
                                var notify = db.sp_Workflow_Notify_GetReqMail(formheaderid).ToList();
                                mailto = notify[0].Email;
                                mailname = notify[0].FirstNameTh + " " + notify[0].LastNameTh;
                            }
                        }
                        else if (action == 2 || action == -1)
                        {
                            if (step.IsNotifyRejectRequestor.HasValue ? (bool)step.IsNotifyRejectRequestor : false)
                            {
                                templateid = (int)step.NotifyRejectRequestorTemplateID;
                                var notify = db.sp_Workflow_Notify_GetReqMail(formheaderid).ToList();
                                mailto = notify[0].Email;
                                mailname = notify[0].FirstNameTh + " " + notify[0].LastNameTh;
                            }
                            if (step.IsNotifyRejectNext.HasValue ? (bool)step.IsNotifyRejectNext : false)
                            {
                                templateid = (int)step.NotifyRejectNextTemplateID;
                                var notify = db.sp_Workflow_Notify_GetCancelMail(formheaderid).ToList();
                                mailto = notify[0].Email;
                                mailname = notify[0].FirstNameTh + " " + notify[0].LastNameTh;
                            }
                        }
                    }
                    else
                    {
                        if (step.IsNotifyAcceptNext.HasValue ? (bool)step.IsNotifyAcceptNext : false)
                        {
                            templateid = (int)step.NotifyAcceptNextTemplateID;
                            var notify = db.sp_Workflow_Notify_GetNextMail(formheaderid).ToList();
                            mailto = notify[0].Email;
                            mailname = notify[0].FirstNameTh + " " + notify[0].LastNameTh;
                        }
                    }

                    mailtemplate = GetMailTemplate(templateid, formheaderid, mailto, mailname);
                    SendMail(mailtemplate, null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private MailTemplateViewModel GetMailTemplate(int templateid, int formheaderid, string mailto, string mailname)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var form = db.vw_Leave_Request.Where(m => m.FormHeaderID == formheaderid).FirstOrDefault();

                    var data = new MailTemplateRepository().GetByID(templateid);
                    MailTemplateViewModel model = new MailTemplateViewModel();
                    model.MailID = data.MailID;
                    model.MailSubject = data.MailSubject;
                    model.MailBody = ReplaceMailTemplate(mailname, data.MailBody, form);
                    model.MailTo = "shanisara555@gmail.com"; //mailto + ";" + data.MailTo;
                    model.MailCCTo = data.MailCCTo;
                    model.MailBCCTo = data.MailBCCTo;
                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ReplaceMailTemplate(string mailname, string mailbody, vw_Leave_Request form)
        {
            string template = mailbody.Replace("[{DocNo}]", form.DocNo)
                                    .Replace("[{CreateDate}]", form.CreateDate.ToString())
                                    .Replace("[{RequestUserName}]", form.RequestUserName)
                                    .Replace("[{LeaveTypeName}]", form.LeaveTypeName)
                                    .Replace("[{TimeType}]", GetTimeType(form.DayTime))
                                    .Replace("[{StartDate}]", form.StartDate.ToString())
                                    .Replace("[{EndDate}]", form.EndDate.ToString())
                                    .Replace("[{TotalDay}]", form.TotalDay.ToString())
                                    .Replace("[{LeaveReason}]", form.LeaveReason)
                                    .Replace("[{CancelReason}]", form.CancelReason)
                                    .Replace("[{NextApproverName}]", form.NextApproverName)
                                    .Replace("[{Action}]", form.Status);

            if(form.Action == "Canceled")
            {
                template = mailbody.Replace("[{NextApproverName}]", mailname);
            }

            return template;
        }

        private MailMessage SendMail(MailTemplateViewModel template, List<string> file)
        {
            try
            {
                #region Mail From/To/Bcc

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SysConfig.SMTPMAILSENDER);

                if (!string.IsNullOrEmpty(template.MailTo))
                {
                    string[] toAddress = template.MailTo.Split(';');
                    foreach (string to in toAddress)
                    {
                        string mailto = to.Trim();

                        if (!string.IsNullOrEmpty(mailto) && Mails.IsValidEmail(mailto))
                        {
                            mail.To.Add(new MailAddress(mailto));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(template.MailCCTo))
                {
                    string[] ccAddress = template.MailCCTo.Split(';');
                    foreach (string ccTo in ccAddress)
                    {
                        string cc = ccTo.Trim();
                        if (!string.IsNullOrEmpty(cc) && Mails.IsValidEmail(cc))
                        {
                            mail.CC.Add(new MailAddress(cc));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(template.MailBCCTo))
                {
                    string[] bccAddress = template.MailBCCTo.Split(';');
                    foreach (string bccTo in bccAddress)
                    {
                        string bcc = bccTo.Trim();
                        if (!string.IsNullOrEmpty(bcc) && Mails.IsValidEmail(bcc))
                        {
                            mail.Bcc.Add(new MailAddress(bcc));
                        }
                    }
                }

                #endregion

                #region Mail Body/Subject

                mail.Subject = template.MailSubject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = template.MailBody;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;

                #endregion

                #region Mail Attachments

                FileStream fileStream;
                if (1 != 1)
                {
                    string fileSavePath = Path.Combine("", "");
                    fileStream = System.IO.File.Open(fileSavePath, FileMode.Open, FileAccess.Read, FileShare.None);
                    fileStream.Close();
                    fileStream.Dispose();

                    Attachment attachment = new Attachment(fileSavePath);
                    mail.Attachments.Add(attachment);
                }

                #endregion

                Mails.SentMail(mail);
                return mail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetTimeType(int? daytime)
        {
            string result = string.Empty;
            if (daytime == 1)
                result = "ทั้งวัน";
            else if (daytime == 2)
                result = "ครึ่งวันเช้า";
            else if (daytime == 3)
                result = "ครึ่งวันบ่าย";

            return result;
        }

        #endregion 

    }
}