using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;
using System.Web.UI.WebControls;
using System.Data;

namespace SAT.HR.Data.Repository
{
    public class PositionRepository
    {
        public PositionResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, int? type)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Position.Where(m => m.TypeID == type).ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.PoCode.Contains(filter) || x.PoName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "PoCode":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.PoCode).ToList() : data.OrderByDescending(x => x.PoCode).ToList();
                        break;
                    case "PoName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.PoName).ToList() : data.OrderByDescending(x => x.PoName).ToList();
                        break;
                    case "Status":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.PoStatus).ToList() : data.OrderByDescending(x => x.PoStatus).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new PositionViewModel()
                {
                    RowNumber = i + 1,
                    PoID = s.PoID,
                    PoCode = s.PoCode,
                    PoName = s.PoName,
                    PoStatus = s.PoStatus,
                    TypeID = s.TypeID,
                    ProjectNo = s.ProjectNo,
                    ProjectName = s.ProjectName,
                    Status = s.PoStatus == true ? EnumType.StatusName.Active : EnumType.StatusName.NotActive
                }).Skip(start * length).Take(length).ToList();

                PositionResult result = new PositionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<PositionViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Position.Select(s => new PositionViewModel()
                {
                    PoID = s.PoID,
                    PoCode = s.PoCode,
                    PoName = s.PoName,
                    PoStatus = s.PoStatus,
                    TypeID = s.TypeID,
                    ProjectNo = s.ProjectNo,
                    ProjectName = s.ProjectName
                }).OrderBy(x => x.PoCode).ToList();
                return list;
            }
        }

        public List<PositionViewModel> GetByType(int? typeid)
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Position.Where(w => w.TypeID == typeid).Select(s => new PositionViewModel()
                {
                    PoID = s.PoID,
                    PoCode = s.PoCode,
                    PoName = s.PoName,
                    PoStatus = s.PoStatus,
                    TypeID = s.TypeID,
                    ProjectNo = s.ProjectNo,
                    ProjectName = s.ProjectName
                }).OrderBy(x => x.PoCode).ToList();
                return list;
            }
        }

        public PositionViewModel GetByID(int? id, int? type)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Position.Where(x => x.PoID == id).FirstOrDefault();
                PositionViewModel model = new Models.PositionViewModel();
                model.PoID = data.PoID;
                model.PoCode = data.PoCode;
                model.PoName = data.PoName;
                model.PoStatus = data.PoStatus;
                model.Status = data.PoStatus == true ? EnumType.StatusName.Active : EnumType.StatusName.NotActive;
                model.TypeID = data.TypeID;
                model.ProjectNo = data.ProjectNo;
                model.ProjectName = data.ProjectName;
                return model;
            }
        }

        public ResponseData AddByEntity(PositionViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Position model = new tb_Position();
                    model.PoID = data.PoID;
                    model.PoCode = data.PoCode;
                    model.PoName = data.PoName;
                    model.PoStatus = (data.Status == "1") ? true : false;
                    model.TypeID = data.TypeID;
                    model.ProjectNo = data.ProjectNo;
                    model.ProjectName = data.ProjectName;
                    //model.ModifyBy = UtilityService.User.UserID;
                    //model.ModifyDate = DateTime.Now;
                    db.tb_Position.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(PositionViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Position.Single(x => x.PoID == newdata.PoID);
                    model.PoCode = newdata.PoCode;
                    model.PoName = newdata.PoName;
                    model.PoStatus = (newdata.Status == "1") ? true : false;
                    model.TypeID = newdata.TypeID;
                    model.ProjectNo = newdata.ProjectNo;
                    model.ProjectName = newdata.ProjectName;
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
                    var obj = db.tb_Position.SingleOrDefault(c => c.PoID == id);
                    if (obj != null)
                    {
                        db.tb_Position.Remove(obj);
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

        public List<PositionTypeViewModel> GetPositionType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Position_Type.Select(s => new PositionTypeViewModel()
                {
                    PoTID = s.PoTID,
                    PoTName = s.PoTName
                }).ToList();
                return list;
            }
        }

        public List<PositionAgentViewModel> GetPositionAgent()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Position_Agent.Select(s => new PositionAgentViewModel()
                {
                    PoAID = s.PoAID,
                    PoAName = s.PoAName
                }).ToList();
                return list;
            }
        }
    }
}