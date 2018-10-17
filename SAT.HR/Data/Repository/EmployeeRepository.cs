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
        public EmployeeViewModel Login(string username, string password)
        {
            var data = new EmployeeViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    data = db.vw_User.Where(m => m.UserName == username && m.Password == password).Select(s => new EmployeeViewModel()
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
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }

        public EmployeePageResult GetUserNotInUserRole(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            EmployeePageResult result = new EmployeePageResult();

            try
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

                    result.draw = draw ?? 0;
                    result.recordsTotal = recordsTotal;
                    result.recordsFiltered = recordsFiltered;
                    result.data = list;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public EmployeePageResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            EmployeePageResult result = new EmployeePageResult();

            try
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

                    result.draw = draw ?? 0;
                    result.recordsTotal = recordsTotal;
                    result.recordsFiltered = recordsFiltered;
                    result.data = list;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public List<EmployeeViewModel> GetAll()
        {
            var list = new List<EmployeeViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    list = db.vw_User.Select(s => new EmployeeViewModel()
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
                }
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        public EmployeeViewModel GetByID(int id)
        {
            EmployeeViewModel model = new Models.EmployeeViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_User.Where(x => x.UserID == id).FirstOrDefault();

                    model.UserID = data.UserID;
                    model.UserName = data.UserName;
                    model.Password = data.Password;
                    model.TitleID = data.TitleID;
                    model.FirstNameTh = data.FirstNameTh;
                    model.LastNameTh = data.LastNameTh;
                    model.FirstNameEn = data.FirstNameEn;
                    model.LastNameEn = data.LastNameEn;
                    model.StatusID = data.StatusID;
                    model.IDCard = data.IDCard;
                    model.TIN = data.TIN;
                    model.SexID = data.SexID;
                    model.BloodID = data.BloodID;
                    model.ReligionID = data.ReligionID; ;
                    model.EthnicityID = data.EthnicityID;
                    model.NationalityID = data.NationalityID;
                    model.MaritalStatusID = data.MaritalStatusID;
                    model.Birthday = data.Birthday;
                    model.RetireDate = data.RetireDate;
                    model.StartWorkDate = data.StartWorkDate;
                    model.ProbationDate = data.ProbationDate;
                    model.Remuneration = data.Remuneration;
                    model.WorkingTypeID = data.WorkingTypeID;
                    model.FingerScan = data.FingerScan;
                    model.CardScan = data.CardScan;
                    model.SalaryLevel = data.SalaryLevel;
                    model.SalaryStep = data.SalaryStep;
                    model.DivID = data.DivID;
                    model.DepID = data.DepID;
                    model.SecID = data.SecID;
                    model.PoID = data.PoID;
                    model.EmpowerID = data.EmpowerID;
                    model.EmpowerDivID = data.EmpowerDivID;
                    model.EmpowerDepID = data.EmpowerDepID;
                    model.EmpowerSecID = data.EmpowerSecID;
                    model.PoTID = data.PoTID;
                    model.AgentDivID = data.AgentDivID;
                    model.AgentDepID = data.AgentDepID;
                    model.AgentSecID = data.AgentSecID;
                    model.AgentPoID = data.AgentPoID;
                    model.HomeAddr = data.HomeAddr;
                    model.HomeSubDistrictID = data.HomeSubDistrictID;
                    model.HomeDistrictID = data.HomeDistrictID;
                    model.HomeProvinceID = data.HomeProvinceID;
                    model.HomeZipCode = data.HomeZipCode;
                    model.CurrAddr = data.CurrAddr;
                    model.CurrSubDistrictID = data.CurrSubDistrictID;
                    model.CurrDistrictID = data.CurrDistrictID;
                    model.CurrProvinceID = data.CurrProvinceID;
                    model.CurrZipCode = data.CurrZipCode;
                    model.Telephone = data.Telephone;
                    model.Email = data.Email;
                    model.ContactName = data.ContactName;
                    model.ContactPhone = data.ContactPhone;
                    model.Avatar = data.Avatar;
                    model.IsActive = data.IsActive;
                    model.CreateDate = data.CreateDate;
                    model.CreateBy = data.CreateBy;
                    model.ModifyDate = data.ModifyDate;
                    model.ModifyBy = data.ModifyBy;
                    model.DivName = data.DivName;
                    model.DepName = data.DepName;
                    model.SecName = data.SecName;
                    model.PoCode = data.PoCode;
                    model.PoName = data.PoName;
                    //model.Salary = data.Salary;
                    //model.Age = data.Age;
                    //model.Experience = data.Experience;
                    model.FullName = data.FirstNameTh + " " + data.LastNameTh;
                    model.SecName = data.SecName;
                    model.DivName = data.DivName;
                    model.DepName = data.DepName;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }


        #region

        public UserFamilyViewModel GetUserFamily(int userid)
        {
            UserFamilyViewModel data = new UserFamilyViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var listFather = db.tb_User_Family.Where(w => w.RecID == 2).Select(s => new UserFamilyViewModel {
                        #region Father

                        UfID = s.UfID,
                        UfName = s.UfName,
                        UfCardID = s.UfCardID,
                        UfDOB = s.UfDOB,
                        UfAge = null,
                        UfLifeStatus = s.UfLifeStatus,
                        UfLifeStatusName = null,
                        TdID = s.TdID,
                        TdName = null,
                        PoID = s.PoID,
                        PoName = null,
                        RecID = s.RecID,
                        UserID = s.UserID,

                        #endregion
                    }).ToList();

                    var listMother = db.tb_User_Family.Where(w => w.RecID == 3).Select(s => new UserFamilyViewModel {
                        #region Mother

                        UfID = s.UfID,
                        UfName = s.UfName,
                        UfCardID = s.UfCardID,
                        UfDOB = s.UfDOB,
                        UfAge = null,
                        UfLifeStatus = s.UfLifeStatus,
                        UfLifeStatusName = null,
                        TdID = s.TdID,
                        TdName = null,
                        PoID = s.PoID,
                        PoName = null,
                        RecID = s.RecID,
                        UserID = s.UserID,

                        #endregion
                    }).ToList();

                    var listSpouse = db.tb_User_Family.Where(w => w.RecID == 4).Select(s => new UserFamilyViewModel {
                        #region Spouse

                        UfID = s.UfID,
                        UfName = s.UfName,
                        UfCardID = s.UfCardID,
                        UfDOB = s.UfDOB,
                        UfAge = null,
                        WeddingDate = null,
                        DivorceDate = null,
                        UfLifeStatus = s.UfLifeStatus,
                        UfLifeStatusName = null,
                        MaritalID = null,
                        MaritalName = null,
                        Career = null,
                        RecID = s.RecID,
                        UserID = s.UserID,

                        #endregion
                    }).ToList();

                    var listChild = db.tb_User_Family.Where(w => w.RecID == 5).Select(s => new UserFamilyViewModel {
                        #region Child

                        UfID = s.UfID,
                        UfName = s.UfName,
                        UfCardID = s.UfCardID,
                        UfDOB = s.UfDOB,
                        UfAge = null,
                        UfLifeStatusName = null,
                        UfLifeStatus = s.UfLifeStatus,
                        UfStudyStatus = s.UfStudyStatus,
                        UfStudyStatusName = null,

                        #endregion
                    }).ToList();

                    data.ListFather = listFather;
                    data.ListMother = listMother;
                    data.ListSpouse = listSpouse;
                    data.ListChild = listChild;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public UserEducationViewModel GetUserEducation(int userid)
        {
            UserEducationViewModel data = new UserEducationViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public UserPositionViewModel GetUserPosition(int userid)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public UserTrainningViewModel GetUserTrainning(int userid)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public UserInsigniaViewModel GetUserInsignia(int userid)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public UserExcellentViewModel GetUserExcellent(int userid)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public UserCertificateViewModel GetUserCertificate(int userid)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public UserHistoryViewModel GetUserHistory(int userid)
        {
            try
            {
                using (SATEntities db = new SATEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        #endregion


        #region DropDownList

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

        public List<RecieveTypeViewModel> GetRecieveType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Recieve_Type.Select(s => new RecieveTypeViewModel()
                {
                    RecID = s.RecID,
                    RecName = s.RecName
                }).OrderBy(x => x.RecName).ToList();
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

        #endregion

    }
}