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
    public class LevelTransferRepository
    {
        public LevelTransferResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            LevelTransferResult result = new LevelTransferResult();
            List<LevelTransferViewModel> list = new List<LevelTransferViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_Move_Level_Head.ToList();

                    int recordsTotal = data.Count();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        data = data.Where(x => x.MlBookCmd.Contains(filter)).ToList();
                    }

                    int recordsFiltered = data.Count();

                    switch (sortBy)
                    {
                        case "MlYear":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MlYear).ToList() : data.OrderByDescending(x => x.MlYear).ToList(); break;
                        case "MlBookCmd":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MlBookCmd).ToList() : data.OrderByDescending(x => x.MlBookCmd).ToList(); break;
                        case "MlDateCmdText":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.MlDateCmd).ToList() : data.OrderByDescending(x => x.MlDateCmd).ToList(); break;
                    }

                    int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                    int length = pageSize ?? 10;

                    int i = 1;
                    foreach (var item in data)
                    {
                        LevelTransferViewModel model = new LevelTransferViewModel();
                        model.RowNumber = ++i;
                        model.MlID = item.MlID;
                        model.MlYear = item.MlYear;
                        model.MlBookCmd = item.MlBookCmd;
                        model.MlTotal = item.MlTotal;
                        model.MlDateCmd = item.MlDateCmd;
                        model.MlDateCmdText = (item.MlDateCmd.HasValue) ? item.MlDateCmd.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.CreateDateText = item.CreateDate.Value.ToString("dd/MM/yyy");
                        model.MlStatusName = (item.MlStatus.HasValue) ? "ยืนยันแล้ว" : "";
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

        public LevelTransferViewModel GetByID(int? id)
        {
            LevelTransferViewModel model = new LevelTransferViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    if (id != null)
                    {
                        var data = db.tb_Move_Level_Head.Where(x => x.MlID == id).FirstOrDefault();
                        LevelTransferViewModel head = new LevelTransferViewModel();
                        model.MlID = data.MlID;
                        model.MlYear = data.MlYear;
                        model.MlBookCmd = data.MlBookCmd;
                        model.MlDateCmd = data.MlDateCmd;
                        model.MlDateCmdText = data.MlDateCmd.HasValue ? data.MlDateCmd.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.MlSignatory = data.MlSignatory;
                        model.MIPathFile = data.MIPathFile;
                        model.MlApproveStatus = data.MlApproveStatus == null ? false : data.MlApproveStatus;

                        var detail = GetDetail(id);
                        model.ListDetail = detail;
                    }
                    else
                    {
                        model.MlYear = Convert.ToInt32(DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("th-TH")));
                        model.MlApproveStatus = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public LevelTransferDetailViewModel GetDetailByID(int? id)
        {
            LevelTransferDetailViewModel model = new LevelTransferDetailViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.tb_Move_Level_Detail.Where(x => x.MlID == id).Select(s => new LevelTransferDetailViewModel
                    {
                        MlID = s.MlID,
                        UserID = s.UserID,
                        MlLevelOld = s.MlLevelOld,
                        MlStepOld = s.MlStepOld,
                        MlLevelNew = s.MlLevelNew,
                        MlStepNew = s.MlStepNew
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public List<LevelTransferDetailViewModel> GetDetail(int? id)
        {
            var list = new List<LevelTransferDetailViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var detail = db.vw_Move_Level_Detail.Where(x => x.MlID == id).ToList();

                    foreach (var item in detail)
                    {
                        LevelTransferDetailViewModel model = new LevelTransferDetailViewModel();
                        model.RowNumber = index++;
                        model.MlID = item.MlID;
                        model.UserID = item.UserID;
                        model.FullName = item.FullName;
                        model.MlLevelOld = item.MlLevelOld;
                        model.MlStepOld = item.MlStepOld;
                        model.MlSalaryOld = item.MlSalaryOld;
                        model.MlSalaryNew = item.MlSalaryNew;
                        model.MlLevelNew = item.MlLevelNew;
                        model.MlStepNew = item.MlStepNew;
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

        public ResponseData AddByEntity(LevelTransferViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        tb_Move_Level_Head head = new tb_Move_Level_Head();

                        if (data.fileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = data.fileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadLevelTransfer;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = " คส.ที่ " + data.MlBookCmd + " เรื่องการแต่งตั้งเลื่อนระดับพนักงาน" + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.MIPathFile = newFileName;
                            }
                        }
                        
                        head.MlYear = data.MlYear;
                        head.MlBookCmd = data.MlBookCmd;
                        if (data.MlDateCmd.HasValue)
                            head.MlDateCmd = UtilityService.ConvertDate2Save(data.MlDateCmd);
                        head.MlSignatory = data.MlSignatory;
                        head.MlApproveStatus = data.MlApproveStatus;
                        head.CreateBy = UtilityService.User.UserID;
                        head.CreateDate = DateTime.Now;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.tb_Move_Level_Head.Add(head);
                        db.SaveChanges();
                        result.ID = head.MlID;

                        if (data.ListDetail != null)
                        {
                            foreach (var item in data.ListDetail)
                            {
                                tb_Move_Level_Detail detail = new tb_Move_Level_Detail();
                                detail.MlID = head.MlID;
                                detail.UserID = item.UserID;
                                detail.MlLevelOld = item.MlLevelOld;
                                detail.MlStepOld = item.MlStepOld;
                                detail.MlLevelNew = item.MlLevelNew;
                                detail.MlStepNew = item.MlStepNew;
                                detail.CreateBy = UtilityService.User.UserID;
                                detail.CreateDate = DateTime.Now;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Move_Level_Detail.Add(detail);
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

        public ResponseData UpdateByEntity(LevelTransferViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        var head = db.tb_Move_Level_Head.Single(x => x.MlID == newdata.MlID);

                        if (newdata.fileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = newdata.fileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadLevelTransfer;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = "คส.ที่ " + newdata.MlBookCmd + " เรื่องการแต่งตั้งเลื่อนระดับพนักงาน" + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.MIPathFile = newFileName;
                            }
                        }
                        
                        head.MlID = newdata.MlID;
                        head.MlYear = newdata.MlYear;
                        head.MlBookCmd = newdata.MlBookCmd;
                        if (newdata.MlDateCmd.HasValue)
                            head.MlDateCmd = UtilityService.ConvertDate2Save(newdata.MlDateCmd);
                        head.MlSignatory = newdata.MlSignatory;
                        //head.MlApproveStatus = newdata.MlApproveStatus;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.SaveChanges();

                        var listdelete = db.tb_Move_Level_Detail.Where(x => x.MlID == newdata.MlID).ToList();
                        db.tb_Move_Level_Detail.RemoveRange(listdelete);
                        db.SaveChanges();

                        if (newdata.ListDetail != null)
                        {
                            foreach (var item in newdata.ListDetail)
                            {
                                tb_Move_Level_Detail detail = new tb_Move_Level_Detail();
                                detail.MlID = head.MlID;
                                detail.UserID = item.UserID;
                                detail.MlLevelOld = item.MlLevelOld;
                                detail.MlStepOld = item.MlStepOld;
                                detail.MlLevelNew = item.MlLevelNew;
                                detail.MlStepNew = item.MlStepNew;
                                detail.CreateBy = UtilityService.User.UserID;
                                detail.CreateDate = DateTime.Now;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Move_Level_Detail.Add(detail);
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

        public ResponseData ApproveLevelTransfer(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new ResponseData();
                    try
                    {
                        int actionid = UtilityService.User.UserID;
                        db.sp_Move_Level_Approval(id, actionid);
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

        public ResponseData DeleteLevelTransfer(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    try
                    {
                        var listdelete = db.tb_Move_Level_Detail.Where(x => x.MlID == id).ToList();
                        db.tb_Move_Level_Detail.RemoveRange(listdelete);
                        db.SaveChanges();

                        var itemdelete = db.tb_Move_Level_Head.Where(x => x.MlID == id).FirstOrDefault();
                        db.tb_Move_Level_Head.Remove(itemdelete);
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

        public FileViewModel DownloadFileLevelTransfer(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = GetByID(id);
                    string filename = data.MIPathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathUploadLevelTransfer;

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

    }
}