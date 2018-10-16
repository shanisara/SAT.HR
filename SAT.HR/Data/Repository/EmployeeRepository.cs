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
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FirstName + " " + s.LastName,
                    UserTName = s.UserTName,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    //PoCode = s.PoCode,
                    PoName = s.PoName,
                    SexID = s.SexID,
                    SexName = s.SexName,
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
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    UserTID = s.UserTID,
                    UserTName = s.UserTName,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoName = s.PoName,
                    SexID = s.SexID,
                    SexName = s.SexName,
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
                model.FullName = data.FirstName + " " + data.LastName;
                model.Avatar = data.Avatar;
                model.SecID = data.SecID;
                model.SecName = data.SecName;
                model.DivID = data.DivID;
                model.DivName = data.DivName;
                model.DepID = data.DepID;
                model.DepName = data.DepName;
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
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FirstName + " " + s.LastName,
                    UserTName = s.UserTName,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoName = s.PoName,
                    SexID = s.SexID,
                    SexName = s.SexName,
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
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    DivID = s.DivID,
                    DivName = s.DivName,
                    DepID = s.DepID,
                    DepName = s.DepName,
                    SecID = s.SecID,
                    SecName = s.SecName,
                    PoID = s.PoID,
                    PoName = s.PoName,
                    UserTID = s.UserTID,
                    UserTName = s.UserTName,
                    SexID = s.SexID,
                    SexName = s.SexName,
                    IsActive = s.IsActive,
                    FullName = s.FirstName + " " + s.LastName,
                    Avatar = s.Avatar,
                }).FirstOrDefault();

                return data;
            }
        }




        public List<FamilyTypeViewModel> GetFamilyType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Family_Type.Select(s => new FamilyTypeViewModel()
                {
                    FamTID = s.FamTID,
                    FamTName = s.FamTName
                }).OrderBy(x => x.FamTName).ToList();
                return list;
            }
        }

        public List<UserStatusViewModel> GetUserStatus()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_User_Status.Select(s => new UserStatusViewModel()
                {
                    UserSID = s.UserSID,
                    UserSName = s.UserSName
                }).ToList();
                return list;
            }
        }

        //GetMaritalStatus
        //GetBloodType

    }
}