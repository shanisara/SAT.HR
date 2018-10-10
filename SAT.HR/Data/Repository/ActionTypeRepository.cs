using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class ActionTypeRepository
    {
        public ActionTypeResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_ActionType.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.ActName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "ActName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ActName).ToList() : data.OrderByDescending(x => x.ActName).ToList();
                        break;
                    
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new ActionTypeViewModel()
                {
                    RowNumber = ++i,
                    ActID = s.ActID,
                    ActName = s.ActName
                }).Skip(start * length).Take(length).ToList();


                ActionTypeResult result = new ActionTypeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public ActionTypeViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_ActionType.Where(x => x.ActID == id).FirstOrDefault();
                ActionTypeViewModel model = new Models.ActionTypeViewModel();
                model.ActID = data.ActID;
                model.ActName = data.ActName;
                model.ActType = data.ActType;
                model.ActPos = data.ActPos;
                return model;
            }
        }

        public List<ActionTypeViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_ActionType.Select(s => new ActionTypeViewModel()
                {
                    ActID = s.ActID,
                    ActName = s.ActName
                }).OrderBy(x => x.ActName).ToList();
                return list;
            }
        }

        public ResponseData AddByEntity(ActionTypeViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_ActionType model = new tb_ActionType();
                    model.ActID = data.ActID;
                    model.ActName = data.ActName;
                    model.ActType = data.ActType;
                    model.ActPos = data.ActPos;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_ActionType.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(ActionTypeViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_ActionType.Single(x => x.ActID == newdata.ActID);
                    data.ActName = newdata.ActName;
                    data.ActType = newdata.ActType;
                    data.ActPos = newdata.ActPos;
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
                    var obj = db.tb_ActionType.SingleOrDefault(c => c.ActID == id);
                    if (obj != null)
                    {
                        db.tb_ActionType.Remove(obj);
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