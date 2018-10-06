using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class SysConfigViewModel
    {
        public int RowNumber { get; set; }

        public string KeyName { get; set; }

        public string KeyValue { get; set; }

        public string KeyDesc { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyBy { get; set; }
    }
}
