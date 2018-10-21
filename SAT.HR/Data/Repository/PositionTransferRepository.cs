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
                        model.UserPoCode = item.UserPoCode;
                        model.UserPoName = item.UserPoName;
                        model.MovPoID = item.MovPoID;
                        model.MovPoCode = item.MovPoCode;
                        model.MovPoName = item.MovPoName;
                        model.AgentPoTID = item.AgentPoTID;
                        model.AgentPoTName = item.AgentPoTName;
                        model.AgentPoID = item.AgentPoID;
                        model.AgentPoCode = item.AgentPoCode;
                        model.AgentPoName = item.AgentPoName;
                        model.MovRemark = item.MovRemark;
                        model.PoNameNew = "(" + item.MovPoCode + ") " + item.MovPoName;
                        model.PoNameOld = "(" + item.UserPoCode + ") " + item.UserPoName;
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
                    tb_Move_Man_Power_Head model = new tb_Move_Man_Power_Head();
                    
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Move_Man_Power_Head.Add(model);
                    db.SaveChanges();
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
                    var model = db.tb_Move_Man_Power_Head.Single(x => x.MopID == newdata.MopID);
                    
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

    }
}