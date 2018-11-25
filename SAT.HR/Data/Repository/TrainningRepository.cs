﻿using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class TrainningRepository
    {
        public CourseResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            CourseResult result = new CourseResult();
            List<CourseViewModel> list = new List<CourseViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.vw_Course.ToList();

                    int recordsTotal = data.Count();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        data = data.Where(x => x.CountryName.Contains(filter)).ToList();
                    }

                    int recordsFiltered = data.Count();

                    switch (sortBy)
                    {
                        case "CountryName":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.CountryName).ToList() : data.OrderByDescending(x => x.CountryName).ToList(); break;
                        case "DateFromText":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.DateFrom).ToList() : data.OrderByDescending(x => x.DateFrom).ToList(); break;
                        case "DateToText":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.DateTo).ToList() : data.OrderByDescending(x => x.DateTo).ToList(); break;
                        case "Total":
                            data = (sortDir == "asc") ? data.OrderBy(x => x.Total).ToList() : data.OrderByDescending(x => x.Total).ToList(); break;
                        //case "StatusName":
                        //    data = (sortDir == "asc") ? data.OrderBy(x => x.StatusName).ToList() : data.OrderByDescending(x => x.StatusName).ToList(); break;

                    }

                    int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                    int length = pageSize ?? 10;

                    int i = 0;
                    foreach (var item in data)
                    {
                        CourseViewModel model = new CourseViewModel();
                        model.RowNumber = ++i;
                        model.CourseID = item.CourseID;
                        model.CourseNo = item.CourseNo;
                        model.CourseTName = item.CourseTName;
                        model.CourseName = item.CourseName;
                        model.DateFrom = item.DateFrom;
                        model.DateTo = item.DateTo;
                        model.Total = item.Total;
                        model.DateFromText = (item.DateFrom.HasValue) ? item.DateFrom.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.DateToText = (item.DateTo.HasValue) ? item.DateTo.Value.ToString("dd/MM/yyyy") : string.Empty;
                        model.StatusName = item.StatusName;
                        model.recordsTotal = recordsTotal;
                        model.recordsFiltered = recordsFiltered;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            result.draw = draw ?? 0;
            result.recordsTotal = list.Count() > 0 ? list[0].recordsTotal : 0;
            result.recordsFiltered = list.Count() > 0 ? list[0].recordsFiltered : 0;
            result.data = list;

            return result;
        }

        public CourseViewModel GetByID(int? id)
        {
            try
            {
                CourseViewModel model = new CourseViewModel();
                using (SATEntities db = new SATEntities())
                {
                    if (id != null)
                    {
                        var data = db.tb_Course.Where(x => x.CourseID == id).FirstOrDefault();
                        model.CourseID = data.CourseID;
                        model.CourseNo = data.CourseNo;
                        model.CourseTID = data.CourseTID;
                        model.CourseName = data.CourseName;
                        model.DateFrom = data.DateFrom;
                        model.DateTo = data.DateTo;
                        model.CountryID = data.CountryID;
                        model.TrainerName = data.TrainerName;
                        model.Location = data.Location;
                        model.Certificate = data.Certificate;
                        model.CourseDesc = data.CourseDesc;
                        model.PathFile = data.PathFile;

                        var detail = GetDetail(model.CourseID);
                        model.ListTrainning = detail;
                    }
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TrainingViewModel> GetDetail(int? id)
        {
            var list = new List<TrainingViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    int index = 1;
                    var detail = db.vw_Trainning_Course.Where(x => x.CourseID == id).ToList();

                    foreach (var item in detail)
                    {
                        TrainingViewModel model = new TrainingViewModel();
                        model.RowNumber = index++;
                        model.CourseID = item.CourseID;
                        model.UserID = item.UserID;
                        model.FullName = item.FullName;
                        model.DivName = item.DivName;
                        model.DepName = item.DepName;
                        model.SecName = item.SecName;
                        model.PoName = item.PoName;
                        list.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public ResponseData AddByEntity(CourseViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        tb_Course head = new tb_Course();

                        if (data.fileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = data.fileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadCourse;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = data.CourseNo + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.PathFile = newFileName;
                            }
                        }

                        head.CourseNo = DocumentNumberRepository.GetNextNumber("COURSE");
                        head.CourseTID = data.CourseTID;
                        head.CourseName = data.CourseName;
                        head.CourseDesc = data.CourseDesc;
                        head.DateFrom = data.DateFrom;
                        head.DateTo = data.DateTo;
                        head.CountryID = data.CountryID;
                        head.TrainerName = data.TrainerName;
                        head.Location = data.Location;
                        head.Certificate = data.Certificate;
                        head.CreateBy = UtilityService.User.UserID;
                        head.CreateDate = DateTime.Now;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.tb_Course.Add(head);
                        db.SaveChanges();

                        result.ID = head.CourseID;

                        if (data.ListTrainning != null)
                        {
                            foreach (var item in data.ListTrainning)
                            {
                                tb_Training_Course detail = new tb_Training_Course();
                                detail.CourseID = head.CourseID;
                                detail.UserID = item.UserID;
                                detail.CreateBy = UtilityService.User.UserID;
                                detail.CreateDate = DateTime.Now;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Training_Course.Add(detail);
                                db.SaveChanges();
                            }
                        }
                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }
                    return result;
                }
            }
        }

        public ResponseData UpdateByEntity(CourseViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    ResponseData result = new Models.ResponseData();
                    try
                    {
                        var head = db.tb_Course.Single(x => x.CourseID == newdata.CourseID);

                        if (newdata.fileUpload != null)
                        {
                            HttpPostedFileBase fileUpload = newdata.fileUpload;
                            if (fileUpload != null && fileUpload.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(fileUpload.FileName);
                                var fileExt = System.IO.Path.GetExtension(fileUpload.FileName).Substring(1);

                                string directory = SysConfig.PathUploadCourse;
                                bool isExists = System.IO.Directory.Exists(directory);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(directory);

                                string newFileName = newdata.CourseNo + "." + fileExt;
                                string fileLocation = Path.Combine(directory, newFileName);

                                fileUpload.SaveAs(fileLocation);

                                head.PathFile = newFileName;
                            }
                        }

                        head.CourseID = newdata.CourseID;
                        head.CourseTID = newdata.CourseTID;
                        head.CourseName = newdata.CourseName;
                        head.DateFrom = newdata.DateFrom;
                        head.DateTo = newdata.DateTo;
                        head.CountryID = newdata.CountryID;
                        head.TrainerName = newdata.TrainerName;
                        head.Location = newdata.Location;
                        head.Certificate = newdata.Certificate;
                        head.CourseDesc = newdata.CourseDesc;
                        head.ModifyBy = UtilityService.User.UserID;
                        head.ModifyDate = DateTime.Now;
                        db.SaveChanges();

                        var listdelete = db.tb_Training_Course.Where(x => x.CourseID == newdata.CourseID).ToList();
                        db.tb_Training_Course.RemoveRange(listdelete);
                        db.SaveChanges();

                        if (newdata.ListTrainning != null)
                        {
                            foreach (var item in newdata.ListTrainning)
                            {
                                tb_Training_Course detail = new tb_Training_Course();
                                detail.CourseID = head.CourseID;
                                detail.UserID = item.UserID;
                                detail.ModifyBy = UtilityService.User.UserID;
                                detail.ModifyDate = DateTime.Now;
                                db.tb_Training_Course.Add(detail);
                                db.SaveChanges();

                                var usertraining = db.tb_User_Training.Where(x => x.UserID == item.UserID && x.UtCourse == head.CourseName).FirstOrDefault();
                                if (usertraining != null)
                                {
                                    UserTrainningViewModel ut = new UserTrainningViewModel();
                                    ut.UserID = item.UserID;
                                    ut.TtID = head.CourseTID;
                                    ut.CountryID = head.CountryID;
                                    ut.UtCourse = head.CourseName;
                                    ut.UtStartDate = head.DateFrom;
                                    ut.UtEndDate = head.DateTo;
                                    new EmployeeRepository().AddTrainingByEntity(ut);
                                }
                            }
                        }

                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }
                    return result;
                }
            }
        }

        public ResponseData DeleteCourseTrainning(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                using (var transection = db.Database.BeginTransaction())
                {
                    try
                    {
                        var listdelete = db.tb_Training_Course.Where(x => x.CourseID == id).ToList();
                        db.tb_Training_Course.RemoveRange(listdelete);
                        db.SaveChanges();

                        var itemdelete = db.tb_Course.Where(x => x.CourseID == id).FirstOrDefault();
                        db.tb_Course.Remove(itemdelete);
                        db.SaveChanges();

                        transection.Commit();
                    }
                    catch (Exception ex)
                    {
                        transection.Rollback();
                        result.MessageCode = "";
                        result.MessageText = ex.Message;
                    }
                    return result;
                }
            }
        }

        public FileViewModel DownloadFileCourse(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Course.Where(x => x.CourseID == id).FirstOrDefault();
                    string filename = data.PathFile;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    //string filepath = SysConfig.PathDownloadCourse;

                    model.FileName = filename;
                    //model.FilePath = filepath;
                    model.ContentType = Contenttype;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        //public List<CourseViewModel> GetCourseType()
        //{
        //    using (SATEntities db = new SATEntities())
        //    {
        //        var list = db.tb_Course_Type.Select(s => new CourseViewModel()
        //        {
        //            CountryID = s.CourseTID,
        //            CountryName = s.CourseTName,
        //        })
        //        .OrderBy(o => o.CountryName)
        //        .ToList();
        //        return list;
        //    }
        //}

        public List<TrainingTypeViewModel> GetTrainingType()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Training_Type.Select(s => new TrainingTypeViewModel()
                {
                    TrainingTypeID = s.TtID,
                    TrainingTypeName = s.TtName
                }).ToList();
                return list;
            }
        }
    }


}