using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Models
{
    public class CourseViewModel
    {
        public int RowNumber { get; set; }
        public int CourseID { get; set; }
        public string CourseNo { get; set; }
        public Nullable<int> CourseTID { get; set; }
        public string CourseTName { get; set; }
        public string CourseName { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CountryName { get; set; }
        public string TrainerName { get; set; }
        public string Location { get; set; }
        public string Certificate { get; set; }
        public string Remark { get; set; }
        public string PathFile { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public string DateFromText { get; set; }
        public string EndDateText { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public string StatusName { get; set; }
        public HttpPostedFileBase fileUpload { get; set; }
        public List<TrainingViewModel> ListTrainning { get; set; }

    }

    public class CourseResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<CourseViewModel> data { get; set; }
    }
}