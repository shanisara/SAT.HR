using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class UserViewModel
    {
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
}
