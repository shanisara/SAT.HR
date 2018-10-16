using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class EmployeeRepository
    {
        public EmployeePageResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_User.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.UserName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "UserName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.UserName).ToList() : data.OrderByDescending(x => x.UserName).ToList();
                        break;

                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new EmployeeViewModel()
                {
                    RowNumber = ++i,
                    UserID = s.UserID,
                    UserName = s.UserName,
                    Password = s.Password,
                    FullName = s.FirstNameTh + " " + s.LastNameTh,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoCode = s.PoCode,
                    PoName = s.PoName
                }).Skip(start * length).Take(length).ToList();


                EmployeePageResult result = new EmployeePageResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<EmployeeViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.vw_User.Select(s => new EmployeeViewModel()
                {
                    UserID = s.UserID,
                    UserName = s.UserName,
                    Password = s.Password,
                    FirstNameTh = s.FirstNameTh,
                    LastNameTh = s.LastNameTh,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoName = s.PoName,
                })
                .OrderBy(x => x.UserName).ToList();
                return list;
            }
        }

        public List<EmployeeViewModel> GetEmployee()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = GetAll();

                return list;
            }
        }

        public EmployeeViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_User.Where(x => x.UserID == id).FirstOrDefault();
                EmployeeViewModel model = new Models.EmployeeViewModel();
                model.UserID = data.UserID;
                model.UserName = data.UserName;
                model.IDCard = data.IDCard;
                model.FullName = data.FirstNameTh + " " + data.LastNameTh;
                model.Avatar = data.Avatar;
                model.SecID = data.SecID;
                model.SecName = data.SecName;
                model.DivID = data.DivID;
                model.DivName = data.DivName;
                model.DepID = data.DepID;
                model.DepName = data.DepName;
                model.Avatar = data.Avatar;
                return model;
            }
        }

        public EmployeePageResult GetUserNotInUserRole(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_User_NotRole.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.UserName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "UserName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.UserName).ToList() : data.OrderByDescending(x => x.UserName).ToList();
                        break;

                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new EmployeeViewModel()
                {
                    RowNumber = ++i,
                    UserID = s.UserID,
                    UserName = s.UserName,
                    Password = s.Password,
                    FirstNameTh = s.FirstName,
                    LastNameTh = s.LastName,
                    FullName = s.FirstName + " " + s.LastName,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoName = s.PoName,
                }).Skip(start * length).Take(length).ToList();


                EmployeePageResult result = new EmployeePageResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public EmployeeViewModel Login(string username, string password)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_User.Where(m => m.UserName == username && m.Password == password).Select(s => new EmployeeViewModel()
                {
                    UserID = s.UserID,
                    UserName = s.UserName,
                    Password = s.Password,
                    FirstNameTh = s.FirstNameTh,
                    LastNameTh = s.LastNameTh,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoName = s.PoName,
                    IsActive = s.IsActive,
                    FullName = s.FirstNameTh + " " + s.LastNameTh,
                    Avatar = s.Avatar,
                }).FirstOrDefault();

                return data;
            }
        }







        public List<WorkingTypeViewModel> GetWorkingType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Working_Type.Select(s => new WorkingTypeViewModel()
                {
                    WorkingTypeID = s.WorkingTypeID,
                    WorkingTypeName = s.WorkingTypeName
                }).ToList();
                return list;
            }
        }

        public List<UserStatusViewModel> GetUserStatus()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_User_Status.Select(s => new UserStatusViewModel()
                {
                    UserStatusID = s.StatusID,
                    UserStatusName = s.StatusName
                }).ToList();
                return list;
            }
        }

        public List<FamilyTypeViewModel> GetFamilyType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Family_Type.Select(s => new FamilyTypeViewModel()
                {
                    FamilyTypeID = s.FamTID,
                    FamilyTypeName = s.FamTName
                }).OrderBy(x => x.FamilyTypeName).ToList();
                return list;
            }
        }

        public List<MaritalStatusViewModel> GetMaritalStatus()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Marital_Status.Select(s => new MaritalStatusViewModel()
                {
                    MaritalStatusID = s.MaritalID,
                    MaritalStatusName = s.MaritalName
                }).ToList();
                return list;
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

        public List<EmpowerViewModel> GetEmpower()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Empower.Select(s => new EmpowerViewModel()
                {
                    EmpowerID = s.EmpowerID,
                    EmpowerName = s.EmpowerName
                }).ToList();
                return list;
            }
        }

        public List<BloodTypeViewModel> GetBloodType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Blood_Type.Select(s => new BloodTypeViewModel()
                {
                    BloodTypeID = s.BloodID,
                    BloodTypeName = s.BloodName
                }).ToList();
                return list;
            }
        }



    }
}