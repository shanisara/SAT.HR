using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class DocumentNumberViewModel
    {
        public int ObjectID { get; set; }
        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public Nullable<int> NumLength { get; set; }
        public Nullable<int> CurrentNum { get; set; }
        public Nullable<bool> ZeroContain { get; set; }
        public Nullable<bool> ResetMonth { get; set; }
        public Nullable<bool> ResetYear { get; set; }
        public Nullable<int> LastMonth { get; set; }
        public Nullable<int> LastYear { get; set; }
        public string Culture { get; set; }
    }
}