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
    public class DelegateRepository
    {
        public DelegateResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Delegate.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.FromMp.Contains(filter) || x.FromUser.Contains(filter) || x.ToMp.Contains(filter) || x.ToUser.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "FromMp":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.FromMp).ToList() : data.OrderByDescending(x => x.FromMp).ToList();
                        break;
                    case "FromUser":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.FromUser).ToList() : data.OrderByDescending(x => x.FromUser).ToList();
                        break;
                    case "ToMp":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ToMp).ToList() : data.OrderByDescending(x => x.ToMp).ToList();
                        break;
                    case "ToUser":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ToUser).ToList() : data.OrderByDescending(x => x.ToUser).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new DelegateViewModel()
                {
                    RowNumber = ++i,
                    DelegateID = s.DelegateID,
                    DelegateTypeName = s.DelegateTypeName,
                    FromMp = s.FromMp,
                    ToMp = s.ToMp,
                    FromUser = s.FromUser,
                    ToUser = s.ToUser,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    IsActive = s.IsActive,
                    DelegateType = s.DelegateType,
                    FormMasterID = s.FormMasterID,
                    FromMpID = s.FromMpID,
                    FromUserID = s.FromUserID,
                    ToMpID = s.ToMpID,
                    ToUserID = s.ToUserID,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifyDate = s.ModifyDate,
                    ModifyBy = s.ModifyBy
                }).Skip(start * length).Take(length).ToList();

                DelegateResult result = new DelegateResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<DelegateViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Delegate.ToList();

                var list = new List<DelegateViewModel>();
                foreach (var item in data)
                {
                    DelegateViewModel model = new Models.DelegateViewModel();
                    model.DelegateID = item.DelegateID;
                    model.DelegateTypeName = item.DelegateTypeName;
                    model.FromMp = item.FromMp;
                    model.ToMp = item.ToMp;
                    model.FromUser = item.FromUser;
                    model.ToUser = item.ToUser;
                    model.StartDate = item.StartDate;
                    model.EndDate = item.EndDate;
                    model.IsActive = item.IsActive;
                    model.DelegateType = item.DelegateType;
                    model.FormMasterID = item.FormMasterID;
                    model.FromMpID = item.FromMpID;
                    model.FromUserID = item.FromUserID;
                    model.ToMpID = item.ToMpID;
                    model.ToUserID = item.ToUserID;
                    model.CreateDate = item.CreateDate;
                    model.CreateBy = item.CreateBy;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyBy = item.ModifyBy;
                    //model.ModifyDateText = Convert.ToDateTime(item.ModifyDate).ToString("dd/MM/yyyy");
                    list.Add(model);
                }

                return list;
            }
        }

        public DelegateViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Delegate.Where(x => x.DelegateID == id).FirstOrDefault();
                DelegateViewModel model = new Models.DelegateViewModel();
                model.DelegateID = data.DelegateID;
                model.StartDate = data.StartDate;
                model.EndDate = data.EndDate;
                model.IsActive = data.IsActive;
                model.DelegateType = data.DelegateType;
                model.FormMasterID = data.FormMasterID;
                model.FromMpID = data.FromMpID;
                model.FromUserID = data.FromUserID;
                model.ToMpID = data.ToMpID;
                model.ToUserID = data.ToUserID;
                model.CreateDate = data.CreateDate;
                model.CreateBy = data.CreateBy;
                model.ModifyDate = data.ModifyDate;
                model.ModifyBy = data.ModifyBy;

                model.FromUserDivName = data.FromUserDivName;
                model.FromUserDepName = data.FromUserDepName;
                model.FromUserSecName = data.FromUserSecName;
                model.FromUserPoName = data.FromUserPoName;

                model.ToUserDivName = data.ToUserDivName;
                model.ToUserDepName = data.ToUserDepName;
                model.ToUserSecName = data.ToUserSecName;
                model.ToUserPoName = data.ToUserPoName;

                model.FromMPDivName = data.FromMPDivName;
                model.FromMPDepName = data.FromMPDepName;
                model.FromMPSecName = data.FromMPSecName;
                model.FromMPFullName = data.FromMPFullName;

                model.ToMPDivName = data.ToMPDivName;
                model.ToMPDepName = data.ToMPDepName;
                model.ToMPSecName = data.ToMPSecName;
                model.ToMPFullName = data.ToMPFullName;

                model.FromUserDepartment = data.FromUserDivName + (!string.IsNullOrEmpty(data.FromUserDepName) ? " / " : string.Empty) + data.FromUserDepName + (!string.IsNullOrEmpty(data.FromUserSecName) ? " / " : string.Empty) + data.FromUserSecName;
                model.ToUserDepartment = data.ToUserDivName + (!string.IsNullOrEmpty(data.ToUserDepName) ? " / " : string.Empty) + data.ToUserDepName + (!string.IsNullOrEmpty(data.ToUserSecName) ? " / " : string.Empty) + data.ToUserSecName;
                model.FromMPDepartment = data.FromMPDivName + (!string.IsNullOrEmpty(data.FromMPDepName) ? " / " : string.Empty) + data.FromMPDepName + (!string.IsNullOrEmpty(data.FromMPSecName) ? " / " : string.Empty) + data.FromMPSecName;
                model.ToMPDepartment = data.ToMPDivName + (!string.IsNullOrEmpty(data.ToMPDepName) ? " / " : string.Empty) + data.ToMPDepName + (!string.IsNullOrEmpty(data.ToMPSecName) ? " / " : string.Empty) + data.ToMPSecName;


                return model;
            }
        }

        public ResponseData AddByEntity(DelegateViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Delegate model = new tb_Delegate();
                    model.StartDate = data.StartDate;
                    model.EndDate = data.EndDate;
                    model.IsActive = data.IsActive;
                    model.DelegateType = data.DelegateType;
                    model.FormMasterID = data.FormMasterID;
                    model.FromMpID = data.FromMpID;
                    model.FromUserID = data.FromUserID;
                    model.ToMpID = data.ToMpID;
                    model.ToUserID = data.ToUserID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Delegate.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(DelegateViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Delegate.Single(x => x.DelegateID == newdata.DelegateID);
                    model.StartDate = newdata.StartDate;
                    model.EndDate = newdata.EndDate;
                    model.IsActive = newdata.IsActive;
                    model.DelegateType = newdata.DelegateType;
                    model.FormMasterID = newdata.FormMasterID;
                    model.FromMpID = newdata.FromMpID;
                    model.FromUserID = newdata.FromUserID;
                    model.ToMpID = newdata.ToMpID;
                    model.ToUserID = newdata.ToUserID;
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

        public ResponseData RemoveByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_Delegate.SingleOrDefault(c => c.DelegateID == id);
                    if (obj != null)
                    {
                        db.tb_Delegate.Remove(obj);
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