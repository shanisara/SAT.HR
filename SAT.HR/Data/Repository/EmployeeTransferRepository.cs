using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class EmployeeTransferRepository
    {
        public EmployeeTransferResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            EmployeeTransferResult result = new EmployeeTransferResult();
            List<EmployeeTransferViewModel> list = new List<EmployeeTransferViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Move_Level_Head.ToList();

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
                        EmployeeTransferViewModel model = new EmployeeTransferViewModel();
                        model.RowNumber = ++i;
                        model.MlID = item.MlID;
                        model.MlYear = item.MlYear;
                        model.MlBookCmd = item.MlBookCmd;
                        model.MlTotal = 0; // item.MlTotal;
                        model.MlDateCmd = item.MlDateCmd;
                        model.MlDateCmdText = (item.MlDateCmd.HasValue) ? item.MlDateCmd.Value.ToString("dd/MM/yyy") : string.Empty;
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

        public EmployeeTransferViewModel GetByID(int? id)
        {
            EmployeeTransferViewModel model = new EmployeeTransferViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.tb_Move_Level_Head.Where(x => x.MlID == id).Select(s => new EmployeeTransferViewModel
                    {
                        MlID = s.MlID,
                        MlYear = s.MlYear,
                        MlBookCmd = s.MlBookCmd,
                        MlDateCmd = s.MlDateCmd,
                        MlSignatory = s.MlSignatory,
                        MIPathFile = s.MIPathFile,
                        MlStatus = s.MlStatus,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public EmployeeTransferDetailViewModel GetDetailByID(int? id)
        {
            EmployeeTransferDetailViewModel model = new EmployeeTransferDetailViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.tb_Move_Level_Detail.Where(x => x.MlID == id).Select(s => new EmployeeTransferDetailViewModel
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

        public List<EmployeeTransferDetailViewModel> GetDetail(int? id)
        {
            var list = new List<EmployeeTransferDetailViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var detail = db.vw_Move_Level_Detail.Where(x => x.MlID == id).ToList();

                    foreach (var item in detail)
                    {
                        EmployeeTransferDetailViewModel model = new EmployeeTransferDetailViewModel();
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

        public ResponseData AddByEntity(EmployeeTransferViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    //if (fileUpload != null && fileUpload.ContentLength > 0)
                    //{
                    //    var fileName = Path.GetFileName(fileUpload.FileName);
                    //    var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                    //    string directory = SysConfig.PathUploadLevelTransfer;
                    //    bool isExists = System.IO.Directory.Exists(directory);
                    //    if (!isExists)
                    //        System.IO.Directory.CreateDirectory(directory);

                    //    string newFileName = newdata.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                    //    string fileLocation = Path.Combine(directory, newFileName);

                    //    fileUpload.SaveAs(fileLocation);

                    //    newdata.UpPathFile = newFileName;
                    //}

                    tb_Move_Level_Head head = new tb_Move_Level_Head();
                    head.MlYear = data.MlYear;
                    head.MlBookCmd = data.MlBookCmd;
                    if (!string.IsNullOrEmpty(data.MlDateCmdText))
                        head.MlDateCmd = Convert.ToDateTime(data.MlDateCmdText);
                    head.MlSignatory = data.MlSignatory;
                    head.MIPathFile = data.MIPathFile;
                    head.MlStatus = data.MlStatus;
                    head.CreateBy = UtilityService.User.UserID;
                    head.CreateDate = DateTime.Now;
                    head.ModifyBy = UtilityService.User.UserID;
                    head.ModifyDate = DateTime.Now;
                    db.tb_Move_Level_Head.Add(head);
                    db.SaveChanges();
                    result.ID = head.MlID;

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
                    }
                }
                catch (Exception)
                {

                }
                
                return result;
            }
        }

        public ResponseData UpdateByEntity(EmployeeTransferViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var head = db.tb_Move_Level_Head.Single(x => x.MlID == newdata.MlID);
                    head.MlID = newdata.MlID;
                    head.MlYear = newdata.MlYear;
                    head.MlBookCmd = newdata.MlBookCmd;
                    if (!string.IsNullOrEmpty(newdata.MlDateCmdText))
                        head.MlDateCmd = Convert.ToDateTime(newdata.MlDateCmdText);
                    head.MlSignatory = newdata.MlSignatory;
                    head.MIPathFile = newdata.MIPathFile;
                    head.MlStatus = newdata.MlStatus;
                    head.ModifyBy = UtilityService.User.UserID;
                    head.ModifyDate = DateTime.Now;
                    db.SaveChanges();

                    var listdelete = db.tb_Move_Level_Detail.Where(x => x.MlID == newdata.MlID).ToList();
                    db.tb_Move_Level_Detail.RemoveRange(listdelete);
                    db.SaveChanges();

                    foreach (var item in newdata.ListDetail)
                    {
                        tb_Move_Level_Detail detail = new tb_Move_Level_Detail();
                        detail.MlID = item.MlID;
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
                    }
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

    }
}