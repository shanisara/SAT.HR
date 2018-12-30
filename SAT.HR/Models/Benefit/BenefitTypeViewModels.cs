using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class BenefitViewModel
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public decimal IDCard { get; set; }
        public string FullNameTh { get; set; }
    }

    public class BenefitTypeViewModel
    {
        public int RowNumber { get; set; }
        public int BenTID { get; set; }
        public string BenTName { get; set; }
        public DateTime? CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
    }

    public class BenefitTypeResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<BenefitTypeViewModel> data { get; set; }
    }

}