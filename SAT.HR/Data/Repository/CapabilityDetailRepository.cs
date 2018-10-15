using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class CapabilityDetailRepository
    {
        public List<CapabilityDetailViewModel> GetByCap(int capis)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_CapabilityDetail.Where(x => x.CapID == capis).Select(s => new CapabilityDetailViewModel
                {
                    CapDID = s.CapDID,
                    CapDName = s.CapDName,
                    CapDDesc = s.CapDDesc,
                    CapID = s.CapID,
                    Score1 = s.Score1,
                    Score2 = s.Score2,
                }).ToList();
                return data;
            }
        }

        public ResponseData AddByEntity(CapabilityDetailViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_CapabilityDetail model = new tb_CapabilityDetail();
                    model.CapDID = data.CapDID;
                    model.CapDName = data.CapDName;
                    model.CapDDesc = data.CapDDesc;
                    model.CapID = data.CapID;
                    model.Score1 = data.Score1;
                    model.Score2 = data.Score2;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_CapabilityDetail.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(CapabilityDetailViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_CapabilityDetail.Single(x => x.CapID == newdata.CapID);
                    data.CapDID = newdata.CapDID;
                    data.CapDName = newdata.CapDName;
                    data.CapDDesc = newdata.CapDDesc;
                    data.CapID = newdata.CapID;
                    data.Score1 = newdata.Score1;
                    data.Score2 = newdata.Score2;
                    data.ModifyBy = UtilityService.User.UserID;
                    data.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData SubmitByEntity(List<CapabilityDetailViewModel> newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    foreach (var item in newdata)
                    {
                        if (item.CapDID != 0)
                        {
                            var data = db.tb_CapabilityDetail.Single(x => x.CapDID == item.CapDID);
                            data.CapDID = item.CapDID;
                            data.CapDName = item.CapDName;
                            data.CapDDesc = item.CapDDesc;
                            data.CapID = item.CapID;
                            data.Score1 = item.Score1;
                            data.Score2 = item.Score2;
                            data.ModifyBy = UtilityService.User.UserID;
                            data.ModifyDate = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            tb_CapabilityDetail model = new tb_CapabilityDetail();
                            model.CapDID = item.CapDID;
                            model.CapDName = item.CapDName;
                            model.CapDDesc = item.CapDDesc;
                            model.CapID = item.CapID;
                            model.Score1 = item.Score1;
                            model.Score2 = item.Score2;
                            model.CreateBy = UtilityService.User.UserID;
                            model.CreateDate = DateTime.Now;
                            model.ModifyBy = UtilityService.User.UserID;
                            model.ModifyDate = DateTime.Now;
                            db.tb_CapabilityDetail.Add(model);
                            db.SaveChanges();
                        }
                    }
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
                    var obj = db.tb_CapabilityDetail.SingleOrDefault(c => c.CapDID == id);
                    if (obj != null)
                    {
                        db.tb_CapabilityDetail.Remove(obj);
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