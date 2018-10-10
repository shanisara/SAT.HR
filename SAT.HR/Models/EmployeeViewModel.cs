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
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> UserTID { get; set; }
        public string UserTName { get; set; }
        public int DivID { get; set; }
        public string DivName { get; set; }
        public int DepID { get; set; }
        public string DepName { get; set; }
        public int SecID { get; set; }
        public string SecName { get; set; }
        public int PoID { get; set; }
        public string PoName { get; set; }
        public Nullable<int> SexID { get; set; }
        public string SexName { get; set; }
        public bool IsActive { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public Nullable<System.DateTime> ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

    }

    public class EmployeePageResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeViewModel> data { get; set; }
    }


}