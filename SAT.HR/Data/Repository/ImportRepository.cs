using SAT.HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Data.Repository
{
    public class ImportRepository
    {


        public List<SelectListItem> Subject()
        {
            using (SATEntities db = new SATEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();

                var data = db.tb_Import_Master.Where(m => m.ParentID == null).ToList();
                foreach (var item in data)
                {
                    SelectListItem select = new SelectListItem();
                    select.Value = item.ID.ToString();
                    select.Text = item.Name;
                    list.Add(select);
                }
                return list;
            }
        }

        public List<SelectListItem> SubSubject(int? parentid)
        {
            using (SATEntities db = new SATEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();

                var data = db.tb_Import_Master.Where(m => m.ParentID == parentid).ToList();
                foreach (var item in data)
                {
                    SelectListItem select = new SelectListItem();
                    select.Value = item.ID.ToString();
                    select.Text = item.Name;
                    list.Add(select);
                }
                return list;
            }
        }


    }
}