using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class UserViewModel
    {
        public string RowNumber { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }

        public string SurName { get; set; }

        public int? GroupID { get; set; }

        public int? SexID { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
        
    }

    public class UserResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<UserViewModel> data { get; set; }
    }

    public class UserProfile
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int DivID { get; set; }

        public string DivName { get; set; }

        public int DepID { get; set; }

        public string DepName { get; set; }

        public int SecID { get; set; }

        public string SecName { get; set; }

        public int PoID { get; set; }

        public string PoName { get; set; }

        public int? UserTypeID { get; set; }

        public string UserTypeName { get; set; }

        
    }
}
