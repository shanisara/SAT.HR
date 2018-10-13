using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class EmployeeRepository
    {
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

        public EmployeePageResult GetUserNotInUserRole(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_UserNotInUserRole.ToList();

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


    }
}