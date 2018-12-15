using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserEducationViewModel
    {
        public int RowNumber { get; set; }
        public int UeID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> EduID { get; set; }
        public string EduName { get; set; }
        public Nullable<int> DegID { get; set; }
        public string DegName { get; set; }
        public Nullable<int> MajID { get; set; }
        public string MajName { get; set; }
        public string UeInstituteName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CountryName { get; set; }
        public Nullable<System.DateTime> UeGraduationDate { get; set; }
        public Nullable<decimal> UeGPA { get; set; }
        public string UeGraduationDateText { get; set; }
        public bool? UeEduOfficial { get; set; }
        public bool? UeEduOfficialLevel { get; set; }
        public string UePartFile { get; set; }
        public string GPA { get; set; }
        public List<UserEducationViewModel> ListEducation { get; set; }
    }

}