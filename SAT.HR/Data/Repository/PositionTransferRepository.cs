using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
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
                    var data = db.vw_Move_Man_Power_Head.ToList();

                    int recordsTotal = data.Count();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        data = data.Where(x => x.MopYear.Contains(filter) || x.EmpTName.Contains(filter) || x.MtName.Contains(filter) || x.MopBookCmd.Contains(filter)).ToList();

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
                            model.MopDateCmdText = (item.MopDateCmd.HasValue) ? item.MopDateCmd.Value.ToString("dd/MM/yyy") : string.Empty;
                            model.MopStatusName = (item.MopStatus.HasValue) ? "ยืนยันแล้ว" : "";
                            model.recordsTotal = recordsTotal;
                            model.recordsFiltered = recordsFiltered;
                            list.Add(model);
                        }
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
                    model = db.tb_Move_Man_Power_Head.Where(x => x.MopID == id).Select(s => new PositionTransferViewModel
                    {
                        MopID = s.MopID,
                        UserTID = s.UserTID,
                        MtID = s.MtID,
                        MopYear = s.MopYear,
                        MopBookCmd = s.MopBookCmd,
                        MopDateCmd = s.MopDateCmd,
                        MopDateEff = s.MopDateEff,
                        MopSignatory = s.MopSignatory,
                        MopPathFile = s.MopPathFile,
                        MopStatus = s.MopStatus
                    }).FirstOrDefault();
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
                    model = db.tb_Move_Man_Power_Detail.Where(x => x.MopID == id).Select(s => new PositionTransferDetailViewModel
                    {
                        MopID = s.MopID,
                        UserID = s.UserID,
                        CurPoID = s.CurPoID,
                        MovPoID = s.MovPoID,
                        AgentPoID = s.AgentID,
                        AgentPoTID = s.PoTID,
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
                        model.UserName = item.UserName;
                        model.CurPoCode = item.CurPoCode;
                        model.CurPoName = item.CurPoName;
                        model.MovPoID = item.MovPoID;
                        model.MovPoCode = item.MovPoCode;
                        model.MovPoName = item.MovPoName;
                        model.AgentPoTID = item.AgentPoTID;
                        model.AgentPoTName = item.AgentPoTName;
                        model.AgentPoID = item.AgentPoID;
                        model.AgentPoCode = item.AgentPoCode;
                        model.AgentPoName = item.AgentPoName;
                        model.MovRemark = item.MovRemark;
                        model.CurrentPo = "(" + item.CurPoCode + ") " + item.CurPoName;
                        model.MovePo = "(" + item.MovPoCode + ") " + item.MovPoName;
                        model.AgentPo = "(" + item.AgentPoCode + ") " + item.AgentPoName;
                        model.AgentPo = item.AgentPoTName + " (" + item.AgentPoCode + ") " + item.AgentPoName;
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
                ResponseData result = new Models.ResponseData();
                try
                {
                    //if (fileUpload != null && fileUpload.ContentLength > 0)
                    //{
                    //    var fileName = Path.GetFileName(fileUpload.FileName);
                    //    var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                    //    string directory = SysConfig.PathUploadPositionTransfer;
                    //    bool isExists = System.IO.Directory.Exists(directory);
                    //    if (!isExists)
                    //        System.IO.Directory.CreateDirectory(directory);

                    //    string newFileName = newdata.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                    //    string fileLocation = Path.Combine(directory, newFileName);

                    //    fileUpload.SaveAs(fileLocation);

                    //    newdata.UpPathFile = newFileName;
                    //}

                    tb_Move_Man_Power_Head head = new tb_Move_Man_Power_Head();
                    head.UserTID = data.UserTID;
                    head.MtID = data.MtID;
                    head.MopYear = data.MopYear;
                    head.MopBookCmd = data.MopBookCmd;
                    head.MopDateCmd = data.MopDateCmd;
                    head.MopDateEff = data.MopDateEff;
                    head.MopSignatory = data.MopSignatory;
                    head.MopPathFile = data.MopPathFile;
                    head.MopStatus = data.MopStatus;
                    head.CreateBy = UtilityService.User.UserID;
                    head.CreateDate = DateTime.Now;
                    head.ModifyBy = UtilityService.User.UserID;
                    head.ModifyDate = DateTime.Now;
                    db.tb_Move_Man_Power_Head.Add(head);
                    db.SaveChanges();
                    result.ID = head.MopID;

                    foreach (var item in data.ListDetail)
                    {
                        tb_Move_Man_Power_Detail detail = new tb_Move_Man_Power_Detail();
                        detail.MopID = head.MopID;
                        detail.UserID = item.UserID;
                        detail.CurPoID = item.CurPoID;
                        detail.MovPoID = item.MovPoID;
                        detail.PoTID = item.AgentPoTID;
                        detail.AgentID = item.AgentPoID;
                        detail.MovRemark = item.MovRemark;
                        detail.CreateBy = UtilityService.User.UserID;
                        detail.CreateDate = DateTime.Now;
                        detail.ModifyBy = UtilityService.User.UserID;
                        detail.ModifyDate = DateTime.Now;
                        db.tb_Move_Man_Power_Detail.Add(detail);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(PositionTransferViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var head = db.tb_Move_Man_Power_Head.Single(x => x.MopID == newdata.MopID);
                    head.UserTID = newdata.UserTID;
                    head.MtID = newdata.MtID;
                    head.MopYear = newdata.MopYear;
                    head.MopBookCmd = newdata.MopBookCmd;
                    head.MopDateCmd = newdata.MopDateCmd;
                    head.MopDateEff = newdata.MopDateEff;
                    head.MopSignatory = newdata.MopSignatory;
                    head.MopPathFile = newdata.MopPathFile;
                    head.MopStatus = newdata.MopStatus;
                    head.ModifyBy = UtilityService.User.UserID;
                    head.ModifyDate = DateTime.Now;
                    db.SaveChanges();

                    var listdelete = db.tb_Move_Man_Power_Detail.Where(x => x.MopID == newdata.MopID).ToList();
                    db.tb_Move_Man_Power_Detail.RemoveRange(listdelete);
                    db.SaveChanges();

                    foreach (var item in newdata.ListDetail)
                    {
                        tb_Move_Man_Power_Detail detail = new tb_Move_Man_Power_Detail();
                        detail.UserID = item.UserID;
                        detail.CurPoID = item.CurPoID;
                        detail.MovPoID = item.MovPoID;
                        detail.PoTID = item.AgentPoTID;
                        detail.AgentID = item.AgentPoID;
                        detail.MovRemark = item.MovRemark;
                        detail.ModifyBy = UtilityService.User.UserID;
                        detail.ModifyDate = DateTime.Now;
                        db.tb_Move_Man_Power_Detail.Add(detail);
                        db.SaveChanges();
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