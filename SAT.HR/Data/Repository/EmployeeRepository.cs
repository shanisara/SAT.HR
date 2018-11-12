using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class EmployeeRepository
    {
        #region Login & Role

        public UserProfile Login(string username, string password)
        {
            var data = new UserProfile();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    data = db.vw_Employee.Where(m => m.UserName == username && m.Password == password).Select(s => new UserProfile()
                    {
                        UserID = s.UserID,
                        UserName = s.UserName,
                        FullNameTh = s.FullNameTh,
                        Avatar = SysConfig.ApplicationRoot + (!string.IsNullOrEmpty(s.Avatar) ? SysConfig.PathDownloadUserAvatar +"/"+ s.Avatar : "Content/assets/img/default-avatar.png"),
                        Email = s.Email,
                        DivID = s.DivID,
                        DivName = s.DivName,
                        DepID = s.DepID,
                        DepName = s.DepName,
                        SecID = s.SecID,
                        SecName = s.SecName,
                        PoID = s.PoID,
                        PoName = s.PoName,
                        RoleID = s.RoleID,
                        IsActive = s.IsActive,
                        StatusID = s.StatusID,
                        UserTypeID = s.UserTypeID
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
            UserProfile model = new Models.UserProfile();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_Employee.Where(m => m.UserID == userid).FirstOrDefault();

                    model.UserID = data.UserID;
                    model.UserName = data.UserName;
                    model.FullNameTh = data.FullNameTh;
                    model.Avatar = SysConfig.ApplicationRoot + (!string.IsNullOrEmpty(data.Avatar) ? SysConfig.PathDownloadUserAvatar + data.Avatar : "Content/assets/img/image_placeholder.jpg");
                    model.Email = data.Email;
                    model.DivID = data.DivID;
                    model.DivName = data.DivName;
                    model.DepID = data.DepID;
                    model.DepName = data.DepName;
                    model.SecID = data.SecID;
                    model.SecName = data.SecName;
                    model.PoID = data.PoID;
                    model.PoName = data.PoName;
                    model.RoleID = data.RoleID;
                    model.IsActive = data.IsActive;
                    model.StatusID = data.StatusID;
                    model.UserTypeID = data.UserTypeID;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public EmployeePageResult GetUserNotInRole(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
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
                        FullNameTh = s.FullNameTh,
                        DivID = s.DivID,
                        DivName = s.DivName,
                        DepID = s.DepID,
                        DepName = s.DepName,
                        SecID = s.SecID,
                        SecName = s.SecName,
                        PoID = s.PoID,
                        PoCode = s.PoCode,
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

        #endregion

        #region 1.1 Tab: User-Employee

        public EmployeePageResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, string userType, string userStatus)
        {
            EmployeePageResult result = new EmployeePageResult();
            List<EmployeeViewModel> list = new List<EmployeeViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    string perPage = initialPage.HasValue ? Convert.ToInt32(initialPage) == 0 ? "1" : initialPage.ToString() : "1";
                    var data = db.sp_Employee_List(pageSize.ToString(), perPage, sortBy, sortDir, userType, userStatus, filter).ToList();

                    int i = 0;
                    foreach (var item in data)
                    {
                        EmployeeViewModel model = new EmployeeViewModel();
                        model.RowNumber = ++i;
                        model.UserID = item.UserID;
                        model.IDCard = item.IDCard;
                        model.UserName = item.UserName;
                        model.FullNameTh = item.FullNameTh;
                        model.DivID = item.DivID;
                        model.DivName = item.DivName;
                        model.DepID = item.DepID;
                        model.DepName = item.DepName;
                        model.SecID = item.SecID;
                        model.SecName = item.SecName;
                        model.PoID = item.PoID;
                        model.PoCode = item.PoCode;
                        model.PoName = item.PoName;
                        model.recordsTotal = (int)item.recordsTotal;
                        model.recordsFiltered = (int)item.recordsFiltered;
                        list.Add(model);
                    }

                    result.draw = draw ?? 0;
                    result.recordsTotal = list.Count != 0 ? list[0].recordsTotal : 0;
                    result.recordsFiltered = list.Count != 0 ? list[0].recordsFiltered : 0;
                    result.data = list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
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
                    model.Avatar = SysConfig.ApplicationRoot + (!string.IsNullOrEmpty(data.Avatar) ? SysConfig.PathDownloadUserAvatar + "/" + data.Avatar : "Content/assets/img/image_placeholder.jpg");
                    model.IsActive = data.IsActive;
                    model.UserType = data.UserTID;
                    model.Age = data.Age;
                    model.FullNameTh = data.FirstNameTh + " " + data.LastNameTh;

                    model.DivID = data.DivID;
                    model.DepID = data.DepID;
                    model.SecID = data.SecID;
                    model.PoID = data.PoID;
                    model.SalaryLevel = data.SalaryLevel;
                    model.SalaryStep = data.SalaryStep;
                    model.Salary = data.Salary.HasValue ? (decimal)data.Salary : 0;

                    model.DivName = data.DivName;
                    model.DepName = data.DepName;
                    model.SecName = data.SecName;
                    model.PoName = data.PoName;

                    model.EmpowerID = data.EmpowerID;
                    model.EmpowerDivID = data.EmpowerDivID;
                    model.EmpowerDepID = data.EmpowerDepID;
                    model.EmpowerSecID = data.EmpowerSecID;

                    model.AgentPoAID = data.AgentPoAID;
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

                    //model.Experience = data.Experience;
                    //model.CreateDate = data.CreateDate;
                    //model.CreateBy = data.CreateBy;
                    //model.ModifyDate = data.ModifyDate;
                    //model.ModifyBy = data.ModifyBy;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public List<UserProfile> GetEmployee(int? userType)
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.vw_Employee.Where(x => x.StatusID == 1 && x.UserTypeID == userType && x.IsActive == true).Select(s => new UserProfile()
                {
                    UserID = s.UserID,
                    UserName = s.FullNameTh
                }).ToList();
                return list;
            }
        }

        public ResponseData AddUserByEntity(EmployeeViewModel data, HttpPostedFileBase fileUpload)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_User model = new tb_User();

                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadUserAvatar;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFileName = data.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFileName);

                        fileUpload.SaveAs(fileLocation);

                        model.Avatar = newFileName;
                    }

                    model.UserTID = data.UserType;
                    //model.UserName = data.UserName;
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

                    int userid = model.UserID;
                    result.ID = userid;
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdateUserByEntity(EmployeeViewModel newdata, HttpPostedFileBase fileUpload)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        var model = db.tb_User.Single(x => x.UserID == newdata.UserID);

                        if (fileUpload != null && fileUpload.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(fileUpload.FileName);
                            var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                            string directory = SysConfig.PathUploadUserAvatar;
                            bool isExists = System.IO.Directory.Exists(directory);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(directory);

                            string newFileName = newdata.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                            string fileLocation = Path.Combine(directory, newFileName);

                            fileUpload.SaveAs(fileLocation);

                            model.Avatar = newFileName;
                        }

                        #region Information

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

                        model.EmpowerID = newdata.EmpowerID;
                        model.EmpowerDivID = newdata.EmpowerDivID;
                        model.EmpowerDepID = newdata.EmpowerDepID;
                        model.EmpowerSecID = newdata.EmpowerSecID;

                        model.AgentPoAID = newdata.AgentPoAID;
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

                        #endregion

                        #region Division / Department /Section / Position

                        var man = db.tb_Man_Power.Where(x => x.UserID == newdata.UserID).FirstOrDefault();
                        if (man != null)
                        {
                            //var po = db.tb_Man_Power.Where(x => x.MpID == newdata.PoID).FirstOrDefault();
                            //if (po != null)
                            //{
                            man.DivID = newdata.DivID;
                            man.DepID = newdata.DepID;
                            man.SecID = newdata.SecID;
                            man.PoID = newdata.PoID;
                            man.TypeID = newdata.UserType;
                            db.SaveChanges();
                            //}
                        }
                        //else
                        //{
                        //    int maxID = db.tb_Man_Power.Where(m => m.TypeID == newdata.UserType).Max(m => (int)m.MpID);

                        //    tb_Man_Power objMan = new tb_Man_Power();
                        //    objMan.MpID = maxID;
                        //    objMan.DivID = newdata.DivID;
                        //    objMan.DepID = newdata.DepID;
                        //    objMan.SecID = newdata.SecID;
                        //    objMan.PoID = newdata.SecID;
                        //    objMan.TypeID = newdata.UserType;
                        //    objMan.UserID = newdata.UserID;
                        //    objMan.CreateBy = UtilityService.User.UserID;
                        //    objMan.CreateDate = DateTime.Now;
                        //    objMan.ModifyBy = UtilityService.User.UserID;
                        //    objMan.ModifyDate = DateTime.Now;
                        //    db.tb_Man_Power.Add(objMan);
                        //    db.SaveChanges();
                        //}

                        #endregion

                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }
                    return result;
                }
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

        public ResponseData UpdateProvidentFund(int id, string fundno, string funddate)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User.SingleOrDefault(c => c.UserID == id);
                    if (model != null)
                    {
                        model.ProvidentFundNo = fundno;
                        if (Convert.ToDateTime(funddate) > DateTime.MinValue)
                            model.ProvidentFundDate = Convert.ToDateTime(funddate);
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

        #region 1.2 Tab: User-Family

        public UserFamilyViewModel GetFamily(int id)
        {
            UserFamilyViewModel data = new UserFamilyViewModel();
            using (SATEntities db = new SATEntities())
            {
                var countFather = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == 2).Count();
                var countMother = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == 3).Count();
                data.CountFather = countFather;
                data.CountMother = countMother;
            }
            return data;
        }

        public UserFamilyViewModel GetFamilyByUser(int id, int? recid)
        {
            UserFamilyViewModel data = new UserFamilyViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    List<UserFamilyViewModel> list = new List<UserFamilyViewModel>();

                    int index = 1;
                    var family = db.vw_User_Family.Where(w => w.UserID == id && w.RecID == recid).ToList();
                    foreach (var s in family)
                    {
                        UserFamilyViewModel model = new UserFamilyViewModel();
                        model.RowNumber = index++;
                        model.UfID = s.UfID;
                        model.UfName = s.UfName;
                        model.UfCardID = s.UfCardID;
                        model.UfDOB = s.UfDOB;
                        model.UfDOBText = s.UfDOB.HasValue ? s.UfDOB.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UfAge = s.Age;
                        model.UfWeddingDate = s.UfWeddingDate;
                        model.UfWeddingDateText = s.UfWeddingDate.HasValue ? s.UfWeddingDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.DivorceDate = s.DivorceDate;
                        model.DivorceDateText = s.DivorceDate.HasValue ? s.DivorceDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UfLifeStatus = s.UfLifeStatus;
                        model.UfLifeStatusName = s.UfLifeStatusName;
                        model.MaritalStatusID = s.MaritalStatusID;
                        model.MaritalName = s.MaritalName;
                        model.OcID = s.OcID;
                        model.OcName = s.OcName;
                        model.TdStatus = s.TdStatus;
                        model.TdName = s.TdName;
                        model.PoID = s.PoID;
                        model.PoName = s.PoName;
                        model.UfStudyStatus = s.UfStudyStatus;
                        model.UfStudyStatusName = s.UfStudyStatusName;
                        model.RecID = s.RecID;
                        model.UserID = s.UserID;
                        list.Add(model);
                    }

                    data.UserID = id;
                    data.ListFamily = list;
                }
            }
            catch (Exception ex)
            {

            }

            return data;
        }

        public List<UserFamilyViewModel> GetUserFamilyByRec(int? recid, int? id)
        {
            List<UserFamilyViewModel> list = new List<UserFamilyViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    if (recid == 1)
                    {
                        var user = db.tb_User.Where(w => w.UserID == id).FirstOrDefault();
                        UserFamilyViewModel model = new UserFamilyViewModel();
                        model.UfName = user.FirstNameTh + " " + user.LastNameTh;
                        list.Add(model);
                    }
                    else
                    {
                        var family = db.tb_User_Family.Where(w => w.UserID == id && w.RecID == recid).ToList();
                        foreach (var s in family)
                        {
                            UserFamilyViewModel model = new UserFamilyViewModel();
                            model.UfName = s.UfName;
                            list.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public UserFamilyViewModel GetFamilyByID(int userid, int recid, int ufid)
        {
            UserFamilyViewModel data = new UserFamilyViewModel();
            data.UserID = userid;
            data.RecID = recid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_User_Family.Where(x => x.UfID == ufid).FirstOrDefault();
                    UserFamilyViewModel model = new UserFamilyViewModel();
                    model.UfID = obj.UfID;
                    model.UserID = obj.UserID;
                    model.UfName = obj.UfName;
                    model.UfCardID = obj.UfCardID;
                    if(obj.UfDOB != null)
                        model.UfDOBText = Convert.ToDateTime(obj.UfDOB).ToString("dd/MM/yyyy");
                    model.UfLifeStatus = obj.UfLifeStatus;
                    model.TdStatus = obj.TdStatus;
                    model.PoID = obj.PoID;
                    model.UfWeddingDate = obj.UfWeddingDate;
                    model.UfWeddingDateText = (obj.UfWeddingDate != null) ? Convert.ToDateTime(obj.UfWeddingDate).ToString("dd/MM/yyyy") : string.Empty;
                    model.DivorceDate = obj.DivorceDate;
                    model.DivorceDateText = (obj.DivorceDate != null) ? Convert.ToDateTime(obj.DivorceDate).ToString("dd/MM/yyyy") : string.Empty;
                    model.MaritalStatusID = obj.MaritalStatusID;
                    model.UfStudyStatus = obj.UfStudyStatus;
                    model.RecID = obj.RecID;
                    model.OcID = obj.OcID;

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
                    model.UserID = data.UserID;
                    model.UfName = data.UfName;
                    model.UfCardID = data.UfCardID;
                    if (Convert.ToDateTime(data.UfDOBText) > DateTime.MinValue)
                        model.UfDOB = Convert.ToDateTime(data.UfDOBText);
                    model.UfLifeStatus = data.UfLifeStatus;
                    model.TdStatus = data.TdStatus;
                    model.PoID = data.PoID;
                    if (Convert.ToDateTime(data.UfWeddingDateText) > DateTime.MinValue)
                        model.UfWeddingDate = Convert.ToDateTime(data.UfWeddingDateText);
                    if (Convert.ToDateTime(data.DivorceDateText) > DateTime.MinValue)
                        model.DivorceDate = Convert.ToDateTime(data.DivorceDateText);
                    model.MaritalStatusID = data.MaritalStatusID;
                    model.UfStudyStatus = data.UfStudyStatus;
                    model.OcID = data.OcID;
                    model.RecID = data.RecID;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Family.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
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
                    if (Convert.ToDateTime(newdata.UfDOB) > DateTime.MinValue)
                        model.UfDOB = Convert.ToDateTime(newdata.UfDOBText);
                    model.UfLifeStatus = newdata.UfLifeStatus;
                    model.TdStatus = newdata.TdStatus;
                    model.PoID = newdata.PoID;
                    if (Convert.ToDateTime(newdata.UfWeddingDateText) > DateTime.MinValue)
                        model.UfWeddingDate = Convert.ToDateTime(newdata.UfWeddingDateText);
                    if (Convert.ToDateTime(newdata.DivorceDateText) > DateTime.MinValue)
                        model.DivorceDate = Convert.ToDateTime(newdata.DivorceDateText);
                    model.MaritalStatusID = newdata.MaritalStatusID;
                    model.UfStudyStatus = newdata.UfStudyStatus;
                    model.OcID = newdata.OcID;
                    model.RecID = newdata.RecID;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteFamilyByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_User_Family.SingleOrDefault(x => x.UfID == id);
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

        #region 1.3 Tab: User-Education

        public UserEducationViewModel GetEducationByUser(int userid)
        {
            var data = new UserEducationViewModel();
            var list = new List<UserEducationViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var education = db.vw_User_Education.Where(x => x.UserID == userid).OrderByDescending(o => o.UeGraduationDate).ToList();
                    foreach (var s in education)
                    {
                        UserEducationViewModel model = new Models.UserEducationViewModel();
                        model.RowNumber = index++;
                        model.UeID = s.UeID;
                        model.UserID = s.UserID;
                        model.EduName = s.EduName;
                        model.DegName = s.DegName;
                        model.MajName = s.MajName;
                        model.CountryName = s.CountryName;
                        model.UeInstituteName = s.UeInstituteName;
                        model.UeGraduationDateText = (s.UeGraduationDate.HasValue) ? s.UeGraduationDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UeGPA = s.UeGPA;
                        model.UeEduOfficial = s.UeEduOfficial;
                        model.UeEduOfficialLevel = s.UeEduOfficialLevel;
                        list.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            data.UserID = userid;
            data.ListEducation = list;

            return data;
        }

        public UserEducationViewModel GetEducationByID(int userid, int ueid)
        {
            UserEducationViewModel data = new UserEducationViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var edu = db.tb_User_Education.Where(x => x.UeID == ueid).FirstOrDefault();

                    UserEducationViewModel model = new UserEducationViewModel();
                    model.UeID = edu.UeID;
                    model.UserID = edu.UserID;
                    model.EduID = edu.EduID;
                    model.DegID = edu.DegID;
                    model.MajID = edu.MajID;
                    model.UeInstituteName = edu.UeInstituteName;
                    model.CountryID = edu.CountryID;
                    model.UeGraduationDate = edu.UeGraduationDate;
                    model.UeGraduationDateText = Convert.ToDateTime(edu.UeGraduationDate).ToString("dd/MM/yyyy");
                    model.UeGPA = edu.UeGPA;
                    model.UeEduOfficial = edu.UeEduOfficial;
                    model.UeEduOfficialLevel = edu.UeEduOfficialLevel;

                    if (model != null)
                        data = model;
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
                    model.UserID = data.UserID;
                    model.EduID = data.EduID;
                    model.DegID = data.DegID;
                    model.MajID = data.MajID;
                    model.UeInstituteName = data.UeInstituteName;
                    model.CountryID = data.CountryID;
                    if (Convert.ToDateTime(data.UeGraduationDateText) > DateTime.MinValue)
                        model.UeGraduationDate = Convert.ToDateTime(data.UeGraduationDateText);
                    model.UeGPA = data.UeGPA;
                    model.UeEduOfficial = data.UeEduOfficial;
                    model.UeEduOfficialLevel = data.UeEduOfficialLevel;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Education.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
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
                    if (Convert.ToDateTime(newdata.UeGraduationDateText) > DateTime.MinValue)
                        model.UeGraduationDate = Convert.ToDateTime(newdata.UeGraduationDateText);
                    model.UeGPA = newdata.UeGPA;
                    model.UeEduOfficial = newdata.UeEduOfficial;
                    model.UeEduOfficialLevel = newdata.UeEduOfficialLevel;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteEducationByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Education.SingleOrDefault(x => x.UeID == id);
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

        #region 1.4 Tab: User-Position

        public UserPositionViewModel GetPositionByUser(int userid)
        {
            var data = new UserPositionViewModel();
            var list = new List<UserPositionViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var position = db.vw_User_Position.Where(x => x.UserID == userid).OrderByDescending(o => o.UpForceDate).ToList();
                    foreach (var item in position)
                    {
                        UserPositionViewModel model = new UserPositionViewModel();
                        model.RowNumber = index++;
                        model.UpID = item.UpID;
                        model.UserID = item.UserID;
                        model.ActName = item.ActName;
                        model.UpCmd = item.UpCmd;
                        model.PoName = item.PoName;
                        model.PoAName = item.PoAName;
                        model.UpLevel = item.UpLevel;
                        model.UpSalary = item.UpSalary;
                        model.UpCmdDateText = (item.UpCmdDate.HasValue) ? item.UpCmdDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UpForceDateText = (item.UpForceDate.HasValue) ? item.UpForceDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UpRemark = item.UpRemark;
                        model.UpPathFile = item.UpPathFile;
                        model.FullPosition = "";
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListPosition = list;
            return data;
        }

        public UserPositionViewModel GetPositionByID(int userid, int id)
        {
            UserPositionViewModel data = new UserPositionViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_User_Position.Where(x => x.UpID == id).FirstOrDefault();
                    UserPositionViewModel model = new UserPositionViewModel();
                    model.UpID = obj.UpID;
                    model.UserID = obj.UserID;
                    model.ActID = obj.ActID;
                    model.UpCmd = obj.UpCmd;
                    model.PoTID = obj.PoTID;
                    model.DivID = obj.DivID;
                    model.DepID = obj.DepID;
                    model.SecID = obj.SecID;
                    model.PoID = obj.PoID;
                    model.PoAID = obj.PoAID;
                    model.UpLevel = obj.UpLevel;
                    model.UpSalary = obj.UpSalary;
                    model.UpCmdDate = obj.UpCmdDate;
                    model.UpCmdDateText = Convert.ToDateTime(obj.UpCmdDate).ToString("dd/MM/yyyy");
                    model.UpForceDate = obj.UpForceDate;
                    model.UpForceDateText = Convert.ToDateTime(obj.UpForceDate).ToString("dd/MM/yyyy");
                    model.UpRemark = obj.UpRemark;
                    model.UpPathFile = obj.UpPathFile;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddPositionByEntity(UserPositionViewModel data, HttpPostedFileBase fileUpload)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadUserPosition;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFileName = data.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFileName);

                        fileUpload.SaveAs(fileLocation);

                        data.UpPathFile = newFileName;
                    }

                    tb_User_Position model = new tb_User_Position();
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
                    if (Convert.ToDateTime(data.UpCmdDateText) > DateTime.MinValue)
                        model.UpCmdDate = Convert.ToDateTime(data.UpCmdDateText);
                    if (Convert.ToDateTime(data.UpForceDateText) > DateTime.MinValue)
                        model.UpForceDate = Convert.ToDateTime(data.UpForceDateText);
                    model.UpRemark = data.UpRemark;
                    model.UpPathFile = data.UpPathFile;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Position.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData UpdatePositionByEntity(UserPositionViewModel newdata, HttpPostedFileBase fileUpload)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadUserPosition;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFileName = newdata.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFileName);

                        fileUpload.SaveAs(fileLocation);

                        newdata.UpPathFile = newFileName;
                    }

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
                    if (Convert.ToDateTime(newdata.UpCmdDateText) > DateTime.MinValue)
                        model.UpCmdDate = Convert.ToDateTime(newdata.UpCmdDateText);
                    if (Convert.ToDateTime(newdata.UpForceDateText) > DateTime.MinValue)
                        model.UpForceDate = Convert.ToDateTime(newdata.UpForceDateText);
                    model.UpRemark = newdata.UpRemark;
                    model.UpPathFile = newdata.UpPathFile;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeletePositionByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Position.SingleOrDefault(x => x.UpID == id);
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

        public FileViewModel DownloadPosition(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_User_Position.Where(x => x.UpID == id).FirstOrDefault();
                    string filename = data.UpPathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadUserPosition;

                    model.FileName = filename;
                    model.FilePath = filepath;
                    model.ContentType = Contenttype;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }


        #endregion

        #region 1.5 Tab: User-Trainning

        public UserTrainningViewModel GetTrainningByUser(int userid)
        {
            var data = new UserTrainningViewModel();
            var list = new List<UserTrainningViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var training = db.vw_User_Training.Where(x => x.UserID == userid).OrderByDescending(o => o.UtStartDate).ToList();
                    foreach (var item in training)
                    {
                        UserTrainningViewModel model = new UserTrainningViewModel();
                        model.RowNumber = index++;
                        model.UtID = item.UtID;
                        model.UserID = item.UserID;
                        model.TtID = item.TtID;
                        model.TtName = item.TtName;
                        model.CountryID = item.CountryID;
                        model.CountryName = item.CountryName;
                        model.UtCourse = item.UtCourse;
                        model.UtStartDateText = (item.UtStartDate.HasValue) ? item.UtStartDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UtEndDateText = (item.UtEndDate.HasValue) ? item.UtEndDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListTrainning = list;
            return data;
        }

        public UserTrainningViewModel GetTrainningByID(int userid, int id)
        {
            UserTrainningViewModel data = new UserTrainningViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_User_Training.Where(x => x.UtID == id).FirstOrDefault();
                    UserTrainningViewModel model = new UserTrainningViewModel();
                    model.UtID = obj.UtID;
                    model.UserID = obj.UserID;
                    model.TtID = obj.TtID;
                    model.CountryID = obj.CountryID;
                    model.UtCourse = obj.UtCourse;
                    model.UtStartDate = obj.UtStartDate;
                    model.UtStartDateText = Convert.ToDateTime(obj.UtStartDate).ToString("dd/MM/yyyy");
                    model.UtEndDate = obj.UtEndDate;
                    model.UtEndDateText = Convert.ToDateTime(obj.UtEndDate).ToString("dd/MM/yyyy");

                    if (model != null)
                        data = model;
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
                    if (Convert.ToDateTime(data.UtStartDateText) > DateTime.MinValue)
                        model.UtStartDate = Convert.ToDateTime(data.UtStartDateText);
                    if (Convert.ToDateTime(data.UtEndDateText) > DateTime.MinValue)
                        model.UtEndDate = Convert.ToDateTime(data.UtEndDateText);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Training.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
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
                    var model = db.tb_User_Training.Where(x => x.UserID == newdata.UserID && x.TtID == newdata.TtID).FirstOrDefault();
                    model.TtID = newdata.TtID;
                    model.CountryID = newdata.CountryID;
                    model.UtCourse = newdata.UtCourse;
                    if (Convert.ToDateTime(newdata.UtStartDateText) > DateTime.MinValue)
                        model.UtStartDate = Convert.ToDateTime(newdata.UtStartDateText);
                    if (Convert.ToDateTime(newdata.UtEndDateText) > DateTime.MinValue)
                        model.UtEndDate = Convert.ToDateTime(newdata.UtEndDateText);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteTrainingByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Training.SingleOrDefault(x => x.UtID == id);
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

        #region 1.6 Tab: User-Insignia

        public UserInsigniaViewModel GetInsigniaByUser(int userid)
        {
            var data = new UserInsigniaViewModel();
            var list = new List<UserInsigniaViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var insignia = db.vw_User_Insignia.Where(x => x.UserID == userid).OrderByDescending(o => o.UiRecDate).ToList();

                    foreach (var item in insignia)
                    {
                        UserInsigniaViewModel model = new UserInsigniaViewModel();
                        model.RowNumber = index++;
                        model.UiID = item.UiID;
                        model.UserID = item.UserID;
                        model.InsFullName = item.InsFullName;
                        model.UiYear = item.UiYear;
                        model.UiBook = item.UiBook;
                        model.UiPart = item.UiPart;
                        model.UiPage = item.UiPage;
                        model.UiCmd = item.UiCmd;
                        model.UiRecDateText = (item.UiRecDate.HasValue) ? item.UiRecDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.UiRetDateText = (item.UiRetDate.HasValue) ? item.UiRetDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListInsignia = list;
            return data;
        }

        public UserInsigniaViewModel GetInsigniaByID(int userid, int id)
        {
            UserInsigniaViewModel data = new UserInsigniaViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_User_Insignia.Where(x => x.UiID == id).FirstOrDefault();
                    UserInsigniaViewModel model = new UserInsigniaViewModel();
                    model.UiID = obj.UiID;
                    model.UserID = obj.UserID;
                    model.InsID = obj.InsID;
                    model.UiYear = obj.UiYear;
                    model.UiBook = obj.UiBook;
                    model.UiPart = obj.UiPart;
                    model.UiPage = obj.UiPage;
                    model.UiRecDate = obj.UiRecDate;
                    model.UiRecDateText = obj.UiRecDate.HasValue ? Convert.ToDateTime(obj.UiRecDate).ToString("dd/MM/yyyy") : string.Empty;
                    model.UiRetDate = obj.UiRetDate;
                    model.UiRetDateText = obj.UiRetDate.HasValue ? Convert.ToDateTime(obj.UiRetDate).ToString("dd/MM/yyyy") : string.Empty;
                    model.UiCmd = obj.UiCmd;
                    model.UiPartFile = obj.UiPartFile;

                    if (model != null)
                        data = model;
                }
            }
            catch (Exception)
            {

            }
            return data;
        }

        public ResponseData AddInsigniaByEntity(UserInsigniaViewModel data, HttpPostedFileBase fileUpload)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadUserInsignia;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFileName = data.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFileName);

                        fileUpload.SaveAs(fileLocation);

                        data.UiPartFile = newFileName;
                    }

                    tb_User_Insignia model = new tb_User_Insignia();
                    model.UiID = data.UiID;
                    model.UserID = data.UserID;
                    model.InsID = data.InsID;
                    model.UiYear = data.UiYear;
                    model.UiBook = data.UiBook;
                    model.UiPart = data.UiPart;
                    model.UiPage = data.UiPage;
                    if (Convert.ToDateTime(data.UiRecDateText) > DateTime.MinValue)
                        model.UiRecDate = Convert.ToDateTime(data.UiRecDateText);
                    if (Convert.ToDateTime(data.UiRetDateText) > DateTime.MinValue)
                        model.UiRetDate = Convert.ToDateTime(data.UiRetDateText);
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

        public ResponseData UpdateInsigniaByEntity(UserInsigniaViewModel newdata, HttpPostedFileBase fileUpload)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadUserInsignia;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFileName = newdata.UserID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFileName);

                        fileUpload.SaveAs(fileLocation);

                        newdata.UiPartFile = newFileName;
                    }

                    var model = db.tb_User_Insignia.Single(x => x.UserID == newdata.UserID && x.UiID == newdata.UiID);
                    model.InsID = newdata.InsID;
                    model.UiYear = newdata.UiYear;
                    model.UiBook = newdata.UiBook;
                    model.UiPart = newdata.UiPart;
                    model.UiPage = newdata.UiPage;
                    if (Convert.ToDateTime(newdata.UiRecDateText) > DateTime.MinValue)
                        model.UiRecDate = Convert.ToDateTime(newdata.UiRecDateText);
                    if (Convert.ToDateTime(newdata.UiRetDateText) > DateTime.MinValue)
                        model.UiRetDate = Convert.ToDateTime(newdata.UiRetDateText);
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

        public ResponseData DeleteInsigniaByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Insignia.SingleOrDefault(x => x.UiID == id);
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

        public FileViewModel DownloadInsignia(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_User_Insignia.Where(x => x.UiID == id).FirstOrDefault();
                    string filename = data.UiPartFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadUserInsignia;

                    model.FileName = filename;
                    model.FilePath = filepath;
                    model.ContentType = Contenttype;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }


        #endregion

        #region 1.7 Tab: User-Excellent

        public UserExcellentViewModel GetExcellentByUser(int userid)
        {
            var data = new UserExcellentViewModel();
            var list = new List<UserExcellentViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var excellent = db.vw_User_Excellent.Where(x => x.UserID == userid).OrderByDescending(o => o.UeRecDate).ToList();

                    foreach (var item in excellent)
                    {
                        UserExcellentViewModel model = new UserExcellentViewModel();
                        model.RowNumber = index++;
                        model.UeID = item.UeID;
                        model.UserID = item.UserID;
                        model.UeRecYear = item.UeRecDate.HasValue ? Convert.ToDateTime(item.UeRecDate).Year.ToString() : string.Empty;
                        model.ExTName = item.ExTName;
                        model.UeProjectName = item.UeProjectName;
                        model.UeRecDateText = (item.UeRecDate.HasValue) ? item.UeRecDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.PoName = "";
                        model.FullPosition = "";
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListExcellent = list;
            return data;
        }

        public UserExcellentViewModel GetExcellentByID(int userid, int id)
        {
            UserExcellentViewModel data = new UserExcellentViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_User_Excellent.Where(x => x.UeID == id).FirstOrDefault();
                    UserExcellentViewModel model = new UserExcellentViewModel();
                    model.UeID = obj.UeID;
                    model.UserID = obj.UserID;
                    model.ExTID = obj.ExTID;
                    model.UeProjectName = obj.UeProjectName;
                    model.UeRecDate = obj.UeRecDate;
                    model.UeRecYear = obj.UeRecDate.HasValue ? Convert.ToDateTime(obj.UeRecDate).Year.ToString() : string.Empty;
                    model.UeRecDateText = Convert.ToDateTime(obj.UeRecDate).ToString("dd/MM/yyyy");

                    if (model != null)
                        data = model;
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
                    model.ExTID = data.ExTID;
                    model.UeProjectName = data.UeProjectName;
                    if (Convert.ToDateTime(data.UeRecDateText) > DateTime.MinValue)
                        model.UeRecDate = Convert.ToDateTime(data.UeRecDateText);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Excellent.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
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
                    var model = db.tb_User_Excellent.Single(x => x.UserID == newdata.UserID && x.UeID == newdata.UeID);
                    model.UeID = newdata.UeID;
                    model.ExTID = newdata.ExTID;
                    model.UeProjectName = newdata.UeProjectName;
                    if (Convert.ToDateTime(newdata.UeRecDateText) > DateTime.MinValue)
                        model.UeRecDate = Convert.ToDateTime(newdata.UeRecDateText);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteExcellentByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Excellent.SingleOrDefault(x => x.UeID == id);
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

        #region 1.8 Tab: User-Certificate

        public UserCertificateViewModel GetCertificateByUser(int userid)
        {
            var data = new UserCertificateViewModel();
            var list = new List<UserCertificateViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var certificate = db.vw_User_Certificate.Where(x => x.UserID == userid).OrderByDescending(o => o.UcRecDate).ToList();

                    foreach (var item in certificate)
                    {
                        UserCertificateViewModel model = new UserCertificateViewModel();
                        model.RowNumber = index++;
                        model.UcID = item.UcID;
                        model.UserID = item.UserID;
                        model.CerName = item.CerName;
                        model.UcRecDateText = (item.UcRecDate.HasValue) ? item.UcRecDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListCertificate = list;
            return data;
        }

        public UserCertificateViewModel GetCertificateByID(int userid, int id)
        {
            UserCertificateViewModel data = new UserCertificateViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.tb_User_Certificate.Where(x => x.UcID == id).FirstOrDefault();
                    UserCertificateViewModel model = new UserCertificateViewModel();
                    model.UcID = obj.UcID;
                    model.UserID = obj.UserID;
                    model.CerId = obj.CerId;
                    model.UcRecDate = obj.UcRecDate;
                    model.UcRecDateText = Convert.ToDateTime(obj.UcRecDate).ToString("dd/MM/yyyy");

                    if (model != null)
                        data = model;
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
                    if (Convert.ToDateTime(data.UcRecDateText) > DateTime.MinValue)
                        model.UcRecDate = Convert.ToDateTime(data.UcRecDateText);
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_Certificate.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
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
                    var model = db.tb_User_Certificate.Single(x => x.UserID == newdata.UserID && x.UcID == newdata.UcID);
                    model.CerId = newdata.CerId;
                    if (Convert.ToDateTime(newdata.UcRecDateText) > DateTime.MinValue)
                        model.UcRecDate = Convert.ToDateTime(newdata.UcRecDateText);
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

        public ResponseData DeleteCertificateByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_Certificate.SingleOrDefault(x => x.UcID == id);
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

        #region 1.9 Tab: User-History

        public UserHistoryViewModel GetHistoryByUser(int userid)
        {
            var data = new UserHistoryViewModel();
            var list = new List<UserHistoryViewModel>();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var history = db.vw_User_History.Where(x => x.UserID == userid).OrderByDescending(o => o.UhEditDate).ToList();

                    foreach (var item in history)
                    {
                        UserHistoryViewModel model = new UserHistoryViewModel();
                        model.RowNumber = index++;
                        model.UhID = item.UhID;
                        model.UserID = item.UserID;
                        model.UhEditDateText = (item.UhEditDate.HasValue) ? item.UhEditDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.OldFirstNameTh = item.OldFirstNameTh;
                        model.OldLastNameTh = item.OldLastNameTh;
                        model.OldFirstNameEn = item.OldFirstNameEn;
                        model.OldLastNameEn = item.NewLastNameEn;
                        model.NewFirstNameTh = item.NewFirstNameTh;
                        model.NewLastNameTh = item.NewLastNameTh;
                        model.NewFirstNameEn = item.NewFirstNameEn;
                        model.NewLastNameEn = item.NewLastNameEn;
                        model.Remark = item.Remark;
                        model.UhStatus = item.UhStatus;
                        model.OldFullNameTh = item.OldFirstNameTh + " " + item.OldLastNameTh;
                        model.OldFullNameEn = item.OldFirstNameEn + " " + item.OldLastNameEn;
                        model.NewFullNameTh = item.NewFirstNameTh + " " + item.NewLastNameTh;
                        model.NewFullNameEn = item.NewFirstNameEn + " " + item.NewLastNameEn;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            data.UserID = userid;
            data.ListHistory = list;
            return data;
        }

        public UserHistoryViewModel GetHistoryByID(int userid, int id)
        {
            UserHistoryViewModel data = new UserHistoryViewModel();
            data.UserID = userid;

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var obj = db.vw_User_History.Where(x => x.UhID == id).FirstOrDefault();

                    UserHistoryViewModel model = new UserHistoryViewModel();
                    model.UhID = obj.UhID;
                    model.UserID = obj.UserID;
                    model.UhEditDate = obj.UhEditDate;
                    model.UhEditDateText = Convert.ToDateTime(obj.UhEditDate).ToString("dd/MM/yyyy");
                    model.OldTiID = obj.OldTiID;
                    model.OldFirstNameTh = obj.OldFirstNameTh;
                    model.OldLastNameTh = obj.OldLastNameTh;
                    model.OldFirstNameEn = obj.OldFirstNameEn;
                    model.OldLastNameEn = obj.NewLastNameEn;
                    model.NewTiID = obj.NewTiID;
                    model.NewFirstNameTh = obj.NewFirstNameTh;
                    model.NewLastNameTh = obj.NewLastNameTh;
                    model.NewFirstNameEn = obj.NewFirstNameEn;
                    model.NewLastNameEn = obj.NewLastNameEn;
                    model.Remark = obj.Remark;
                    model.UhStatus = obj.UhStatus;

                    if (model != null)
                        data = model;
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
                    var obj = db.tb_User.Single(x => x.UserID == data.UserID);
                    int? OldTi = obj.TitleID;
                    string OldFTh = obj.FirstNameTh;
                    string OldLTh = obj.LastNameTh;
                    string OldFEn = obj.FirstNameEn;
                    string OldLEn = obj.LastNameEn;

                    tb_User_History model = new tb_User_History();
                    model.UserID = data.UserID;
                    model.OldTiID = OldTi;
                    model.OldFirstNameTh = OldFTh;
                    model.OldLastNameTh = OldLTh;
                    model.OldFirstNameEn = OldFEn;
                    model.OldLastNameEn = OldLEn;
                    model.NewTiID = data.NewTiID;
                    model.NewFirstNameTh = data.NewFirstNameTh;
                    model.NewLastNameTh = data.NewLastNameTh;
                    model.NewFirstNameEn = data.NewFirstNameEn;
                    model.NewLastNameEn = data.NewLastNameEn;
                    if (Convert.ToDateTime(data.UhEditDateText) > DateTime.MinValue)
                        model.UhEditDate = Convert.ToDateTime(data.UhEditDateText);
                    model.Remark = data.Remark;
                    model.UhStatus = data.UhStatus;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_User_History.Add(model);
                    db.SaveChanges();

                    if (data.UhStatus == true)
                    {
                        obj.TitleID = model.NewTiID;
                        obj.FirstNameTh = model.NewFirstNameTh;
                        obj.LastNameTh = model.NewLastNameTh;
                        obj.FirstNameEn = model.NewFirstNameEn;
                        obj.LastNameEn = model.NewLastNameEn;
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

        public ResponseData UpdateHistoryByEntity(UserHistoryViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_User_History.Single(x => x.UserID == newdata.UserID && x.UhID == newdata.UhID);
                    if (Convert.ToDateTime(newdata.UhEditDateText) > DateTime.MinValue)
                        model.UhEditDate = Convert.ToDateTime(newdata.UhEditDateText);
                    model.NewTiID = newdata.NewTiID;
                    model.NewFirstNameTh = newdata.NewFirstNameTh;
                    model.NewLastNameTh = newdata.NewLastNameTh;
                    model.NewFirstNameEn = newdata.NewFirstNameEn;
                    model.NewLastNameEn = newdata.NewLastNameEn;
                    model.Remark = newdata.Remark;
                    model.UhStatus = newdata.UhStatus;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();

                    if (model.UhStatus == true)
                    {
                        var obj = db.tb_User.Single(x => x.UserID == model.UserID);
                        obj.TitleID = model.NewTiID;
                        obj.FirstNameTh = model.NewFirstNameTh;
                        obj.LastNameTh = model.NewLastNameTh;
                        obj.FirstNameEn = model.NewFirstNameEn;
                        obj.LastNameEn = model.NewLastNameEn;
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

        public ResponseData DeleteHistoryByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var model = db.tb_User_History.SingleOrDefault(x => x.UhID == id);
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

    }
}