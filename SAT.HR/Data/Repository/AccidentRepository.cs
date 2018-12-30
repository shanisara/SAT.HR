using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class AccidentRepository
    {
        public AccidentResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Accident.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.ActPlace.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "ActDate":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ActDate).ToList() : data.OrderByDescending(x => x.ActDate).ToList(); break;
                    case "ActPlace":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.ActPlace).ToList() : data.OrderByDescending(x => x.ActPlace).ToList(); break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new AccidentViewModel()
                {
                    RowNumber = i + 1,
                    ActID = s.ActID,
                    UserID = s.UserID,
                    ActDate = s.ActDate,
                    ActPlace = s.ActPlace,
                    ActDesc = s.ActDesc,
                }).Skip(start * length).Take(length).ToList();

                AccidentResult result = new AccidentResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public AccidentViewModel AccidentDetail(int? userid)
        {
            try
            {
                AccidentViewModel model = new AccidentViewModel();

                using (SATEntities db = new SATEntities())
                {
                    var user = db.vw_Employee.Where(x => x.UserID == userid).FirstOrDefault();
                    model.UserID = user.UserID;
                    model.IDCard = user.IDCard;
                    model.FullNameTh = user.TiShortName + user.FullNameTh;

                    return model;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AccidentViewModel AccidentByUser(int? userid)
        {
            try
            {
                AccidentViewModel model = new AccidentViewModel();

                using (SATEntities db = new SATEntities())
                {
                    List<AccidentViewModel> lists = new List<AccidentViewModel>();
                    var items = db.tb_Accident.Where(x => x.UserID == userid).ToList();
                    if (items.Count > 0)
                    {
                        int index = 0;
                        foreach (var item in items)
                        {
                            AccidentViewModel obj = new AccidentViewModel();
                            obj.RowNumber = ++index;
                            obj.ActID = item.ActID;
                            obj.UserID = item.UserID;
                            obj.ActPlace = item.ActPlace;
                            obj.ActDesc = item.ActDesc;
                            obj.ActDate = item.ActDate;
                            obj.ActDateText = Convert.ToDateTime(item.ActDate).ToString("dd/MM/yyyy");
                            obj.Amount = item.Amount;
                            lists.Add(obj);
                        }
                    }
                    model.listAccident = lists;

                    return model;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AccidentViewModel AccidentDetailByID(int? id, int userid)
        {
            try
            {
                AccidentViewModel model = new AccidentViewModel();

                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_Accident.Where(x => x.ActID == id).FirstOrDefault();
                    if (obj != null)
                    {
                        model.ActID = obj.ActID;
                        model.UserID = obj.UserID;
                        model.ActDate = obj.ActDate;
                        model.ActPlace = obj.ActPlace;
                        model.ActDesc = obj.ActDesc;
                        model.CreateDate = obj.CreateDate;
                        model.CreateBy = obj.CreateBy;
                        model.ModifyDate = obj.ModifyDate;
                        model.ModifyBy = obj.ModifyBy;
                        model.Amount = obj.Amount;
                    }
                    else
                    {
                        model.UserID = userid;
                    }
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseData AddByEntity(AccidentViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Accident model = new tb_Accident();
                    model.UserID = data.UserID;
                    model.ActDate = data.ActDate;
                    model.ActPlace = data.ActPlace;
                    model.ActDesc = data.ActDesc;
                    model.Amount = data.Amount;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Accident.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(AccidentViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Accident.Single(x => x.ActID == newdata.ActID);
                    data.UserID = newdata.UserID;
                    data.ActDate = newdata.ActDate;
                    data.ActPlace = newdata.ActPlace;
                    data.ActDesc = newdata.ActDesc;
                    data.Amount = newdata.Amount;
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
                    var obj = db.tb_Accident.SingleOrDefault(c => c.ActID == id);
                    if (obj != null)
                    {
                        db.tb_Accident.Remove(obj);
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