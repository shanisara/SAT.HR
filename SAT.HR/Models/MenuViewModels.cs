using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class MenuViewModel
    {
        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Icon { get; set; }

        public int? ParentID { get; set; }

        public string MenuType { get; set; }

        public int? Sequence { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }

    }
}
