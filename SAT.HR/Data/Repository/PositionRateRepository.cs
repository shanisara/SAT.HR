using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class PositionRateRepository
    {
        public PositionRatePageResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, string userType)
        {
            PositionRatePageResult result = new PositionRatePageResult();
            List<PositionRateViewModel> list = new List<PositionRateViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    sortBy = (sortBy == "RowNumber") ? "MpID" : sortBy;
                    string perPage = initialPage.HasValue ? Convert.ToInt32(initialPage) == 0 ? "1" : (Convert.ToInt32(initialPage.ToString().Substring(0, initialPage.ToString().Length - 1)) + 1).ToString() : "1";
                    var data = db.sp_Man_Power_List(pageSize.ToString(), perPage, sortBy, sortDir, userType, filter).ToList();

                    int i = 0;
                    foreach (var item in data)
                    {
                        PositionRateViewModel model = new PositionRateViewModel();
                        model.RowNumber = ++i;
                        model.MpID = (int)item.MpID;
                        model.MpCode = item.MpCode;
                        model.UserID = item.UserID;
                        model.FullNameTh = !string.IsNullOrEmpty(item.FullNameTh) ? "("+ model.MpCode + ") " + item.FullNameTh : "ตำแหน่งว่าง ✓";
                        model.FullDepartment = item.DivName + (!string.IsNullOrEmpty(item.DepName) ? " / " : "") + item.DepName + (!string.IsNullOrEmpty(item.SecName) ? " / " : "") + item.SecName;
                        //model.DivID = item.DivID;
                        //model.DivName = item.DivName;
                        //model.DepID = item.DepID;
                        //model.DepName = item.DepName;
                        //model.SecID = item.SecID;
                        //model.SecName = item.SecName;
                        //model.PoID = item.PoID;
                        model.PoName = item.PoName;
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

        public PositionRateViewModel GetByID(int? id, int? type)
        {
            PositionRateViewModel model = new PositionRateViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_Man_Power.Where(x => x.MpID == id).FirstOrDefault();
                    if (data != null)
                    {
                        model.MpID = data.MpID;
                        model.MpCode = data.TypeID == 1 ? data.MpID.ToString().PadLeft(3, '0') : data.MpID.ToString().PadLeft(4, '0');
                        //model.DivID = data.DivID;
                        //model.DepID = data.DepID;
                        //model.SecID = data.SecID;
                        model.PoID = data.PoID;
                        model.DisID = data.DisID;
                        model.UserID = data.UserID;
                        model.FullNameTh = data.TiShortName + data.FullNameTh;
                        model.EduID = data.EduID;
                        model.TypeID = data.TypeID;
                    }
                    else
                    {
                        model.TypeID = type;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }

        public ResponseData AddByEntity(PositionRateViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    int maxID = db.tb_Man_Power.Where(m => m.TypeID == data.TypeID).Max(m => (int)m.MpID);

                    tb_Man_Power model = new tb_Man_Power();
                    model.MpID = maxID + 1;
                    model.TypeID = data.TypeID;
                    //model.DivID = data.DivID;
                    model.DepID = data.DepID;
                    //model.SecID = data.SecID;
                    model.PoID = data.PoID;
                    model.DisID = data.DisID;
                    model.UserID = data.UserID;
                    model.EduID = data.EduID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Man_Power.Add(model);
                    db.SaveChanges();
                    result.ID = model.MpID;
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(PositionRateViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Man_Power.Single(x => x.MpID == newdata.MpID);
                    //model.DivID = newdata.DivID;
                    model.DepID = newdata.DepID;
                    //model.SecID = newdata.SecID;
                    model.PoID = newdata.PoID;
                    model.DisID = newdata.DisID;
                    model.UserID = newdata.UserID;
                    model.EduID = newdata.EduID;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
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

    }
}



