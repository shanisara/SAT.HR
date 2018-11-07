using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.HR.Models
{
    public class CertificateViewModel
    {
        public int RowNumber { get; set; }

        public int CerID { get; set; }

        public string CerName { get; set; }

        public DateTime? CreateDate { get; set; }

        public Nullable<int> CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Nullable<int> ModifyBy { get; set; }

        public bool? CerStatus { get; set; }

        public string Status { get; set; }

    }

    public class CertificateResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<CertificateViewModel> data { get; set; }
    }
}
