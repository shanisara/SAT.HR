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
        public int FullName { get; set; }
        public int PositionName { get; set; }
    }

    public class TrainingTypeViewModel
    {
        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }
    }

}