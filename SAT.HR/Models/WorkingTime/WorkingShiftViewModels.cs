using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class WorkingShiftViewModel
    {
        //public WorkingShiftViewModel GetWorkingShiftByUser(int userid)
        //{
        //    var data = new BenefitRemunerationViewModel();
        //    var list = new List<BenefitRemunerationViewModel>();
        //    try
        //    {
        //        using (SATEntities db = new SATEntities())
        //        {
        //            int index = 1;
        //            var remuneration = db.tb_Benefit_Remuneration.Where(x => x.UserID == userid).OrderByDescending(o => o.BrID).ToList();

        //            foreach (var item in remuneration)
        //            {
        //                BenefitRemunerationViewModel model = new BenefitRemunerationViewModel();
        //                model.RowNumber = index++;
        //                model.BrID = item.BrID;
        //                model.UserID = item.UserID;
        //                model.BrYear = item.BrYear;
        //                model.RecID = item.RecID;
        //                model.RecFullName = item.RecFullName;
        //                model.BrAmout = item.BrAmout;
        //                model.BrRemark = item.BrRemark;
        //                model.CreateDate = item.CreateDate;
        //                model.CreateBy = item.CreateBy;
        //                model.ModifyDate = item.ModifyDate;
        //                model.ModifyBy = item.ModifyBy;
        //                model.RecName = string.Empty;
        //                list.Add(model);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    data.UserID = userid;
        //    data.ListRemuneration = list;
        //    return data;
        //}

        //public WorkingShiftViewModel GetWorkingShiftByID(int userid, int id)
        //{
        //    BenefitRemunerationViewModel data = new BenefitRemunerationViewModel();
        //    data.UserID = userid;

        //    try
        //    {
        //        using (SATEntities db = new SATEntities())
        //        {
        //            var item = db.tb_Benefit_Remuneration.Where(x => x.BrID == id).FirstOrDefault();
        //            BenefitRemunerationViewModel model = new BenefitRemunerationViewModel();
        //            if (item != null)
        //            {
        //                model.BrID = item.BrID;
        //                model.UserID = item.UserID;
        //                model.BrYear = item.BrYear;
        //                model.BrDate = item.BrDate;
        //                model.RecID = item.RecID;
        //                model.RecFullName = item.RecFullName;
        //                model.BrAmout = item.BrAmout;
        //                model.BrRemark = item.BrRemark;
        //                model.CreateDate = item.CreateDate;
        //                model.CreateBy = item.CreateBy;
        //                model.ModifyDate = item.ModifyDate;
        //                model.ModifyBy = item.ModifyBy;
        //            }
        //            else
        //            {
        //                model.BrYear = DateTime.Now.Year;
        //            }
        //            data = model;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return data;
        //}

        //public ResponseData AddByEntity(WorkingShiftViewModel data)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        ResponseData result = new Models.ResponseData();
        //        try
        //        {
        //            tb_Benefit_Remuneration model = new tb_Benefit_Remuneration();
        //            model.BrID = data.BrID;
        //            model.UserID = data.UserID;
        //            model.BrYear = data.BrYear;
        //            if (Convert.ToDateTime(data.BrDate) > DateTime.MinValue)
        //                model.BrDate = Convert.ToDateTime(data.BrDate);
        //            model.RecID = data.RecID;
        //            model.RecFullName = data.RecFullName;
        //            model.BrAmout = data.BrAmout;
        //            model.BrRemark = data.BrRemark;
        //            model.CreateBy = UtilityService.User.UserID;
        //            model.CreateDate = DateTime.Now;
        //            model.ModifyBy = UtilityService.User.UserID;
        //            model.ModifyDate = DateTime.Now;
        //            db.tb_Benefit_Remuneration.Add(model);
        //            db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            result.MessageCode = "";
        //            result.MessageText = ex.Message;
        //        }
        //        return result;
        //    }
        //}

        //public ResponseData UpdateRemunerationByEntity(WorkingShiftViewModel newdata)
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        ResponseData result = new Models.ResponseData();
        //        try
        //        {
        //            var model = db.tb_Benefit_Remuneration.Single(x => x.UserID == newdata.UserID && x.BrID == newdata.BrID);
        //            model.BrID = newdata.BrID;
        //            model.UserID = newdata.UserID;
        //            model.BrYear = newdata.BrYear;
        //            if (Convert.ToDateTime(newdata.BrDate) > DateTime.MinValue)
        //                model.BrDate = Convert.ToDateTime(newdata.BrDate);
        //            model.RecID = newdata.RecID;
        //            model.RecFullName = newdata.RecFullName;
        //            model.BrAmout = newdata.BrAmout;
        //            model.BrRemark = newdata.BrRemark;
        //            model.ModifyBy = UtilityService.User.UserID;
        //            model.ModifyDate = DateTime.Now;
        //            db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            result.MessageCode = "";
        //            result.MessageText = ex.Message;
        //        }
        //        return result;
        //    }
        //}

        //public ResponseData DeleteByID(int id)
        //{
        //    ResponseData result = new ResponseData();
        //    using (SATEntities db = new SATEntities())
        //    {
        //        try
        //        {
        //            var model = db.tb_Benefit_Remuneration.SingleOrDefault(x => x.BrID == id);
        //            if (model != null)
        //            {
        //                db.tb_Benefit_Remuneration.Remove(model);
        //                db.SaveChanges();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.MessageCode = "";
        //            result.MessageText = ex.Message;
        //        }
        //        return result;
        //    }
        //}
    }
}