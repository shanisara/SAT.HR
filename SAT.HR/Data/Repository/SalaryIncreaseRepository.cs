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
        public SalaryIncreaseViewModel SalaryIncrease()
        {
            try
            {
                SalaryIncreaseViewModel model = new SalaryIncreaseViewModel();
                model.Step1 = SalaryIncreaseSep1();
                model.Step3 = new SalaryIncreaseSep3ViewModel();

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SalaryIncreaseSep1ViewModel SalaryIncreaseSep1()
        {
            try
            {
                SalaryIncreaseSep1ViewModel model = new SalaryIncreaseSep1ViewModel();
                model.Year = DateTime.Now.Year + 543;
                model.UpLevel = 1;
                model.UpStep = (decimal)1.0;

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SalaryIncreaseSep2ViewModel> GetEmpSalaryIncrease(int year, int level, decimal step)
        {
            List<SalaryIncreaseSep2ViewModel> list = new List<SalaryIncreaseSep2ViewModel>();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var salaryincrease = db.sp_Salary_Increase_List(year, level, step).ToList();
                    foreach (var item in salaryincrease)
                    {
                        SalaryIncreaseSep2ViewModel model = new SalaryIncreaseSep2ViewModel();
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

        public ResponseData SalaryIncreaseConfirm(SalaryIncreaseSep1ViewModel step1, List<SalaryIncreaseSep2ViewModel> step2, SalaryIncreaseSep3ViewModel step3)
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