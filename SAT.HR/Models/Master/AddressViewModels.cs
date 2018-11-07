using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class ProvinceViewModel
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    }

    public class DistrictViewModel
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int? ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    }

    public class SubDistrictViewModel
    {
        public int SubDistrictID { get; set; }
        public string SubDistrictName { get; set; }
        public int? DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int? ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    }

    public class CountryViewModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

        

}