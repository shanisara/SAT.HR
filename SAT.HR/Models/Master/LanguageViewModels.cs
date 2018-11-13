using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class LanguageViewModel
    {
        public int LID { get; set; }
        public string Language { get; set; }
    }

    public class LanguageSkillViewModel
    {
        public int LkID { get; set; }
        public string LkName { get; set; }
    }

    public class LanguageSkillTypeViewModel
    {
        public int LkTID { get; set; }
        public string LkTName { get; set; }
    }
}