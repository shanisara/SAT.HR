using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.HR.Data.Entities;
using SAT.HR.Models;

namespace SAT.HR.Data.Repository
{
    public class SystemConfigRepository
    {
        public SystemConfigViewModel GetByKeyName(string keyname)
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_SystemConfig.Where(x => x.KeyName == keyname).FirstOrDefault();
                SystemConfigViewModel model = new Models.SystemConfigViewModel();
                model.KeyName = data.KeyName;
                model.KeyValue = data.KeyValue;
                //model.KeyDescription = data.KeyDescription;
                model.ModifyBy = data.ModifyBy;
                model.ModifyDate = data.ModifyDate;
                return model;
            }
        }

        public List<SystemConfigViewModel> GetAll()
        {
            using (SATEntities db = new SATEntities())
            {
                var data = db.tb_SystemConfig.Select((s,i) => new SystemConfigViewModel() {
                    RowNumber = i + 1,
                    KeyName = s.KeyName,
                    KeyValue = s.KeyValue,
                    //KeyDescription = s.KeyDescription
                }).ToList();
                
                return data;
            }
        }

        public ResponseData UpdateByEntity(SystemConfigViewModel newdata)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new Models.ResponseData();
                try
                {
                    var data = db.tb_SystemConfig.Single(x => x.KeyName == newdata.KeyName);
                    data.KeyValue = newdata.KeyValue;
                    //data.KeyDescription = newdata.KeyDescription;
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

    }
}