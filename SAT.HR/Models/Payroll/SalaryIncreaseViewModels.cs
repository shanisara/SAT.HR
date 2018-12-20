using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class SalaryIncreaseViewModel
    {
        public Nullable<int> Seq { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> UpLevel { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public string BookCmd { get; set; }
        public DateTime? DateCmd { get; set; }
        public DateTime? DateEff { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string PathFile { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }

        public SalaryIncreaseSep1ViewModel Step1 { get; set; }
        public List<SalaryIncreaseSep2ViewModel> Step2 { get; set; }
        public SalaryIncreaseSep3ViewModel Step3 { get; set; }
    }

    public class SalaryIncreaseSep1ViewModel
    {
        public Nullable<int> Seq { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> UpLevel { get; set; }
        public Nullable<decimal> UpStep { get; set; }
    }

    public class SalaryIncreaseSep2ViewModel
    {
        public Nullable<bool> Selected { get; set; }
        public Nullable<int> Year { get; set; }
        public int Seq { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public Nullable<int> UserID { get; set; }
        public string FullNameTh { get; set; }
        public Nullable<int> Old_Level { get; set; }
        public Nullable<int> New_Level { get; set; }
        public decimal Old_Step { get; set; }
        public decimal New_Step { get; set; }
        public decimal Old_Salary { get; set; }
        public decimal New_Salary { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }

    public class SalaryIncreaseSep3ViewModel
    {
        public string BookCmd { get; set; }
        public DateTime? DateCmd { get; set; }
        public DateTime? DateEff { get; set; }
        public string PathFile { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }
    }

    public class SalaryIncreaseToExport
    {
        [DisplayName("ปีบัญชี")]
        public Nullable<int> Year { get; set; }

        [DisplayName("รอบที่")]
        public int Seq { get; set; }

        [DisplayName("ชื่อ นามสกุล")]
        public string FullNameTh { get; set; }

        [DisplayName("อัตรา")]
        public Nullable<int> Level { get; set; }

        [DisplayName("ระดับ")]
        public Nullable<decimal> UpStep { get; set; }

        [DisplayName("ขั้นเก่า")]
        public decimal Old_Step { get; set; }

        [DisplayName("ขั้นใหม่")]
        public decimal New_Step { get; set; }

        [DisplayName("เงินเดือนเก่า")]
        public decimal Old_Salary { get; set; }

        [DisplayName("เงินเดือนใหม่")]
        public decimal New_Salary { get; set; }
    }

    public class SalaryIncreaseHeader
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SalaryIncreaseHeaderViewModel> data { get; set; }
    }

    public class SalaryIncreaseHeaderViewModel
    {
        public int RowNumber { get; set; }
        public int HeaderID { get; set; }
        public Nullable<int> Seq { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> UpLevel { get; set; }
        public Nullable<decimal> UpStep { get; set; }
        public string BookCmd { get; set; }
        public DateTime? DateCmd { get; set; }
        public DateTime? DateEff { get; set; }
        public string PathFile { get; set; }
        public string DateCmdText { get; set; }
        public string DateEffText { get; set; }
    }


}