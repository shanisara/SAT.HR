using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
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

    }
}