using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data
{
    public class SalaryIncreaseRepository
    {
        public List<SalaryIncreaseProcessViewModel> SalaryIncreaseProcess(int year, int level, decimal step)
        {
            List<SalaryIncreaseProcessViewModel> list = new List<SalaryIncreaseProcessViewModel>();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var salaryincrease = db.sp_Salary_Increase_List(year, level, step).ToList();
                    foreach (var item in salaryincrease)
                    {
                        SalaryIncreaseProcessViewModel model = new SalaryIncreaseProcessViewModel();
                        model.Year = item.Year;
                        model.Seq = item.Seq;
                        model.UpStep = item.UpStep;
                        model.UserID = item.UserID;
                        model.FullNameTh = item.FullNameTh;
                        model.Old_Level = item.Old_Level;
                        model.New_Level = item.New_Level;
                        model.Old_Step = item.Old_Step;
                        model.New_Step = item.New_Step;
                        model.Old_Salary = item.Old_Salary;
                        model.New_Salary = item.New_Salary;
                        list.Add(model);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                return list;
            }
        }

        public ResponseData SalaryIncreaseConfirm(SalaryIncreaseProcessViewModel data)
        {
            using (SATEntities db = new SATEntities())
            {
                ResponseData result = new ResponseData();
                try
                {
                    
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