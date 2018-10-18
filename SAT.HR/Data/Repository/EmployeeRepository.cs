using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
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
        public UserProfile Login(string username, string password)
        {
            var data = new UserProfile();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    data = db.vw_User.Where(m => m.UserName == username && m.Password == password).Select(s => new UserProfile()
                    {
                        UserID = s.UserID,
                        UserName = s.UserName,
                        FullName = s.FirstNameTh + " " + s.LastNameTh,
                        DivID = s.DivID,
                        DivName = s.DivName,
                        DepID = s.DepID,
                        DepName = s.DepName,
                        SecID = s.SecID,
                        SecName = s.SecName,
                        PoID = s.PoID,
                        PoName = s.PoName,
                        Avatar = s.Avatar,
                        IsActive = s.IsActive,
                        RoleID = 1,
                        RoleName = "Admin"
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }

        public UserProfile LoginByID(int userid)
        {
            var data = GetByID(userid);
            UserProfile model = new Models.UserProfile();
            model.UserID = data.UserID;
            model.UserName = data.UserName;
            model.FullName = data.FullName;
            model.DivID = data.DivID;
            model.DivName = data.DivName;
            model.DepID = data.DepID;
            model.DepName = data.DepName;
            model.SecID = data.SecID;
            model.SecName = data.SecName;
            model.PoID = data.PoID;
            model.PoName = data.PoName;
            model.Avatar = !string.IsNullOrEmpty(data.Avatar) ? data.Avatar : "avatar.png";
            model.IsActive = data.IsActive;
            model.RoleID = 1;
            model.RoleName = "Admin";
            return model;
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


        #region Tab: User-Employee

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

        //public List<EmployeeViewModel> GetAll()
        //{
        //    var list = new List<EmployeeViewModel>();

        //    try
        //    {
        //        using (SATEntities db = new SATEntities())
        //        {
        //            list = db.vw_User.Select(s => new EmployeeViewModel()
        //            {
        //                UserID = s.UserID,
        //                UserName = s.UserName,
        //                Password = s.Password,
        //                FirstNameTh = s.FirstNameTh,
        //                LastNameTh = s.LastNameTh,
        //                DivID = s.DivID,
        //                DivName = s.DivName,
        //                DepID = s.DepID,
        //                DepName = s.DepName,
        //                SecID = s.SecID,
        //                SecName = s.SecName,
        //                PoID = s.PoID,
        //                PoName = s.PoName,
        //            })
        //            .OrderBy(x => x.UserName).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return list;
        //}

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
                    //model.CreateDate = data.CreateDate;
                    //model.CreateBy = data.CreateBy;
                    //model.ModifyDate = data.ModifyDate;
                    //model.ModifyBy = data.ModifyBy;
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

        public ResponseData AddUserByEntity(EmployeeViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User model = new tb_User();
                    model.UserID = data.UserID;
                    model.UserName = data.UserName;
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
                    model.ReligionID = data.ReligionID;
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
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateUserByEntity(EmployeeViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User.Single(x => x.UserID == newdata.UserID);
                    //model.UserName = newdata.UserName;
                    model.TitleID = newdata.TitleID;
                    model.FirstNameTh = newdata.FirstNameTh;
                    model.LastNameTh = newdata.LastNameTh;
                    model.FirstNameEn = newdata.FirstNameEn;
                    model.LastNameEn = newdata.LastNameEn;
                    model.StatusID = newdata.StatusID;
                    model.IDCard = newdata.IDCard;
                    model.TIN = newdata.TIN;
                    model.SexID = newdata.SexID;
                    model.BloodID = newdata.BloodID;
                    model.ReligionID = newdata.ReligionID;
                    model.EthnicityID = newdata.EthnicityID;
                    model.NationalityID = newdata.NationalityID;
                    model.MaritalStatusID = newdata.MaritalStatusID;
                    model.Birthday = newdata.Birthday;
                    model.RetireDate = newdata.RetireDate;
                    model.StartWorkDate = newdata.StartWorkDate;
                    model.ProbationDate = newdata.ProbationDate;
                    model.Remuneration = newdata.Remuneration;
                    model.WorkingTypeID = newdata.WorkingTypeID;
                    model.FingerScan = newdata.FingerScan;
                    model.CardScan = newdata.CardScan;
                    model.SalaryLevel = newdata.SalaryLevel;
                    model.SalaryStep = newdata.SalaryStep;
                    model.DivID = newdata.DivID;
                    model.DepID = newdata.DepID;
                    model.SecID = newdata.SecID;
                    model.PoID = newdata.PoID;
                    model.EmpowerID = newdata.EmpowerID;
                    model.EmpowerDivID = newdata.EmpowerDivID;
                    model.EmpowerDepID = newdata.EmpowerDepID;
                    model.EmpowerSecID = newdata.EmpowerSecID;
                    model.PoTID = newdata.PoTID;
                    model.AgentDivID = newdata.AgentDivID;
                    model.AgentDepID = newdata.AgentDepID;
                    model.AgentSecID = newdata.AgentSecID;
                    model.AgentPoID = newdata.AgentPoID;
                    model.HomeAddr = newdata.HomeAddr;
                    model.HomeSubDistrictID = newdata.HomeSubDistrictID;
                    model.HomeDistrictID = newdata.HomeDistrictID;
                    model.HomeProvinceID = newdata.HomeProvinceID;
                    model.HomeZipCode = newdata.HomeZipCode;
                    model.CurrAddr = newdata.CurrAddr;
                    model.CurrSubDistrictID = newdata.CurrSubDistrictID;
                    model.CurrDistrictID = newdata.CurrDistrictID;
                    model.CurrProvinceID = newdata.CurrProvinceID;
                    model.CurrZipCode = newdata.CurrZipCode;
                    model.Telephone = newdata.Telephone;
                    model.Email = newdata.Email;
                    model.ContactName = newdata.ContactName;
                    model.ContactPhone = newdata.ContactPhone;
                    //model.Avatar = newdata.Avatar;
                    //model.IsActive = newdata.IsActive;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteUserByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_User.SingleOrDefault(c => c.UserID == id);
                    if (obj != null)
                    {
                        db.tb_User.Remove(obj);
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

        #endregion

        #region Tab: User-Family

        public UserFamilyViewModel GetFamilyByUser(int id)
        {
            UserFamilyViewModel data = new UserFamilyViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var listFather = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == 2).Select(s => new UserFamilyViewModel
                    {
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

                    var listMother = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == 3).Select(s => new UserFamilyViewModel
                    {
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

                    var listSpouse = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == 4).Select(s => new UserFamilyViewModel
                    {
                        #region Spouse

                        UfID = s.UfID,
                        UfName = s.UfName,
                        UfCardID = s.UfCardID,
                        UfDOB = s.UfDOB,
                        UfAge = null,
                        UfWeddingDate = s.UfWeddingDate,
                        DivorceDate = s.DivorceDate,
                        UfLifeStatus = s.UfLifeStatus,
                        UfLifeStatusName = s.UfLifeStatusName,
                        MaritalStatusID = s.MaritalStatusID,
                        MaritalName = null,
                        OcID = null,
                        RecID = s.RecID,
                        UserID = s.UserID,

                        #endregion
                    }).ToList();

                    var listChild = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == 5).Select(s => new UserFamilyViewModel
                    {
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

                    data.UserID = id;
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

        public UserFamilyViewModel GetFamilyByID(int userid, int recid)
        {
            UserFamilyViewModel data = new UserFamilyViewModel();
            data.UserID = userid;
            data.RecID = recid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Family.Where(x => x.UfID == userid).Select(s => new UserFamilyViewModel
                    {
                        UfID = s.UfID,
                        UserID = s.UserID,
                        UfName = s.UfName,
                        UfCardID = s.UfCardID,
                        UfDOB = s.UfDOB,
                        UfLifeStatus = s.UfLifeStatus,
                        TdID = s.TdID,
                        PoID = s.PoID,
                        UfWeddingDate = s.UfWeddingDate,
                        DivorceDate = s.DivorceDate,
                        MaritalStatusID = s.MaritalStatusID,
                        UfStudyStatus = s.UfStudyStatus,
                        RecID = s.RecID,
                    }).FirstOrDefault();

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            
            return data;
        }

        public ResponseData AddFamilyByEntity(UserFamilyViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Family model = new tb_User_Family();
                    model.UfID = data.UfID;
                    model.UserID = data.UserID;
                    model.UfName = data.UfName;
                    model.UfCardID = data.UfCardID;
                    model.UfDOB = data.UfDOB;
                    model.UfLifeStatus = data.UfLifeStatus;
                    model.TdID = data.TdID;
                    model.PoID = data.PoID;
                    model.UfWeddingDate = data.UfWeddingDate;
                    model.DivorceDate = data.DivorceDate;
                    model.MaritalStatusID = data.MaritalStatusID;
                    model.UfStudyStatus = data.UfStudyStatus;
                    model.RecID = data.RecID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Family.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateFamilyeByEntity(UserFamilyViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Family.Single(x => x.UserID == newdata.UserID && x.UfID == newdata.UfID);
                    model.UfName = newdata.UfName;
                    model.UfCardID = newdata.UfCardID;
                    model.UfDOB = newdata.UfDOB;
                    model.UfLifeStatus = newdata.UfLifeStatus;
                    model.TdID = newdata.TdID;
                    model.PoID = newdata.PoID;
                    //model.WeddingDate = newdata.WeddingDate;
                    //model.DivorceDate = newdata.DivorceDate;
                    //model.MaritalID = newdata.MaritalID;
                    model.UfStudyStatus = newdata.UfStudyStatus;
                    model.RecID = newdata.RecID;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteFamilyByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_User_Family.SingleOrDefault(x => x.UserID == userid && x.UfID == id);
                    if (obj != null)
                    {
                        db.tb_User_Family.Remove(obj);
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

        #endregion

        #region Tab: User-Education

        public List<UserEducationViewModel> GetEducationByUser(int userid)
        {
            var model = new List<UserEducationViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_Education.Where(x => x.UserID == userid).Select(s => new UserEducationViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserEducationViewModel GetEducationByID(int id)
        {
            UserEducationViewModel data = new UserEducationViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Education.Where(x => x.UeID == id).Select(s => new UserEducationViewModel
                    {
                        UeID = s.UeID,
                        UserID = s.UserID,
                        EduID = s.EduID,
                        DegID = s.DegID,
                        MajID = s.MajID,
                        UeInstituteName = s.UeInstituteName,
                        CountryID = s.CountryID,
                        UeGraduationDate = s.UeGraduationDate,
                        UeGPA = s.UeGPA,
                        UeEduType = s.UeEduType
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddEducationByEntity(UserEducationViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Education model = new tb_User_Education();
                    model.UeID = data.UeID;
                    model.UserID = data.UserID;
                    model.EduID = data.EduID;
                    model.DegID = data.DegID;
                    model.MajID = data.MajID;
                    model.UeInstituteName = data.UeInstituteName;
                    model.CountryID = data.CountryID;
                    model.UeGraduationDate = data.UeGraduationDate;
                    model.UeGPA = data.UeGPA;
                    model.UeEduType = data.UeEduType;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Education.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateEducationByEntity(UserEducationViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Education.Single(x => x.UserID == newdata.UserID && x.UeID == newdata.UeID);
                    model.EduID = newdata.EduID;
                    model.DegID = newdata.DegID;
                    model.MajID = newdata.MajID;
                    model.UeInstituteName = newdata.UeInstituteName;
                    model.CountryID = newdata.CountryID;
                    model.UeGraduationDate = newdata.UeGraduationDate;
                    model.UeGPA = newdata.UeGPA;
                    model.UeEduType = newdata.UeEduType;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteEducationByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Education.SingleOrDefault(x => x.UserID == userid && x.UeID == id);
                    if (model != null)
                    {
                        db.tb_User_Education.Remove(model);
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

        #endregion

        #region Tab: User-Position

        public List<UserPositionViewModel> GetPositionByUser(int userid)
        {
            var model = new List<UserPositionViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_Position.Where(x => x.UserID == userid).Select(s => new UserPositionViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserPositionViewModel GetPositionByID(int id)
        {
            UserPositionViewModel data = new UserPositionViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Position.Where(x => x.UpID == id).Select(s => new UserPositionViewModel
                    {
                        UpID = s.UpID,
                        UserID = s.UserID,
                        ActID = s.ActID,
                        UpCmd = s.UpCmd,
                        PoTID = s.PoTID,
                        DivID = s.DivID,
                        DepID = s.DepID,
                        SecID = s.SecID,
                        PoID = s.PoID,
                        PoAID = s.PoAID,
                        UpLevel = s.UpLevel,
                        UpSalary = s.UpSalary,
                        UpCmdDate = s.UpCmdDate,
                        UpForceDate = s.UpForceDate,
                        UpRemark = s.UpRemark,
                        UpPathFile = s.UpPathFile,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddPositionByEntity(UserPositionViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Position model = new tb_User_Position();
                    model.UpID = data.UpID;
                    model.UserID = data.UserID;
                    model.ActID = data.ActID;
                    model.UpCmd = data.UpCmd;
                    model.PoTID = data.PoTID;
                    model.DivID = data.DivID;
                    model.DepID = data.DepID;
                    model.SecID = data.SecID;
                    model.PoID = data.PoID;
                    model.PoAID = data.PoAID;
                    model.UpLevel = data.UpLevel;
                    model.UpSalary = data.UpSalary;
                    model.UpCmdDate = data.UpCmdDate;
                    model.UpForceDate = data.UpForceDate;
                    model.UpRemark = data.UpRemark;
                    model.UpPathFile = data.UpPathFile;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Position.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdatePositionByEntity(UserPositionViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Position.Single(x => x.UserID == newdata.UserID && x.UpID == newdata.UpID);
                    model.ActID = newdata.ActID;
                    model.UpCmd = newdata.UpCmd;
                    model.PoTID = newdata.PoTID;
                    model.DivID = newdata.DivID;
                    model.DepID = newdata.DepID;
                    model.SecID = newdata.SecID;
                    model.PoID = newdata.PoID;
                    model.PoAID = newdata.PoAID;
                    model.UpLevel = newdata.UpLevel;
                    model.UpSalary = newdata.UpSalary;
                    model.UpCmdDate = newdata.UpCmdDate;
                    model.UpForceDate = newdata.UpForceDate;
                    model.UpRemark = newdata.UpRemark;
                    model.UpPathFile = newdata.UpPathFile;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeletePositionByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Position.SingleOrDefault(x => x.UserID == userid && x.UpID == id);
                    if (model != null)
                    {
                        db.tb_User_Position.Remove(model);
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

        #endregion

        #region Tab: User-Trainning

        public List<UserTrainningViewModel> GetTrainningByUser(int userid)
        {
            var model = new List<UserTrainningViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_Training.Where(x => x.UserID == userid).Select(s => new UserTrainningViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserTrainningViewModel GetTrainningByID(int id)
        {
            UserTrainningViewModel data = new UserTrainningViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Training.Where(x => x.UtID == id).Select(s => new UserTrainningViewModel
                    {
                        UtID = s.UtID,
                        UserID = s.UserID,
                        TtID = s.TtID,
                        CountryID = s.CountryID,
                        UtCourse = s.UtCourse,
                        UtStartDate = s.UtStartDate,
                        UtEndDate = s.UtEndDate,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddTrainingByEntity(UserTrainningViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Training model = new tb_User_Training();
                    model.UtID = data.UtID;
                    model.UserID = data.UserID;
                    model.TtID = data.TtID;
                    model.CountryID = data.CountryID;
                    model.UtCourse = data.UtCourse;
                    model.UtStartDate = data.UtStartDate;
                    model.UtEndDate = data.UtEndDate;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Training.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateTrainingByEntity(UserTrainningViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Training.Single(x => x.UserID == newdata.UserID && x.TtID == newdata.TtID);
                    model.TtID = newdata.TtID;
                    model.CountryID = newdata.CountryID;
                    model.UtCourse = newdata.UtCourse;
                    model.UtStartDate = newdata.UtStartDate;
                    model.UtEndDate = newdata.UtEndDate;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteTrainingByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Training.SingleOrDefault(x => x.UserID == userid && x.TtID == id);
                    if (model != null)
                    {
                        db.tb_User_Training.Remove(model);
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

        #endregion

        #region Tab: User-Insignia

        public List<UserInsigniaViewModel> GetInsigniaByUser(int userid)
        {
            var model = new List<UserInsigniaViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_Insignia.Where(x => x.UserID == userid).Select(s => new UserInsigniaViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserInsigniaViewModel GetInsigniaByID(int id)
        {
            UserInsigniaViewModel data = new UserInsigniaViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Insignia.Where(x => x.UiID == id).Select(s => new UserInsigniaViewModel
                    {
                        UiID = s.UiID,
                        UserID = s.UserID,
                        InsID = s.InsID,
                        UiYear = s.UiYear,
                        UiBook = s.UiBook,
                        UiPart = s.UiPart,
                        UiPage = s.UiPage,
                        UiRecDate = s.UiRecDate,
                        UiRetDate = s.UiRetDate,
                        UiCmd = s.UiCmd,
                        UiPartFile = s.UiPartFile,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddInsigniaByEntity(UserInsigniaViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Insignia model = new tb_User_Insignia();
                    model.UiID = data.UiID;
                    model.UserID = data.UserID;
                    model.InsID = data.InsID;
                    model.UiYear = data.UiYear;
                    model.UiBook = data.UiBook;
                    model.UiPart = data.UiPart;
                    model.UiPage = data.UiPage;
                    model.UiRecDate = data.UiRecDate;
                    model.UiRetDate = data.UiRetDate;
                    model.UiCmd = data.UiCmd;
                    model.UiPartFile = data.UiPartFile;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Insignia.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateInsigniaByEntity(UserInsigniaViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Insignia.Single(x => x.UserID == newdata.UserID && x.InsID == newdata.InsID);
                    model.InsID = newdata.InsID;
                    model.UiYear = newdata.UiYear;
                    model.UiBook = newdata.UiBook;
                    model.UiPart = newdata.UiPart;
                    model.UiPage = newdata.UiPage;
                    model.UiRecDate = newdata.UiRecDate;
                    model.UiRetDate = newdata.UiRetDate;
                    model.UiCmd = newdata.UiCmd;
                    model.UiPartFile = newdata.UiPartFile;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteInsigniaByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Insignia.SingleOrDefault(x => x.UserID == userid && x.InsID == id);
                    if (model != null)
                    {
                        db.tb_User_Insignia.Remove(model);
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

        #endregion

        #region Tab: User-Excellent

        public List<UserExcellentViewModel> GetExcellentByUser(int userid)
        {
            var model = new List<UserExcellentViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_Excellent.Where(x => x.UserID == userid).Select(s => new UserExcellentViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserExcellentViewModel GetExcellentByID(int id)
        {
            UserExcellentViewModel data = new UserExcellentViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Excellent.Where(x => x.UeID == id).Select(s => new UserExcellentViewModel
                    {
                        UeID = s.UeID,
                        UserID = s.UserID,
                        ExID = s.ExID,
                        UeProjectName = s.UeProjectName,
                        UeRecYear = s.UeRecYear,
                        UeRecDate = s.UeRecDate,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddExcellentByEntity(UserExcellentViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Excellent model = new tb_User_Excellent();
                    model.UeID = data.UeID;
                    model.UserID = data.UserID;
                    model.ExID = data.ExID;
                    model.UeProjectName = data.UeProjectName;
                    model.UeRecYear = data.UeRecYear;
                    model.UeRecDate = data.UeRecDate;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Excellent.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateExcellentByEntity(UserExcellentViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Excellent.Single(x => x.UserID == newdata.UserID && x.ExID == newdata.ExID);
                    model.ExID = newdata.ExID;
                    model.UeProjectName = newdata.UeProjectName;
                    model.UeRecYear = newdata.UeRecYear;
                    model.UeRecDate = newdata.UeRecDate;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteExcellentByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Excellent.SingleOrDefault(x => x.UserID == userid && x.ExID == id);
                    if (model != null)
                    {
                        db.tb_User_Excellent.Remove(model);
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

        #endregion

        #region Tab: User-Certificate

        public List<UserCertificateViewModel> GetCertificateByUser(int userid)
        {
            var model = new List<UserCertificateViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_Certificate.Where(x => x.UserID == userid).Select(s => new UserCertificateViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserCertificateViewModel GetCertificateByID(int id)
        {
            UserCertificateViewModel data = new UserCertificateViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_Certificate.Where(x => x.UcID == id).Select(s => new UserCertificateViewModel
                    {
                        UcID = s.UcID,
                        UserID = s.UserID,
                        CerId = s.CerId,
                        UcRecDate = s.UcRecDate,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddCertificateByEntity(UserCertificateViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_Certificate model = new tb_User_Certificate();
                    model.UcID = data.UcID;
                    model.UserID = data.UserID;
                    model.CerId = data.CerId;
                    model.UcRecDate = data.UcRecDate;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Certificate.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateCertificateByEntity(UserCertificateViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_Certificate.Single(x => x.UserID == newdata.UserID && x.CerId == newdata.CerId);
                    model.CerId = newdata.CerId;
                    model.UcRecDate = newdata.UcRecDate;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteCertificateByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Certificate.SingleOrDefault(x => x.UserID == userid && x.CerId == id);
                    if (model != null)
                    {
                        db.tb_User_Certificate.Remove(model);
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

        #endregion

        #region Tab: User-History

        public List<UserHistoryViewModel> GetHistoryByUser(int userid)
        {
            var model = new List<UserHistoryViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    model = db.vw_User_History.Where(x => x.UserID == userid).Select(s => new UserHistoryViewModel
                    {

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserHistoryViewModel GetHistoryByID(int id)
        {
            UserHistoryViewModel data = new UserHistoryViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var model = db.tb_User_History.Where(x => x.UhID == id).Select(s => new UserHistoryViewModel
                    {
                        UhID = s.UhID,
                        UserID = s.UserID,
                        UhEditDate = s.UhEditDate,
                        TiID = s.TiID,
                        UhFirstNameTH = s.UhFirstNameTH,
                        UhLastNameTH = s.UhLastNameTH,
                        UhFirstNameEN = s.UhFirstNameEN,
                        UhLastNameEN = s.UhLastNameEN,
                        Remark = s.Remark,
                        UhStatus = s.UhStatus,
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddHistoryByEntity(UserHistoryViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User_History model = new tb_User_History();
                    model.UhID = data.UhID;
                    model.UserID = data.UserID;
                    model.UhEditDate = data.UhEditDate;
                    model.TiID = data.TiID;
                    model.UhFirstNameTH = data.UhFirstNameTH;
                    model.UhLastNameTH = data.UhLastNameTH;
                    model.UhFirstNameEN = data.UhFirstNameEN;
                    model.UhLastNameEN = data.UhLastNameEN;
                    model.Remark = data.Remark;
                    model.UhStatus = data.UhStatus;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_History.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateHistoryByEntity(UserHistoryViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_History.Single(x => x.UserID == newdata.UserID && x.UhID == newdata.UhID);
                    model.UhEditDate = newdata.UhEditDate;
                    model.TiID = newdata.TiID;
                    model.UhFirstNameTH = newdata.UhFirstNameTH;
                    model.UhLastNameTH = newdata.UhLastNameTH;
                    model.UhFirstNameEN = newdata.UhFirstNameEN;
                    model.UhLastNameEN = newdata.UhLastNameEN;
                    model.Remark = newdata.Remark;
                    model.UhStatus = newdata.UhStatus;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData DeleteHistoryByID(int userid, int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_History.SingleOrDefault(x => x.UserID == userid && x.UhID == id);
                    if (model != null)
                    {
                        db.tb_User_History.Remove(model);
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

        public List<OccupationViewModel> GetOccupation()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Occupation.Select(s => new OccupationViewModel()
                {
                    OcID = s.OcID,
                    OcName = s.OcName
                }).ToList();
                return list;
            }
        }
        

        #endregion

    }
}