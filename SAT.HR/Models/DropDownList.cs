using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT.HR.Data.Repository;

namespace SAT.HR.Models
{
    public class DropDownList
    {
        public static List<SelectListItem> GetDivision()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DivisionRepository().GetAll();
            data = data.Where(m => m.DivStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DivID.ToString();
                select.Text = item.DivName;
                list.Add(select);
            }
            //list.Insert(0, (new SelectListItem { Text = "-- กรุณาเลือก -- ", Value = "-1" }));
            return list;
        }

        public static List<SelectListItem> GetDepartment(int? divid)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DepartmentRepository().GetAll();
            data = data.Where(m => m.DepStatus == true).ToList();

            if (divid.HasValue)
                data.Where(m => m.DivID == divid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DepID.ToString();
                select.Text = item.DepName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetSection(int? divid, int? depid)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SectionRepository().GetAll();
            data = data.Where(m => m.SecStatus == true).ToList();

            if (divid.HasValue)
                data.Where(m => m.DivID == divid).ToList();

            if (depid.HasValue)
                data.Where(m => m.DepID == depid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SecID.ToString();
                select.Text = item.SecName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDiscipline()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DisciplineRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DisID.ToString();
                select.Text = item.DisName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetPosition()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new PositionRepository().GetAll();
            data = data.Where(m => m.PoStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.PoID.ToString();
                select.Text = item.PoName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetLevel()
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

        public static List<SelectListItem> GetCertificate()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new CertificateRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CerID.ToString();
                select.Text = item.CerName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetInsignia()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new InsigniaRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.InsID.ToString();
                select.Text = item.InsFullName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetTitle(int? sexid)
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
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetEducation()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new EducationRepository().GetAll();
            data = data.Where(m => m.EduStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.EduID.ToString();
                select.Text = item.EduName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetDegree()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new DegreeRepository().GetAll();
            data.Where(m => m.DegStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.DegID.ToString();
                select.Text = item.DegName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetMajor()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new MajorRepository().GetAll();
            data = data.Where(m => m.MajStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.MajID.ToString();
                select.Text = item.MajName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetNationality()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new NationalityRepository().GetAll();
            data = data.Where(m => m.NatStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.NatID.ToString();
                select.Text = item.NatName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetReligion()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new ReligionRepository().GetAll();
            data = data.Where(m => m.RelStatus == true).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.RelD.ToString();
                select.Text = item.RelName;
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> GetLeaveType()
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

        public static List<SelectListItem> GetCapability(int? year, int? menuid, int? typeid, int? groupid)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new CapabilityRepository().GetAll();

            if (year.HasValue)
                data = data.Where(m => m.CapYear == year).ToList();

            if (menuid.HasValue)
                data = data.Where(m => m.MenuID == menuid).ToList();

            if (typeid.HasValue)
                data = data.Where(m => m.CapTID == typeid).ToList();

            if (groupid.HasValue)
                data = data.Where(m => m.CapGroupID == groupid).ToList();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.CapID.ToString();
                select.Text = item.CapTID.ToString();
                list.Add(select);
            }
            return list;
        }

        public static List<SelectListItem> getSex()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var data = new SexRepository().GetAll();

            foreach (var item in data)
            {
                SelectListItem select = new SelectListItem();
                select.Value = item.SexID.ToString();
                select.Text = item.SexName;
                list.Add(select);
            }
            return list;
        }

        
    }

}