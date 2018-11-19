using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class TrainingViewModel
    {
        public int RowNumber { get; set; }
        public int CourseID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string DivName { get; set; }
        public string DepName { get; set; }
        public string SecName { get; set; }
        public string PoName { get; set; }
    }

    public class TrainingTypeViewModel
    {
        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }
    }

}