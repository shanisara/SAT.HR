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

        public static List<SelectListItem> GetPosition(int? defaultValue, bool isActive)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRepository().GetAll();
            if (isActive == true)
                data = data.Where(m => m.PoStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PoID.ToString();
                select.Text = item.PoName;
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

        public static List<SelectListItem> GetActionType(string poid, string type)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new ActionTypeRepository().GetAll();

            if (!string.IsNullOrEmpty(poid))
                data = data.Where(m => m.ActPos == poid).ToList();

            if (!string.IsNullOrEmpty(type))
                data = data.Where(m => m.ActType == type).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.ActID.ToString();
                select.Text = item.ActName;
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
                list = GetPosition(defaultValue, false);
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

        public static List<SelectListItem> GetFamilyType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetFamilyType();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.FamTID.ToString();
                select.Text = item.FamTName;
                select.Selected = defaultValue.HasValue ? (item.FamTID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetUserStatus(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }


        
        public static List<SelectListItem> GetBloodType(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }
        public static List<SelectListItem> GetMaritalStatus(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
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
        public static List<SelectListItem> GetSalaryStep(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            //var data = new SalaryRepository().GetSalaryStep();

            //foreach (var item in data)
            //{
            //    SelectListItem select = new SelectListItem();
            //    select.Value = item.SalaryStep.ToString();
            //    select.Text = item.SalaryStep;
            //    select.Selected = defaultValue.HasValue ? (item.SalaryStep == defaultValue ? true : false) : false;
            //    list.Add(select);
            //}
            return list;
        }

        public static List<SelectListItem> GetProvince(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }
        public static List<SelectListItem> GetDistrict(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }
        public static List<SelectListItem> GetSubDistrict(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }
        public static List<SelectListItem> GovernmentHelper(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }
        public static List<SelectListItem> GetAgent(int? defaultValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EmployeeRepository().GetUserStatus();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.UserSID.ToString();
                select.Text = item.UserSName;
                select.Selected = defaultValue.HasValue ? (item.UserSID == defaultValue ? true : false) : false;
                list.Add(select);
            }
            return list;
        }

    }

}