using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class PositionTransferRepository
    {
        public PositionTransferResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, int? userType)
        {
            PositionTransferResult result = new PositionTransferResult();
            List<PositionTransferViewModel> list = new List<PositionTransferViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_Move_Man_Power_Head.Where(x => x.UserTID == userType).ToList();

                    int recordsTotal = data.Count();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        data = data.Where(x => x.MopYear.Contains(filter) || x.EmpTName.Contains(filter) || x.MtName.Contains(filter) || x.MopBookCmd.Contains(filter)).ToList();
                    }

                    int recordsFiltered = data.Count();

                    switch (sortBy)
                    {
                        case "MopYear":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MopYear).ToList() : data.OrderByDescending(x => x.MopYear).ToList(); break;
                        case "EmpTName":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.EmpTName).ToList() : data.OrderByDescending(x => x.EmpTName).ToList(); break;
                        case "MtName":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MtName).ToList() : data.OrderByDescending(x => x.MtName).ToList(); break;
                        case "MopBookCmd":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MopBookCmd).ToList() : data.OrderByDescending(x => x.MopBookCmd).ToList(); break;
                        case "MopTotal":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MopTotal).ToList() : data.OrderByDescending(x => x.MopTotal).ToList(); break;
                        case "MopDateCmdText":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MopDateCmd).ToList() : data.OrderByDescending(x => x.MopDateCmd).ToList(); break;
                        case "MopStatusName":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MopStatus).ToList() : data.OrderByDescending(x => x.MopStatus).ToList(); break;

                    }

                    int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                    int length = pageSize ?? 10;

                    int i = 1;
                    foreach (var item in data)
                    {
                        PositionTransferViewModel model = new PositionTransferViewModel();
                        model.RowNumber = ++i;
                        model.MopID = item.MopID;
                        model.MopYear = item.MopYear;
                        model.UserTName = item.EmpTName;
                        model.MtName = item.MtName;
                        model.MopBookCmd = item.MopBookCmd;
                        model.MopTotal = item.MopTotal;
                        model.MopDateCmdText = (item.MopDateCmd.HasValue) ? item.MopDateCmd.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.CreateDateText = item.CreateDate.Value.ToString("dd/MM/yyyy");
                        model.MopStatus = item.MopStatus.HasValue ? (item.MopStatus == 0 ? false : true) : false;
                        model.MopStatusName = (item.MopStatus == 0) ? "รอยืนยัน" : "ยืนยันแล้ว";
                        model.recordsTotal = recordsTotal;
                        model.recordsFiltered = recordsFiltered;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            result.draw = draw ?? 0;
            result.recordsTotal = list.Count() > 0 ? list[0].recordsTotal : 0;
            result.recordsFiltered = list.Count() > 0 ? list[0].recordsFiltered : 0;
            result.data = list;

            return result;
        }

        public PositionTransferViewModel GetByID(int? id, int? type)
        {
            PositionTransferViewModel model = new PositionTransferViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    if (id != null)
                    {
                        var data = db.tb_Move_Man_Power_Head.Where(x => x.MopID == id).FirstOrDefault();
                        PositionTransferViewModel head = new PositionTransferViewModel();
                        model.MopID = data.MopID;
                        model.UserTID = data.UserTID;
                        model.MtID = data.MtID;
                        model.MopYear = data.MopYear;
                        model.MopBookCmd = data.MopBookCmd;
                        model.MopDateCmd = data.MopDateCmd;
                        model.MopDateEff = data.MopDateEff;
                        model.MopDateCmdText = data.MopDateCmd.HasValue ? data.MopDateCmd.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.MopDateEffText = data.MopDateEff.HasValue ? data.MopDateEff.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.MopSignatory = data.MopSignatory;
                        model.MopPathFile = data.MopPathFile;
                        model.MopStatus = data.MopApproveStatus == null ? false : data.MopApproveStatus;

                        var detail = GetDetail(model.MopID);
                        model.ListDetail = detail;
                    }
                    else
                    {
                        model.MopYear = DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("th-TH")).ToString();
                        model.MopStatus = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public PositionTransferDetailViewModel GetDetailByID(int? id)
        {
            PositionTransferDetailViewModel model = new PositionTransferDetailViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_Move_Man_Power_Detail.Where(x => x.MopID == id).Select(s => new PositionTransferDetailViewModel
                    {
                        MopID = s.MopID,
                        UserID = s.UserID,
                        CurMpID = s.CurMpID,
                        MovMpID = s.MovMpID,
                        AgentMpID = s.AgentMpID,
                        AgentPoTID = s.AgentPoTID,
                        CurrentPo = s.CurPoName,
                        MovePo = s.MovPoName,
                        AgentPo = s.AgentPoName,
                        MovRemark = s.MovRemark
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public List<PositionTransferDetailViewModel> GetDetail(int? id)
        {
            var list = new List<PositionTransferDetailViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var detail = db.vw_Move_Man_Power_Detail.Where(x => x.MopID == id).ToList();

                    foreach (var item in detail)
                    {
                        PositionTransferDetailViewModel model = new PositionTransferDetailViewModel();
                        model.RowNumber = index++;
                        model.MopID = item.MopID;
                        model.UserID = item.UserID;
                        model.FullName = item.FullName;
                        model.CurMpID = item.CurMpID;
                        model.CurPoName = item.CurPoName;
                        model.MovMpID = item.MovMpID;
                        model.MovPoName = item.MovPoName;
                        model.AgentPoTID = item.AgentPoTID;
                        model.AgentPoTName = item.AgentPoTName;
                        model.AgentMpID = item.AgentMpID;
                        model.AgentPoName = item.AgentPoName;
                        model.MovRemark = item.MovRemark;
                        model.CurrentPo = (item.CurMpID != null) ? "(" + item.CurMpID + ") " + item.CurPoName : string.Empty;
                        model.MovePo = (item.MovMpID != null) ? "(" + item.MovMpID + ") " + item.MovPoName : string.Empty;
                        if (item.AgentMpID != null)
                        {
                            model.AgentPo = item.AgentPoTName + " (" + item.AgentMpID + ") " + item.AgentPoName;
                            //model.BelongTo = item.DivName + (!string.IsNullOrEmpty(item.DepName) ? " / " + item.DepName : string.Empty) + (!string.IsNullOrEmpty(item.SecName) ? " / " + item.SecName : string.Empty);
                        }
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public ResponseData AddByEntity(PositionTransferViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new ResponseData();
                    try
                    {
                        tb_Move_Man_Power_Head head = new tb_Move_Man_Power_Head();

                        if (data.fileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = data.fileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadPositionTransfer;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = " คส.ที่ " + data.MopBookCmd + " เรื่องการโยกย้ายอัตรากำลังพล" + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.MopPathFile = newFileName;
                            }
                        }
                        
                        head.UserTID = data.UserTID;
                        head.MtID = data.MtID;
                        head.MopYear = data.MopYear;
                        head.MopBookCmd = data.MopBookCmd;
                        if (data.MopDateCmd.HasValue)
                            head.MopDateCmd = UtilityService.ConvertDate2Save(data.MopDateCmd);
                        if (data.MopDateEff.HasValue)
                            head.MopDateEff = UtilityService.ConvertDate2Save(data.MopDateEff);
                        head.MopSignatory = data.MopSignatory;
                        //head.MopStatus = data.MopStatus;
                        head.CreateBy = UtilityService.User.UserID;
                        head.CreateDate = DateTime.Now;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.tb_Move_Man_Power_Head.Add(head);
                        db.SaveChanges();
                        result.ID = head.MopID;

                        if (data.ListDetail != null)
                        {
                            foreach (var item in data.ListDetail)
                            {
                                tb_Move_Man_Power_Detail detail = new tb_Move_Man_Power_Detail();
                                detail.MopID = head.MopID;
                                detail.UserID = item.UserID;
                                detail.MopOldPoID = item.CurMpID;
                                detail.MopNewPoID = item.MovMpID;
                                detail.PoTAgentID = item.AgentPoTID;
                                detail.AgentMpID = item.AgentMpID;
                                detail.MovRemark = item.MovRemark;
                                detail.CreateBy = UtilityService.User.UserID;
                                detail.CreateDate = DateTime.Now;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Move_Man_Power_Detail.Add(detail);
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

        public ResponseData UpdateByEntity(PositionTransferViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new ResponseData();
                    try
                    {
                        var head = db.tb_Move_Man_Power_Head.Single(x => x.MopID == newdata.MopID);

                        if (newdata.fileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = newdata.fileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadPositionTransfer;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = "คส.ที่ " + newdata.MopBookCmd + " เรื่องการโยกย้ายอัตรากำลังพล" + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.MopPathFile = newFileName;
                            }
                        }
                        
                        head.UserTID = newdata.UserTID;
                        head.MtID = newdata.MtID;
                        head.MopYear = newdata.MopYear;
                        head.MopBookCmd = newdata.MopBookCmd;
                        if (newdata.MopDateCmd.HasValue)
                            head.MopDateCmd = UtilityService.ConvertDate2Save(newdata.MopDateCmd);
                        if (newdata.MopDateEff.HasValue)
                            head.MopDateEff = UtilityService.ConvertDate2Save(newdata.MopDateEff);
                        head.MopSignatory = newdata.MopSignatory;
                        //head.MopStatus = newdata.MopStatus;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.SaveChanges();

                        var listdelete = db.tb_Move_Man_Power_Detail.Where(x => x.MopID == newdata.MopID).ToList();
                        db.tb_Move_Man_Power_Detail.RemoveRange(listdelete);
                        db.SaveChanges();

                        if (newdata.ListDetail != null)
                        {
                            foreach (var item in newdata.ListDetail)
                            {
                                tb_Move_Man_Power_Detail detail = new tb_Move_Man_Power_Detail();
                                detail.MopID = head.MopID;
                                detail.UserID = item.UserID;
                                detail.MopOldPoID = item.CurMpID;
                                detail.MopNewPoID = item.MovMpID;
                                detail.PoTAgentID = item.AgentPoTID;
                                detail.AgentMpID = item.AgentMpID;
                                detail.MovRemark = item.MovRemark;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Move_Man_Power_Detail.Add(detail);
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

        public ResponseData DeletePositionTransfer(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    try
                    {
                        var listdelete = db.tb_Move_Man_Power_Detail.Where(x => x.MopID == id).ToList();
                        db.tb_Move_Man_Power_Detail.RemoveRange(listdelete);
                        db.SaveChanges();

                        var itemdelete = db.tb_Move_Man_Power_Head.Where(x => x.MopID == id).FirstOrDefault();
                        db.tb_Move_Man_Power_Head.Remove(itemdelete);
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

        public FileViewModel DownloadFilePositionTransfer(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Move_Man_Power_Head.Where(x => x.MopID == id).FirstOrDefault();
                    string filename = data.MopPathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadPositionTransfer;

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

        public ResponseData ApprovePositionTransfer(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new ResponseData();
                    try
                    {
                        int actionid = UtilityService.User.UserID;
                        db.sp_ManPower_Approval(id, actionid);
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
    }
}