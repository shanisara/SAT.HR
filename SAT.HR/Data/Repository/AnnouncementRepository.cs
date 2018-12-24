using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class AnnouncementRepository
    {
        public List<AnnouncementViewModel> GetAnnouncement()
        {
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var data = db.tb_Announcement.ToList();

                    var list = new List<AnnouncementViewModel>();
                    foreach (var item in data)
                    {
                        AnnouncementViewModel model = new AnnouncementViewModel();
                        model.AnnID = item.AnnID;
                        model.AnnDate = item.AnnDate;
                        model.AnnTopic = item.AnnTopic;
                        model.AnnSubTopic = item.AnnSubTopic;
                        model.AnnDescription = item.AnnDescription;
                        list.Add(model);
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public AnnouncementViewModel AnnouncementByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var data = db.tb_Announcement.Where(m => m.AnnID == id).FirstOrDefault();
                    AnnouncementViewModel model = new AnnouncementViewModel();
                    model.AnnID = data.AnnID;
                    model.AnnDate = data.AnnDate;
                    model.AnnTopic = data.AnnTopic;
                    model.AnnSubTopic = data.AnnSubTopic;
                    model.AnnDescription = data.AnnDescription;
                    model.AnnFileName = data.AnnFileName;
                    model.AnnFilePath = data.AnnFilePath;
                    model.StartDate = data.StartDate;
                    model.ExpireDate = data.ExpireDate;
                    model.CreateBy = data.CreateBy;
                    model.CreateDate = data.CreateDate;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = data.ModifyDate;
                    return model;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public FileViewModel DownloadAnnouncement(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Announcement.Where(x => x.AnnID == id).FirstOrDefault();
                    string filename = data.AnnFilePath;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathDownloadAnnouncement;

                    model.FileName = filename;
                    model.FilePath = filepath;
                    model.ContentType = Contenttype;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public AnnouncementResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Announcement.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.AnnTopic.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "AnnTopic":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.AnnTopic).ToList() : data.OrderByDescending(x => x.AnnTopic).ToList();
                        break;
                    case "AnnDate":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.AnnDate).ToList() : data.OrderByDescending(x => x.AnnDate).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new AnnouncementViewModel()
                {
                    RowNumber = ++i,
                    AnnID = s.AnnID,
                    AnnDateText = Convert.ToDateTime(s.AnnDate).ToString("dd/MM/yyyy"),
                    AnnTopic = s.AnnTopic,
                }).Skip(start * length).Take(length).ToList();

                AnnouncementResult result = new AnnouncementResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public AnnouncementViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Announcement.Where(x => x.AnnID == id).FirstOrDefault();
                AnnouncementViewModel model = new Models.AnnouncementViewModel();
                model.AnnID = data.AnnID;
                model.AnnDate = data.AnnDate;
                model.AnnTopic = data.AnnTopic;
                model.AnnSubTopic = data.AnnSubTopic;
                model.AnnDescription = data.AnnDescription;
                model.AnnFileName = data.AnnFileName;
                model.AnnFilePath = data.AnnFilePath;
                model.StartDate = data.StartDate;
                model.ExpireDate = data.ExpireDate;
                return model;
            }
        }

        public ResponseData AddByEntity(AnnouncementViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Announcement model = new tb_Announcement();

                    if (data.fileUpload != null && data.fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(data.fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(data.fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadAnnouncement;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFilePath = data.AnnID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFilePath);

                        data.fileUpload.SaveAs(fileLocation);

                        model.AnnFileName = data.AnnFileName;
                        model.AnnFilePath = newFilePath;
                    }

                    model.AnnID = data.AnnID;
                    model.AnnDate = data.AnnDate;
                    model.AnnTopic = data.AnnTopic;
                    model.AnnSubTopic = data.AnnSubTopic;
                    model.AnnDescription = data.AnnDescription;
                    model.AnnFileName = data.AnnFileName;
                    model.AnnFilePath = data.AnnFilePath;
                    model.StartDate = data.StartDate;
                    model.ExpireDate = data.ExpireDate;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Announcement.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(AnnouncementViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Announcement.Single(x => x.AnnID == newdata.AnnID);

                    if (newdata.fileUpload != null && newdata.fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(newdata.fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(newdata.fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadAnnouncement;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFilePath = newdata.AnnID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        model.AnnFilePath = newFilePath;

                        string fileLocation = Path.Combine(directory, newFilePath);
                        newdata.fileUpload.SaveAs(fileLocation);

                        model.AnnFileName = newdata.AnnFileName;
                        
                    }

                    model.AnnID = newdata.AnnID;
                    model.AnnDate = newdata.AnnDate;
                    model.AnnTopic = newdata.AnnTopic;
                    model.AnnSubTopic = newdata.AnnSubTopic;
                    model.AnnDescription = newdata.AnnDescription;
                    model.AnnFileName = newdata.AnnFileName;
                    model.AnnFilePath = newdata.AnnFilePath;
                    model.StartDate = newdata.StartDate;
                    model.ExpireDate = newdata.ExpireDate;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData RemoveByID(int id)
        {
            ResponseData result = new Models.ResponseData();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var obj = db.tb_Announcement.SingleOrDefault(c => c.AnnID == id);
                    if (obj != null)
                    {
                        db.tb_Announcement.Remove(obj);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.MessageCode = "";
                    result.MessageText = ex.Message;
                }
                return result;
            }
        }

    }
}