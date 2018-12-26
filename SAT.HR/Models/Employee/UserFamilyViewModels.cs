using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserFamilyViewModel
    {
        public int RowNumber { get; set; }
        public int UfID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<decimal> IDCard { get; set; }
        public string UfName { get; set; }
        public Nullable<decimal> UfCardID { get; set; }
        public Nullable<System.DateTime> UfDOB { get; set; }
        public Nullable<bool> UfLifeStatus { get; set; }
        public string UfLifeStatusName { get; set; }
        public Nullable<bool> TdStatus { get; set; }
        public string TdName { get; set; }
        public Nullable<int> PoID { get; set; }
        public string PoName { get; set; }
        public Nullable<int> OcID { get; set; }
        public string OcName { get; set; }
        public Nullable<System.DateTime> UfWeddingDate { get; set; }
        public Nullable<System.DateTime> DivorceDate { get; set; }
        public Nullable<bool> UfStudyStatus { get; set; }
        public Nullable<int> RecID { get; set; }
        public string RecName { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public string MaritalName { get; set; }
        public string UfAge { get; set; }
        public string UfStudyStatusName { get; set; }
        public string UfWeddingDateText { get; set; }
        public string DivorceDateText { get; set; }
        public string UfDOBText { get; set; }
        public int CountFather { get; set; }
        public int CountMother { get; set; }
        public Nullable<int> DivID { get; set; }
        public Nullable<int> DepID { get; set; }
        public Nullable<int> SecID { get; set; }

        public List<UserFamilyViewModel> ListFamily { get; set; }
        public List<UserFamilyViewModel> ListFather { get; set; }
        public List<UserFamilyViewModel> ListMother { get; set; }
        public List<UserFamilyViewModel> ListSpouse { get; set; }
        public List<UserFamilyViewModel> ListChild { get; set; }
    }

}