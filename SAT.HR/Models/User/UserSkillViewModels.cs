using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class UserSkillViewModel
    {
        public int RowNumber { get; set; }
        public int UskID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string FullNameTh { get; set; }
        public Nullable<int> LID { get; set; }
        public string Language { get; set; }
        public Nullable<int> LkID { get; set; }
        public string LkName { get; set; }
        public Nullable<int> LkTID { get; set; }
        public string LkTName { get; set; }
        public string LIOther { get; set; }
        public decimal? Score { get; set; }
        public List<UserSkillViewModel> ListSkill { get; set; }

    }
}