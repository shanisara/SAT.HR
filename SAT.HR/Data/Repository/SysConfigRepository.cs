using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class SysConfigRepository
    {
        public SysConfigViewModel GetByKeyName(string keyname)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_SysConfig.Where(x => x.KeyName == keyname).FirstOrDefault();
                SysConfigViewModel model = new Models.SysConfigViewModel();
                model.KeyName = data.KeyName;
                model.KeyValue = data.KeyValue;
                model.KeyDesc = data.KeyDesc;
                model.ModifyBy = data.ModifyBy;
                model.ModifyDate = data.ModifyDate;
                return model;
            }
        }

        public List<SysConfigViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_SysConfig.Select(s => new SysConfigViewModel() {
                    KeyName = s.KeyName,
                    KeyValue = s.KeyValue,
                    KeyDesc = s.KeyDesc
                }).ToList();
                
                return data;
            }
        }

        public ResponseData UpdateByEntity(List<SysConfigViewModel> list)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    foreach (var item in list)
                    {
                        var data = db.tb_SysConfig.Single(x => x.KeyName == item.KeyName);
                        data.KeyValue = item.KeyValue;
                        //data.KeyDescription = item.KeyDescription;
                        data.ModifyBy = item.ModifyBy;
                        data.ModifyDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

    }
}