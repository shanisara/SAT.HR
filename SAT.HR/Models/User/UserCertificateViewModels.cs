using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserCertificateViewModel
    {
        public int RowNumber { get; set; }
        public int UcID { get; set; }
        public int UserID { get; set; }
        public string FullNameTH { get; set; }
        public string FullNameEn { get; set; }
        public Nullable<int> CerId { get; set; }
        public string CerName { get; set; }
        public Nullable<System.DateTime> UcRecDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string UcRecDateText { get; set; }

        public List<UserCertificateViewModel> ListCertificate { get; set; }
    }

}