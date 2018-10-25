using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class MasterRepository
    {
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

        public List<UserTypeViewModel> GetUserType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_User_Type.Select(s => new UserTypeViewModel()
                {
                    UserTypeID = s.TypeID,
                    UserTypeName = s.TypeName
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

        public List<ExcellentViewModel> GetExcellentType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Excellent_Type.Select(s => new ExcellentViewModel()
                {
                    ExTID = s.ExTID,
                    ExTName = s.ExTName
                }).ToList();
                return list;
            }
        }

        public List<MoveTypeViewModel> GetMoveType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Move_Type.Select(s => new MoveTypeViewModel()
                {
                    MoveTypeID = s.MtID,
                    MoveTypeName = s.MtName
                }).ToList();
                return list;
            }
        }

        #endregion

        public ManPowerViewModel GetDetailByUser(int userid)
        {
            using (SATEntities db = new SATEntities())
            {
                var user = new EmployeeRepository().GetByID(userid);

                ManPowerViewModel model = new ManPowerViewModel();
                model.BelongTo = user.DivName + " / " + user.DepName + " / " + user.SecName;
                model.PositionID = user.PoID;
                model.Position = "(" + user.PoCode + ") " + user.PoName;
                model.Level = user.SalaryLevel.ToString();
                model.Step = user.SalaryStep.ToString();
                model.Salary = user.Salary.ToString();

                return model;
            }
        }

        public ManPowerViewModel GetDetailByPosition(int poid)
        {
            using (SATEntities db = new SATEntities())
            {
                ManPowerViewModel model = new ManPowerViewModel();
                var data = db.vw_Man_Power.Where(m => m.PoID == poid).FirstOrDefault();
                if (data != null)
                {
                    model.BelongTo = data.DivName + " / " + data.DepName + " / " + data.SecName;
                    model.FullName = data.FullNameTh;
                }
                return model;
            }
        }

    }
}