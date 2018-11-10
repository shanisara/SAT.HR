using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT.HR.Data.Repository;

namespace SAT.HR.Data.Repository
{
    public class DropDownList
    {
        public static List<SelectListItem> GetDivision(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DivisionRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.DivStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DivID.ToString();
                select.Text = item.DivName;
                select.Selected = defaultValue.HasValue ? (item.DivID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDepartment(int? divid, int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DepartmentRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.DepStatus == true).ToList();

            if (divid.HasValue)
                data = data.Where(m => m.DivID == divid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DepID.ToString();
                select.Text = item.DepName;
                select.Selected = defaultValue.HasValue ? (item.DepID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSection(int? divid, int? depid, int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SectionRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.SecStatus == true).ToList();

            if (divid.HasValue)
                data = data.Where(m => m.DivID == divid).ToList();

            if (depid.HasValue)
                data = data.Where(m => m.DepID == depid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SecID.ToString();
                select.Text = item.SecName;
                select.Selected = defaultValue.HasValue ? (item.SecID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDiscipline(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DisciplineRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DisID.ToString();
                select.Text = item.DisName;
                select.Selected = defaultValue.HasValue ? (item.DisID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPosition(int? defaultValue, int? typeid, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRepository().GetByType(typeid);
            if (isActive == true)
                data = data.Where(m => m.PoStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PoID.ToString();
                select.Text = " (" + item.PoCode.ToString().PadLeft(3, '0') + ") "+ item.PoName;
                select.Selected = defaultValue.HasValue ? (item.PoID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetLevel(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SalaryRepository().GetSalaryLevel();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.Level.ToString();
                select.Text = item.Level.ToString();
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetCertificate(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new CertificateRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CerID.ToString();
                select.Text = item.CerName;
                select.Selected = defaultValue.HasValue ? (item.CerID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetInsignia(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new InsigniaRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.InsStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.InsID.ToString();
                select.Text = item.InsFullName;
                select.Selected = defaultValue.HasValue ? (item.InsID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetTitle(int? sexid, int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new TitleRepository().GetAll();

            if (sexid.HasValue)
                data = data.Where(m => m.SexID == sexid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.TiID.ToString();
                select.Text = item.TiFullName;
                select.Selected = defaultValue.HasValue ? (item.TiID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetEducation(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EducationRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.EduStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.EduID.ToString();
                select.Text = item.EduName;
                select.Selected = defaultValue.HasValue ? (item.EduID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDegree(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DegreeRepository().GetAll();
            if (isActive == true)
                data.Where(m => m.DegStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DegID.ToString();
                select.Text = item.DegName;
                select.Selected = defaultValue.HasValue ? (item.DegID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetMajor(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MajorRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.MajStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MajID.ToString();
                select.Text = item.MajName;
                select.Selected = defaultValue.HasValue ? (item.MajID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetNationality(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new NationalityRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.NatStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.NatID.ToString();
                select.Text = item.NatName;
                select.Selected = defaultValue.HasValue ? (item.NatID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetReligion(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new ReligionRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.RelStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.RelD.ToString();
                select.Text = item.RelName;
                select.Selected = defaultValue.HasValue ? (item.RelD == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetLeaveType(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new LeaveTypeRepository().GetAll();

            DateTime curDate = DateTime.Now;
            data = data.Where(m => m.LevStartDate >= curDate && m.LevEndDate >= curDate).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.LevID.ToString();
                select.Text = item.LevName;
                select.Selected = defaultValue.HasValue ? (item.LevID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetActionType(int? defaultValue, int? poid)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new ActionTypeRepository().GetAll();

            if (poid.HasValue)
                data = data.Where(m => m.ActID == poid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.ActID.ToString();
                select.Text = item.ActName;
                select.Selected = defaultValue.HasValue ? (item.ActID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSex(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SexRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SexID.ToString();
                select.Text = item.SexName;
                select.Selected = defaultValue.HasValue ? (item.SexID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetCapabilityType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new CapabilityRepository().GetCapabilityType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CapTID.ToString();
                select.Text = item.CapTName;
                select.Selected = defaultValue.HasValue ? (item.CapTID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetCapabilityGroup(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new CapabilityRepository().GetCapabilityGroup();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CapGID.ToString();
                select.Text = item.CapGName;
                select.Selected = defaultValue.HasValue ? (item.CapGID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetCapabilityGroupType(int? capgid, int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new CapabilityRepository().GetCapabilityGroup();

            var group = data.Where(w => w.CapGID == capgid).FirstOrDefault();

            string table = string.Empty;
            if (group != null)
                table = group.TableName;

            if (table == "tb_Division")
                list = GetDivision(defaultValue, false);
            else if (table == "tb_Department")
                list = GetDepartmentFull(null, defaultValue, false);
            else if (table == "tb_Section")
                list = GetSectionFull(null, null, defaultValue, false);
            else if (table == "tb_Position")
                list = GetPosition(defaultValue, null, false);
            else if (table == "tb_Level")
                list = GetLevel(defaultValue);
            else if (table == "tb_Discipline")
                list = GetDiscipline(defaultValue, false);

            return list;
        }

        public static List<SelectListItem> GetDepartmentFull(int? divid, int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DepartmentRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.DepStatus == true).ToList();

            if (divid.HasValue)
                data = data.Where(m => m.DivID == divid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DepID.ToString();
                select.Text = item.DivName + "/" + item.DepName;
                select.Selected = defaultValue.HasValue ? (item.DepID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSectionFull(int? divid, int? depid, int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SectionRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.SecStatus == true).ToList();

            if (divid.HasValue)
                data = data.Where(m => m.DivID == divid).ToList();

            if (depid.HasValue)
                data = data.Where(m => m.DepID == depid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SecID.ToString();
                select.Text = item.DivName + "/" + item.DepName + "/" +item.SecName;
                select.Selected = defaultValue.HasValue ? (item.SecID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetRecieveType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetRecieveType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.RecID.ToString();
                select.Text = item.RecName;
                select.Selected = defaultValue.HasValue ? (item.RecID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetUserStatus(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserStatusID.ToString();
                select.Text = item.UserStatusName;
                select.Selected = defaultValue.HasValue ? (item.UserStatusID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetUserType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetUserType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserTypeID.ToString();
                select.Text = item.UserTypeName;
                select.Selected = defaultValue.HasValue ? (item.UserTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetWorkingType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetWorkingType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.WorkingTypeID.ToString();
                select.Text = item.WorkingTypeName;
                select.Selected = defaultValue.HasValue ? (item.WorkingTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetBloodType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetBloodType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.BloodTypeID.ToString();
                select.Text = item.BloodTypeName;
                select.Selected = defaultValue.HasValue ? (item.BloodTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetMaritalStatus(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetMaritalStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MaritalStatusID.ToString();
                select.Text = item.MaritalStatusName;
                select.Selected = defaultValue.HasValue ? (item.MaritalStatusID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSalaryLevel(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SalaryRepository().GetSalaryLevel();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.Level.ToString();
                select.Text = item.Level.ToString();
                select.Selected = defaultValue.HasValue ? (item.Level == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSalaryStep(decimal? defaultValue, int? level)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SalaryRepository().GetSalaryStep(level);

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.Step.ToString("F2");
                select.Text = item.Step.ToString();
                select.Selected = defaultValue.HasValue ? (item.Step == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetProvince(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new AddressRepository().GetProvince();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.ProvinceID.ToString();
                select.Text = item.ProvinceName;
                select.Selected = defaultValue.HasValue ? (item.ProvinceID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDistrict(int? defaultValue, int? provinceID)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new AddressRepository().GetDistrict();

            if (provinceID.HasValue)
                data = data.Where(m => m.ProvinceID == provinceID).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DistrictID.ToString();
                select.Text = item.DistrictName;
                select.Selected = defaultValue.HasValue ? (item.DistrictID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSubDistrict(int? defaultValue, int? provinceID, int? districtid)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new AddressRepository().GetSubDistrict();

            if (provinceID.HasValue)
                data = data.Where(m => m.ProvinceID == provinceID).ToList();

            if (districtid.HasValue)
                data = data.Where(m => m.DistrictID == districtid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SubDistrictID.ToString();
                select.Text = item.SubDistrictName;
                select.Selected = defaultValue.HasValue ? (item.SubDistrictID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetCountry(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new AddressRepository().GetCountry();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CountryID.ToString();
                select.Text = item.CountryName;
                select.Selected = defaultValue.HasValue ? (item.CountryID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPositionType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRepository().GetPositionType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PoTID.ToString();
                select.Text = item.PoTName;
                select.Selected = defaultValue.HasValue ? (item.PoTID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetEmpower(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetEmpower();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.EmpowerID.ToString();
                select.Text = item.EmpowerName;
                select.Selected = defaultValue.HasValue ? (item.EmpowerID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetOccupation(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetOccupation();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.OcID.ToString();
                select.Text = item.OcName;
                select.Selected = defaultValue.HasValue ? (item.OcID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetTrainingType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new TrainingRepository().GetTrainingType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.TrainingTypeID.ToString();
                select.Text = item.TrainingTypeName;
                select.Selected = defaultValue.HasValue ? (item.TrainingTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetExcellentType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetExcellentType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.ExTID.ToString();
                select.Text = item.ExTName;
                select.Selected = defaultValue.HasValue ? (item.ExTID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetMoveType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetMoveType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MoveTypeID.ToString();
                select.Text = item.MoveTypeName;
                select.Selected = defaultValue.HasValue ? (item.MoveTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }
        
        public static List<SelectListItem> PositionAgent(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRepository().GetPositionAgent();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PoAID.ToString();
                select.Text = item.PoAName;
                select.Selected = defaultValue.HasValue ? (item.PoAID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetEmployee(int? defaultValue, int? userType)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetEmployee(userType);

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserID.ToString();
                select.Text = item.UserName;
                select.Selected = defaultValue.HasValue ? (item.UserID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDivisionManPower(int? type, int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRateRepository().GetDivisionManPower(type);
            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DivID.ToString();
                select.Text = item.DivName;
                select.Selected = defaultValue.HasValue ? (item.DivID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDepartmentManPower(int? type, int? divid, int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRateRepository().GetDepartmentManPower(type, divid);
            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DepID.ToString();
                select.Text = item.DepName;
                select.Selected = defaultValue.HasValue ? (item.DepID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSectionManPower(int? type, int? divid, int? depid, int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRateRepository().GetSectionManPower(type, divid, depid);
            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SecID.ToString();
                select.Text = item.SecName;
                select.Selected = defaultValue.HasValue ? (item.SecID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPositionManPowerValuePo(int? type, int? divid, int? depid, int? secid, int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRateRepository().GetPositionManPower(type, divid, depid, secid);
            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PoID.ToString();
                select.Text = " (" + (type.ToString() == "1" ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0')) + ") " + item.PoName;
                select.Selected = defaultValue.HasValue ? (item.PoID.Equals(defaultValue) ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPositionManPowerValueMp(int? type, int? divid, int? depid, int? secid, int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRateRepository().GetPositionManPower(type, divid, depid, secid);
            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MpID.ToString();
                select.Text = " (" + (type.ToString() == "1" ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0')) + ") " + item.PoName;
                select.Selected = defaultValue.HasValue ? (item.MpID.Equals(defaultValue) ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPositionRate(int? defaultValue, int? type)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRateRepository().GetPositionRate(type);
            
            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MpID.ToString();
                select.Text = " (" + (type.ToString() == "1" ? item.MpID.ToString().PadLeft(3, '0') : item.MpID.ToString().PadLeft(4, '0')) + ") " + item.PoName;
                select.Selected = defaultValue.HasValue ? (item.MpID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetMemberType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetMemberType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MemberTypeID.ToString();
                select.Text = item.MemberTypeName;
                select.Selected = defaultValue.HasValue ? (item.MemberTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetRentType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetRentType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.RentTypeID.ToString();
                select.Text = item.RentTypeName;
                select.Selected = defaultValue.HasValue ? (item.RentTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPartType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetPartType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PartTypeID.ToString();
                select.Text = item.PartTypeName;
                select.Selected = defaultValue.HasValue ? (item.PartTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetBankLoan(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetBankLoan();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.BankTypeID.ToString();
                select.Text = item.BankTypeName;
                select.Selected = defaultValue.HasValue ? (item.BankTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetLoanType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetLoanType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.LoanTypeID.ToString();
                select.Text = item.LoanTypeName;
                select.Selected = defaultValue.HasValue ? (item.LoanTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetClaimType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetClaimType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.ClaimTypeID.ToString();
                select.Text = item.ClaimTypeName;
                select.Selected = defaultValue.HasValue ? (item.ClaimTypeID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetAccumulativeFund(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MasterRepository().GetAccumulativeFund();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CuID.ToString();
                select.Text = item.CuAmoutPer;
                select.Selected = defaultValue.HasValue ? (item.CuID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetUserSpouse(int userid, string defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetFamilyByUser(userid,4);

            foreach (var item in data.ListFamily)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UfName.ToString();
                select.Text = item.UfName;
                select.Selected = !string.IsNullOrEmpty(defaultValue) ? (item.UfName == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetUserChild(int userid, string defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetFamilyByUser(userid,5);

            foreach (var item in data.ListFamily)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UfName.ToString();
                select.Text = item.UfName;
                select.Selected = !string.IsNullOrEmpty(defaultValue) ? (item.UfName == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetBenefitType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var data = new MasterRepository().GetBenefitType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.BenTID.ToString();
                select.Text = item.BenTName;
                select.Selected = defaultValue.HasValue ? (item.BenTID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetUserFamilyByRec(int userid, int? recid, string defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserFamilyByRec(recid, userid);

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UfName.ToString();
                select.Text = item.UfName;
                select.Selected = !string.IsNullOrEmpty(defaultValue) ? (item.UfName == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetYearOtherWelfare(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new BenefitRepository().GetYearOtherWelfare();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.Year.ToString();
                select.Text = item.Year.ToString();
                select.Selected = defaultValue.HasValue ? (item.Year == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        
    }

}