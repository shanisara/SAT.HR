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
        public int BenTID { get; set; }
        public string BenTName { get; set; }
    }
}