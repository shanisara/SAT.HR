using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EmpBonusCalculatorViewModel
    {
        public Nullable<int> Year { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> Seq { get; set; }
        public string FullNameTh { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public Nullable<decimal> Bonus { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<decimal> M1 { get; set; }
        public Nullable<decimal> M2 { get; set; }
        public Nullable<decimal> M3 { get; set; }
        public Nullable<decimal> M4 { get; set; }
        public Nullable<decimal> M5 { get; set; }
        public Nullable<decimal> M6 { get; set; }
        public Nullable<decimal> M7 { get; set; }
        public Nullable<decimal> M8 { get; set; }
        public Nullable<decimal> M9 { get; set; }
        public Nullable<decimal> M10 { get; set; }
        public Nullable<decimal> M11 { get; set; }
        public Nullable<decimal> M12 { get; set; }
    }

    public class BonusCalculatorProcessViewModel
    {
        public Nullable<int> Year { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public string BookCmd { get; set; }
        public DateTime? DateCmd { get; set; }
        public string PathFile { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }

        public List<EmpBonusCalculatorViewModel> detail { get; set; }
    }
}