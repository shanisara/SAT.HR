using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class EmployeeViewModel
    {
        public int RowNumber { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nullable<int> UserType { get; set; }

        public string UserTypeName { get; set; }

        public Nullable<int> SexID { get; set; }

        public string SexName { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public string CreateBy { get; set; }

        public Nullable<System.DateTime> ModifyDate { get; set; }

        public string ModifyBy { get; set; }

    }


    public class EmployeePageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeViewModel> data { get; set; }
    }
}