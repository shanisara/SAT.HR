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

        public SalaryIncreaseViewModel GetEmpSalaryIncrease(int year, int level, decimal step)
        {
            SalaryIncreaseViewModel model = new SalaryIncreaseViewModel();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    List<SalaryIncreaseSep2ViewModel> listStep2 = new List<SalaryIncreaseSep2ViewModel>();
                    var salaryincrease = db.sp_Salary_Increase_List(year, level, step).ToList();
                    foreach (var item in salaryincrease)
                    {
                        SalaryIncreaseSep2ViewModel step2 = new SalaryIncreaseSep2ViewModel();
                        step2.Year = item.Year;
                        step2.Seq = item.Seq;
                        step2.UpStep = item.UpStep;
                        step2.UserID = item.UserID;
                        step2.FullNameTh = item.FullNameTh;
                        step2.Old_Level = item.Old_Level;
                        step2.New_Level = item.New_Level;
                        step2.Old_Step = item.Old_Step;
                        step2.New_Step = item.New_Step;
                        step2.Old_Salary = item.Old_Salary;
                        step2.New_Salary = item.New_Salary;
                        listStep2.Add(step2);
                    }
                    model.Step2 = listStep2;
                }
                catch (Exception ex)
                {
                    throw;
                }
                return model;
            }
        }

        public ResponseData SalaryIncreaseConfirm(SalaryIncreaseViewModel data)
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