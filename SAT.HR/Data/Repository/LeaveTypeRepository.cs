using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class LeaveTypeRepository
    {
        public LeaveTypeResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Leave_Type.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.LevName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "LevYear":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevYear).ToList() : data.OrderByDescending(x => x.LevYear).ToList(); break;
                    case "LevName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevName).ToList() : data.OrderByDescending(x => x.LevName).ToList(); break;
                    case "LevStartDateText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevStartDate).ToList() : data.OrderByDescending(x => x.LevStartDate).ToList(); break;
                    case "LevEndDateText":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevEndDate).ToList() : data.OrderByDescending(x => x.LevEndDate).ToList(); break;
                    case "LevMax":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevMax).ToList() : data.OrderByDescending(x => x.LevMax).ToList(); break;
                    case "Status":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.LevStatus).ToList() : data.OrderByDescending(x => x.LevStatus).ToList(); break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new LeaveTypeViewModel()
                {
                    RowNumber = ++i,
                    LevID = s.LevID,
                    LevYear = s.LevYear,
                    LevName = s.LevName,
                    LevStartDateText = s.LevStartDate.Value.ToString("dd/MM/yyyy"),
                    LevEndDateText = s.LevEndDate.Value.ToString("dd/MM/yyyy"),
                    LevMax = s.LevMax,
                    LevStatus = s.LevStatus,
                    Status = s.LevStatus == true ? EnumType.StatusName.Active : EnumType.StatusName.NotActive,
                    SexID = s.SexID
                }).Skip(start * length).Take(length).ToList();

                LeaveTypeResult result = new LeaveTypeResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<LeaveTypeViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                List<LeaveTypeViewModel> list = new List<LeaveTypeViewModel>();

                DateTime curDate = DateTime.Now;
                var data = db.tb_Leave_Type.Where(m => (m.LevStartDate.Value.Year <= curDate.Year && m.LevStartDate.Value.Month <= curDate.Month && m.LevStartDate.Value.Day <= curDate.Day)
                           && (m.LevEndDate.Value.Year >= curDate.Year && m.LevEndDate.Value.Month >= curDate.Month && m.LevEndDate.Value.Day >= curDate.Day)).ToList();

                foreach (var s in data)
                {
                    LeaveTypeViewModel model = new LeaveTypeViewModel();
                    model.LevID = s.LevID;
                    model.LevYear = s.LevYear;
                    model.LevName = s.LevName;
                    model.LevStartDate = s.LevStartDate;
                    model.LevEndDate = s.LevEndDate;
                    model.LevStartDateText = s.LevStartDate.Value.ToString("dd/MM/yyyy");
                    model.LevEndDateText = s.LevEndDate.Value.ToString("dd/MM/yyyy");
                    model.LevMax = s.LevMax;
                    model.LevStatus = s.LevStatus;
                    model.SexID = s.SexID;
                    list.Add(model);
                }
                return list;
            }
        }

        public List<LeaveTypeViewModel> GetLeaveType(int? sexid, int requestid)
        {
            using (SATEntities db = new SATEntities())
            {
                var user = db.tb_User.Where(m => m.UserID == requestid).FirstOrDefault();
                int type = (int)user.UserTID;

                DateTime curDate = DateTime.Now;
                var data = db.tb_Leave_Type.Where(m => (m.TypeID == type
                           && m.LevStartDate.Value.Year <= curDate.Year && m.LevStartDate.Value.Month <= curDate.Month && m.LevStartDate.Value.Day <= curDate.Day)
                           && (m.LevEndDate.Value.Year >= curDate.Year && m.LevEndDate.Value.Month >= curDate.Month && m.LevEndDate.Value.Day >= curDate.Day)).ToList();

                if (!sexid.HasValue)
                    sexid = UtilityService.User.SexID;

                data = data.Where(w => w.SexID == null || w.SexID == sexid).ToList();

                var list = data.Select(s => new LeaveTypeViewModel()
                {
                    LevID = s.LevID,
                    LevName = s.LevName
                }).ToList();
                return list;
            }
        }

        public LeaveTypeViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Leave_Type.Where(x => x.LevID == id).FirstOrDefault();
                LeaveTypeViewModel model = new Models.LeaveTypeViewModel();
                model.LevID = data.LevID;
                model.LevYear = data.LevYear;
                model.LevName = data.LevName;
                model.LevStartDate = data.LevStartDate;
                model.LevEndDate = data.LevEndDate;
                model.LevMax = data.LevMax;
                model.SexID = data.SexID;
                model.LevStartDateText = data.LevStartDate.Value.ToString("dd/MM/yyyy");
                model.LevEndDateText = data.LevEndDate.Value.ToString("dd/MM/yyyy");
                model.LevStatus = data.LevStatus;
                return model;
            }
        }

        public ResponseData AddByEntity(LeaveTypeViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Leave_Type model = new tb_Leave_Type();
                    model.LevID = data.LevID;
                    model.LevYear = data.LevYear;
                    model.LevName = data.LevName;
                    model.LevStartDate = Convert.ToDateTime(data.LevStartDateText);
                    model.LevEndDate = Convert.ToDateTime(data.LevEndDateText);
                    model.LevMax = data.LevMax;
                    model.SexID = data.SexID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Leave_Type.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(LeaveTypeViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Leave_Type.Single(x => x.LevID == newdata.LevID);
                    data.LevYear = newdata.LevYear;
                    data.LevName = newdata.LevName;
                    data.LevStartDate = Convert.ToDateTime(newdata.LevStartDateText);
                    data.LevEndDate = Convert.ToDateTime(newdata.LevEndDateText);
                    data.LevMax = newdata.LevMax;
                    data.SexID = newdata.SexID;
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
                    var obj = db.tb_Leave_Type.SingleOrDefault(c => c.LevID == id);
                    if (obj != null)
                    {
                        db.tb_Leave_Type.Remove(obj);
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