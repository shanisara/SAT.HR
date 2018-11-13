using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class CapabilityRepository
    {
        public CapabilityResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Capability.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.CapTName.Contains(filter) || x.CapGName.Contains(filter) || x.CapGTName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "CapYear":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CapYear).ToList() : data.OrderByDescending(x => x.CapYear).ToList();
                        break;
                    case "CapTName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CapTName).ToList() : data.OrderByDescending(x => x.CapTName).ToList();
                        break;
                    case "CapGName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CapGName).ToList() : data.OrderByDescending(x => x.CapGName).ToList();
                        break;
                    case "CapGTName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.CapGTName).ToList() : data.OrderByDescending(x => x.CapGTName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new CapabilityViewModel()
                {
                    RowNumber = ++i,
                    CapID = s.CapID,
                    CapYear = s.CapYear,
                    CapTID = s.CapTID,
                    CapTName = s.CapTName,
                    CapGID = s.CapGID,
                    CapGName =s.CapGName,
                    CapGTID = s.CapGTID,
                    CapGTName = s.CapGTName
                }).Skip(start * length).Take(length).ToList();

                CapabilityResult result = new CapabilityResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public CapabilityViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Capability.Where(x => x.CapID == id).FirstOrDefault();
                CapabilityViewModel model = new Models.CapabilityViewModel();
                model.CapID = data.CapID;
                model.CapYear = data.CapYear;
                model.CapTID = data.CapTID;
                model.CapGID = data.CapGID;
                model.CapGTID = data.CapGTID;
                model.CapTName = data.CapTName;
                model.CapGName = data.CapGName;
                model.CapGTName = data.CapGTName;
                return model;
            }
        }

        public List<CapabilityViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Capability.Select(s => new CapabilityViewModel()
                {
                    CapID = s.CapID,
                    CapYear = s.CapYear,
                    CapTID = s.CapTID,
                    CapGID = s.CapGID,
                }).OrderBy(x => x.CapYear).ToList();
                return list;
            }
        }

        public List<CapabilityTypeViewModel> GetCapabilityType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Capability_Type.Select(s => new CapabilityTypeViewModel()
                {
                    CapTID = s.CapTID,
                    CapTName = s.CapTName,
                }).OrderBy(x => x.CapTName).ToList();
                return list;
            }
        }

        public List<CapabilityGroupViewModel> GetCapabilityGroup()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Capability_Group.Select(s => new CapabilityGroupViewModel()
                {
                    CapGID = s.CapGID,
                    CapGName = s.CapGName,
                    TableName = s.TableName
                }).OrderBy(x => x.CapGName).ToList();
                return list;
            }
        }

        public ResponseData AddByEntity(CapabilityViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Capability model = new tb_Capability();
                    model.CapID = data.CapID;
                    model.CapYear = data.CapYear;
                    model.CapTID = data.CapTID;
                    model.CapGID = data.CapGID;
                    model.CapGTID = data.CapGTID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Capability.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(CapabilityViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Capability.Single(x => x.CapID == newdata.CapID);
                    data.CapYear = newdata.CapYear;
                    data.CapTID = newdata.CapTID;
                    data.CapGID = newdata.CapGID;
                    data.CapGTID = newdata.CapGTID;
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
                    var obj = db.tb_Capability.SingleOrDefault(c => c.CapID == id);
                    if (obj != null)
                    {
                        db.tb_Capability.Remove(obj);
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

        public List<YearCapabilityViewModel> GetYearCapability()
        {
            using (SATEntities db = new SATEntities())
            {
                var lists = new List<YearCapabilityViewModel>();
                lists = db.tb_Capability.GroupBy(g => g.CapYear).Select(group => new { CapYear = group.Key })
                            .Select(s => new YearCapabilityViewModel()
                            {
                                Year = (int)s.CapYear
                            })
                            .OrderByDescending(x => x.Year).ToList();
                return lists;
            }
        }

        public List<CapabilityModel> GetCapability(int? year)
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.vw_Capability.Select(s => new CapabilityModel()
                {
                    CapID = s.CapID,
                    CapYear = (int)s.CapYear,
                    CapName = s.CapYear + "/" + s.CapGName + "/" + s.CapGTName,
                })
                .OrderBy(x => x.CapName).ToList();

                if(year.HasValue)
                    list = list.Where(x => x.CapYear == year).ToList();

                return list;
            }
        }

    }
}