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
    public class BenefitDocRepository
    {
        public BenefitDocResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Benefit_Document.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.BdDocName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "BdDocName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.BdDocName).ToList() : data.OrderByDescending(x => x.BdDocName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new BenefitDocViewModel()
                {
                    RowNumber = ++i,
                    BdID = s.BdID,
                    BdDocName = s.BdDocName,
                    BdDocPath = s.BdDocPath,
                }).Skip(start * length).Take(length).ToList();

                BenefitDocResult result = new BenefitDocResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<BenefitDocViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Benefit_Document.ToList();

                var list = new List<BenefitDocViewModel>();
                foreach (var item in data)
                {
                    BenefitDocViewModel model = new Models.BenefitDocViewModel();
                    model.BdID = item.BdID;
                    model.BdDocName = item.BdDocName;
                    model.BdDocPath = item.BdDocPath;
                    model.ModifyDate = item.ModifyDate;
                    model.ModifyDateText = Convert.ToDateTime(item.ModifyDate).ToString("dd/MM/yyyy");
                    list.Add(model);
                }
                
                return list;
            }
        }

        public BenefitDocViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Benefit_Document.Where(x => x.BdID == id).FirstOrDefault();
                BenefitDocViewModel model = new Models.BenefitDocViewModel();
                model.BdID = data.BdID;
                model.BdDocName = data.BdDocName;
                model.BdDocPath = data.BdDocPath;
                return model;
            }
        }

        public ResponseData AddByEntity(BenefitDocViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Benefit_Document model = new tb_Benefit_Document();

                    if (data.fileUpload != null && data.fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(data.fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(data.fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadBenefitDoc;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFilePath = data.BdID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        string fileLocation = Path.Combine(directory, newFilePath);

                        data.fileUpload.SaveAs(fileLocation);

                        model.BdDocName = data.BdDocName;
                        model.BdDocPath = newFilePath;
                    }

                    model.BdID = data.BdID;
                    //model.BdDocName = data.BdDocName;
                    //model.BdDocPath = data.BdDocPath;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Benefit_Document.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(BenefitDocViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Benefit_Document.Single(x => x.BdID == newdata.BdID);

                    if (newdata.fileUpload != null && newdata.fileUpload.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(newdata.fileUpload.FileName);
                        var fileExt = System.IO.Path.GetExtension(newdata.fileUpload.FileName).Substring(1);

                        string directory = SysConfig.PathUploadBenefitDoc;
                        bool isExists = System.IO.Directory.Exists(directory);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(directory);

                        string newFilePath = newdata.BdID.ToString() + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + "." + fileExt;
                        newdata.BdDocPath = newFilePath;

                        string fileLocation = Path.Combine(directory, newFilePath);
                        newdata.fileUpload.SaveAs(fileLocation);

                        newdata.BdDocName = newdata.BdDocName;
                    }

                    model.BdDocName = newdata.BdDocName;
                    model.BdDocPath = newdata.BdDocPath;
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
                    var obj = db.tb_Benefit_Document.SingleOrDefault(c => c.BdID == id);
                    if (obj != null)
                    {
                        db.tb_Benefit_Document.Remove(obj);
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

        public FileViewModel DownloadBenefit(int? id)
        {
            FileViewModel model = new FileViewModel();
            try
            {
                using (SATEntities db = new SATEntities())
                {
                    var data = db.tb_Benefit_Document.Where(x => x.BdID == id).FirstOrDefault();
                    string filename = data.BdDocPath;

                    string[] fileSplit = filename.Split('.');
                    int length = fileSplit.Length - 1;
                    string fileExt = fileSplit[length].ToUpper();

                    var doctype = db.tb_Document_Type.Where(x => x.DocType == fileExt).FirstOrDefault();
                    string Contenttype = doctype.ContentType;

                    string filepath = SysConfig.PathUploadBenefitDoc;

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