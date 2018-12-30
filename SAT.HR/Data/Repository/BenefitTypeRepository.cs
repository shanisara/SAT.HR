using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class BenefitTypeRepository
    {
        public BenefitTypeResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Benefit_Type.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.BenTName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "BenTName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.BenTName).ToList() : data.OrderByDescending(x => x.BenTName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new BenefitTypeViewModel()
                {
                    RowNumber = ++i,
                    BenTID = s.BenTID,
                    BenTName = s.BenTName
                }).Skip(start * length).Take(length).ToList();

                BenefitTypeResult result = new BenefitTypeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<BenefitTypeViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Benefit_Type.Select(s => new BenefitTypeViewModel()
                {
                    BenTID = s.BenTID,
                    BenTName = s.BenTName,
                }).OrderBy(x => x.BenTName).ToList();
                return list;
            }
        }

        public BenefitTypeViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Benefit_Type.Where(x => x.BenTID == id).FirstOrDefault();
                BenefitTypeViewModel model = new Models.BenefitTypeViewModel();
                model.BenTID = data.BenTID;
                model.BenTName = data.BenTName;
                return model;
            }
        }

        public ResponseData AddByEntity(BenefitTypeViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Type model = new tb_Benefit_Type();
                    model.BenTID = data.BenTID;
                    model.BenTName = data.BenTName;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Type.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(BenefitTypeViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Benefit_Type.Single(x => x.BenTID == newdata.BenTID);
                    data.BenTName = newdata.BenTName;
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

        public ResponseData RemoveByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_Benefit_Type.SingleOrDefault(c => c.BenTID == id);
                    if (obj != null)
                    {
                        db.tb_Benefit_Type.Remove(obj);
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