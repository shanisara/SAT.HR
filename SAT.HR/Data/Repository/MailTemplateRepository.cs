using SAT.HR.Data.Entities;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class MailTemplateRepository
    {
        public MailTemplateResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Mail_Template.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.MailCode.Contains(filter) || x.MailName.Contains(filter) || x.MailSubject.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "MailCode":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.MailCode).ToList() : data.OrderByDescending(x => x.MailCode).ToList();
                        break;
                    case "MailName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.MailName).ToList() : data.OrderByDescending(x => x.MailName).ToList();
                        break;
                    case "MailSubject":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.MailSubject).ToList() : data.OrderByDescending(x => x.MailSubject).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((s, i) => new MailTemplateViewModel()
                {
                    RowNumber = ++i,
                    MailID = s.MailID,
                    MailCode = s.MailCode,
                    MailName = s.MailName,
                    MailSubject = s.MailSubject
                }).Skip(start * length).Take(length).ToList();

                MailTemplateResult result = new MailTemplateResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<MailTemplateViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Mail_Template.Select(s => new MailTemplateViewModel()
                {
                    MailID = s.MailID,
                    MailCode = s.MailCode,
                    MailName = s.MailName,
                    MailSubject = s.MailSubject,
                    MailBody = s.MailBody,
                    MailTo = s.MailTo,
                    MailFrom = s.MailFrom,
                    MailCCTo = s.MailCCTo,
                    MailBCCTo = s.MailBCCTo,
                }).OrderBy(x => x.MailName).ToList();
                return list;
            }
        }

        public MailTemplateViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Mail_Template.Where(x => x.MailID == id).FirstOrDefault();
                MailTemplateViewModel model = new Models.MailTemplateViewModel();
                model.MailID = data.MailID;
                model.MailCode = data.MailCode;
                model.MailName = data.MailName;
                model.MailSubject = data.MailSubject;
                model.MailBody = data.MailBody;
                model.MailTo = data.MailTo;
                model.MailFrom = data.MailFrom;
                model.MailCCTo = data.MailCCTo;
                model.MailBCCTo = data.MailBCCTo;
                return model;
            }
        }

        public ResponseData AddByEntity(MailTemplateViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Mail_Template model = new tb_Mail_Template();
                    model.MailID = data.MailID;
                    model.MailCode = data.MailCode;
                    model.MailName = data.MailName;
                    model.MailSubject = data.MailSubject;
                    model.MailBody = data.MailBody;
                    model.MailTo = data.MailTo;
                    model.MailFrom = data.MailFrom;
                    model.MailCCTo = data.MailCCTo;
                    model.MailBCCTo = data.MailBCCTo;
                    model.CreateBy = UtilityService.User.UserID;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = UtilityService.User.UserID;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Mail_Template.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(MailTemplateViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var model = db.tb_Mail_Template.Single(x => x.MailID == newdata.MailID);
                    model.MailID = newdata.MailID;
                    model.MailCode = newdata.MailCode;
                    model.MailName = newdata.MailName;
                    model.MailSubject = newdata.MailSubject;
                    model.MailBody = newdata.MailBody;
                    model.MailTo = newdata.MailTo;
                    model.MailFrom = newdata.MailFrom;
                    model.MailCCTo = newdata.MailCCTo;
                    model.MailBCCTo = newdata.MailBCCTo;
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
                    var obj = db.tb_Mail_Template.SingleOrDefault(c => c.MailID == id);
                    if (obj != null)
                    {
                        db.tb_Mail_Template.Remove(obj);
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