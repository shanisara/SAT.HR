﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class SectionRepository
    {
        public SectionResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.vw_Section.ToList();

                int recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(x => x.DivName.Contains(filter) || x.DepName.Contains(filter) || x.SecName.Contains(filter)).ToList();
                }

                int recordsFiltered = data.Count();

                switch (sortBy)
                {
                    case "DivName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DivName).ToList() : data.OrderByDescending(x => x.DivName).ToList();
                        break;
                    case "DepName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.DepName).ToList() : data.OrderByDescending(x => x.DepName).ToList();
                        break;
                    case "SecName":
                        data = (sortDir == "asc") ? data.OrderBy(x => x.SecName).ToList() : data.OrderByDescending(x => x.SecName).ToList();
                        break;
                }

                int start = initialPage.HasValue ? (int)initialPage / (int)pageSize : 0;
                int length = pageSize ?? 10;

                var list = data.Select((m, i) => new SectionViewModel()
                {
                    RowNumber = i + 1,
                    SecID = m.SecID,
                    SecName = m.SecName,
                    SecStatus = m.SecStatus,
                    DivID = m.DivID,
                    DivName = m.DivName,
                    DepID = m.DepID,
                    DepName = m.DepName,
                }).Skip(start * length).Take(length).ToList();

                SectionResult result = new SectionResult();
                result.draw = draw ?? 0;
                result.recordsTotal = recordsTotal;
                result.recordsFiltered = recordsFiltered;
                result.data = list;

                return result;
            }
        }

        public List<SectionViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var list = db.tb_Section.Select(m => new SectionViewModel()
                {
                    SecID = m.SecID,
                    SecName = m.SecName,
                    SecStatus = m.SecStatus,
                    DivID = m.DivID,
                    DepID = m.DepID,
                }).OrderBy(x => x.SecName).ToList();
                return list;
            }
        }

        public SectionViewModel GetByID(int id)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_Section.Where(x => x.SecID == id).FirstOrDefault();
                SectionViewModel model = new Models.SectionViewModel();
                model.SecID = data.SecID;
                model.SecName = data.SecName;
                model.SecStatus = data.SecStatus;
                model.DivID = data.DivID;
                model.DepID = data.DepID;
                return model;
            }
        }

        public ResponseData AddByEntity(SectionViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    tb_Section model = new tb_Section();
                    model.SecID = data.SecID;
                    model.SecName = data.SecName;
                    model.SecStatus = data.SecStatus;
                    model.DivID = data.DivID;
                    model.DepID = data.DepID;
                    model.CreateBy = data.ModifyBy;
                    model.CreateDate = DateTime.Now;
                    model.ModifyBy = data.ModifyBy;
                    model.ModifyDate = DateTime.Now;
                    db.tb_Section.Add(model);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public ResponseData UpdateByEntity(SectionViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_Section.Single(x => x.SecID == newdata.SecID);
                    data.SecName = newdata.SecName;
                    data.SecStatus = newdata.SecStatus;
                    data.DivID = newdata.DivID;
                    data.DepID = newdata.DepID;
                    data.ModifyBy = newdata.ModifyBy;
                    data.ModifyDate = DateTime.Now;
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
                    var obj = db.tb_Section.SingleOrDefault(c => c.SecID == id);
                    if (obj != null)
                    {
                        db.tb_Section.Remove(obj);
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