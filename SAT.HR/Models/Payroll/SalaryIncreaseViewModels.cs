using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EmpSalaryIncreaseViewModel
    {
        public Nullable<int> Year { get; set; }
        public int Seq { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public Nullable<int> UserID { get; set; }
        public string FullNameTh { get; set; }
        public Nullable<int> Old_Level { get; set; }
        public decimal New_Level { get; set; }
        public decimal Old_Step { get; set; }
        public decimal New_Step { get; set; }
        public decimal Old_Salary { get; set; }
        public decimal New_Salary { get; set; }
    }

    public class SalaryIncreaseProcessViewModel
    {
        public Nullable<int> Year { get; set; }
        public Nullable<int> UpLevel { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public string BookCmd { get; set; }
        public DateTime? DateCmd { get; set; }
        public DateTime? DateEff { get; set; }
        public string PathFile { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }
    }
}