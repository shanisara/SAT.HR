using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;
using SAT.HR.Helpers;

namespace SAT.HR.Data.Repository
{
    public class SalaryRepository
    {
        public SalaryResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Salary.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.SaLevel.ToString().Contains(filter) || x.SaStep.ToString().Contains(filter) || x.SaRate.ToString().Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "SaLevel":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaLevel).ToList() : data.OrderByDescending(x => x.SaLevel).ToList();
                        break;
                    case "SaStep":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaStep).ToList() : data.OrderByDescending(x => x.SaStep).ToList();
                        break;
                    case "saRate":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SaRate).ToList() : data.OrderByDescending(x => x.SaRate).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new SalaryViewModel()
                {
                    RowNumber = i + 1,
                    SaID = s.SaID,
                    SaLevel = s.SaLevel,
                    SaStep = s.SaStep,
                    SaRate = s.SaRate
                }).Skip(start * length).Take(length).ToList();


                SalaryResult result = new SalaryResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<SalaryViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Salary.Select(s => new SalaryViewModel()
                {
                    SaID = s.SaID,
                    SaLevel = s.SaLevel,
                    SaStep = s.SaStep,
                    SaRate = s.SaRate
                }).OrderBy(x => x.SaLevel).ToList();
                return list;
            }
        }

        public SalaryViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Salary.Where(x => x.SaID == id).FirstOrDefault();
                SalaryViewModel model = new Models.SalaryViewModel();
                model.SaID = data.SaID;
                model.SaLevel = data.SaLevel;
                model.SaStep = data.SaStep;
                model.SaRate = data.SaRate;
                return model;
            }
        }

        public decimal GetSalaryRate(int level, int step)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Salary.Where(m => m.SaLevel == level && m.SaStep == step).FirstOrDefault();
                return Convert.ToDecimal(data.SaRate);
            }
        }

        public ResponseData AddByEntity(SalaryViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Salary model = new tb_Salary();
                    model.SaID = data.SaID;
                    model.SaLevel = data.SaLevel;
                    model.SaStep = data.SaStep;
                    model.SaRate = data.SaRate;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Salary.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(SalaryViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Salary.Single(x => x.SaID == newdata.SaID);
                    data.SaLevel = newdata.SaLevel;
                    data.SaStep = newdata.SaStep;
                    data.SaRate = newdata.SaRate;
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
                    var obj = db.tb_Salary.SingleOrDefault(c => c.SaID == id);
                    if (obj != null)
                    {
                        db.tb_Salary.Remove(obj);
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

        public List<SalaryLevelViewModel> GetSalaryLevel()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Salary.Select(s => new SalaryLevelViewModel()
                {
                    Level = (int)s.SaLevel
                })
                .Distinct().OrderBy(x => x.Level).ToList();

                return list;
            }
        }

        public List<SalaryStepViewModel> GetSalaryStep(int? level)
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Salary.Where(m => m.SaLevel == level).Select(s => new SalaryStepViewModel()
                {
                    Step = (decimal)s.SaStep
                })
                .Distinct().OrderBy(x => x.Step).ToList();

                return list;
            }
        }

        public decimal GetSalary(int level, decimal step)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Salary.Where(m => m.SaLevel == level && m.SaStep == step).FirstOrDefault();
                if (data != null)
                    return Convert.ToDecimal(data.SaRate);
                else
                    return 0;
            }
        }
    }
}