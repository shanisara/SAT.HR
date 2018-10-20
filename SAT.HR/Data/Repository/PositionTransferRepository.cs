using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data.Repository
{
    public class PositionTransferRepository
    {
        public PositionTransferResult GetPage(string filter, int? draw, int? initialPage, int? pageSize, string sortDir, string sortBy, int? userType)
        {
            PositionTransferResult result = new PositionTransferResult();
            List<PositionTransferViewModel> list = new List<PositionTransferViewModel>();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    //var data = db.vw_Move_Man_Power_Head().ToList();

                    //int i = 1;
                    //foreach (var item in data)
                    //{
                    //    PositionTransferViewModel model = new PositionTransferViewModel();
                    //    model.RowNumber = ++i;
                    //    model.MopID = item.MopID;
                    //    model.MopYear = item.MopYear;
                    //    model.EmpTName = item.EmpTName;
                    //    model.MtName = item.MtName;
                    //    model.MopBookCmd = item.MopBookCmd;
                    //    model.MopTotal = item.MopTotal;
                    //    model.MopDateCmdText = item.MopDateCmdText;
                    //    model.MopStatusName = item.MopStatusName;
                    //    model.recordsTotal = (int)item.recordsTotal;
                    //    model.recordsFiltered = (int)item.recordsFiltered;
                    //    list.Add(model);
                    //}

                    result.draw = draw ?? 0;
                    result.recordsTotal = list != null ? list[0].recordsTotal : 0;
                    result.recordsFiltered = list != null ? list[0].recordsFiltered : 0;
                    result.data = list;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public PositionTransferViewModel GetByID(int? id)
        {
            PositionTransferViewModel model = new Models.PositionTransferViewModel();

            try
            {
                using (SATEntities db = new SATEntities())
                {
                    //var data = db.tb_Move_Man_Power_Detail.Where(x => x.MopID == id).FirstOrDefault();
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