using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class ManageUserViewModel
    {
        public int UserID { get; set; }
        public string FullNameTh { get; set; }
        public string PositionName { get; set; }
        public string FullDepartment { get; set; }
        public bool IsActive { get; set; }
        public bool IsTerminate { get; set; }
    }
}