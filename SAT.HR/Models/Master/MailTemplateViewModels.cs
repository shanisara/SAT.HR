using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class MailTemplateViewModel
    {
        public int RowNumber { get; set; }
        public int MailID { get; set; }
        public string MailCode { get; set; }
        public string MailName { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string MailCCTo { get; set; }
        public string MailBCCTo { get; set; }
    }
    public class MailTemplateResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<MailTemplateViewModel> data { get; set; }
    }
    
}