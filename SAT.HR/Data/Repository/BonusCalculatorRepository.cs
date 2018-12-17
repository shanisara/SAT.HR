using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAT.HR.Data
{
    public class BonusCalculatorRepository
    {
        public BonusCalculatorViewModel BonusCalculator()
        {
            try
            {
                BonusCalculatorViewModel model = new BonusCalculatorViewModel();
                model.Step1 = SalaryIncreaseSep1();
                model.Step3 = new BonusCalculatorStep3ViewModel();

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BonusCalculatorStep1ViewModel SalaryIncreaseSep1()
        {
            try
            {
                BonusCalculatorStep1ViewModel model = new BonusCalculatorStep1ViewModel();
                model.Year = DateTime.Now.Year + 543;
                model.UpStep = (decimal)1.00;

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BonusCalculatorViewModel GetEmpBonusCalculator(int year, decimal step)
        {
            BonusCalculatorViewModel model = new BonusCalculatorViewModel();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    List<BonusCalculatorStep2ViewModel> listStep2 = new List<BonusCalculatorStep2ViewModel>();
                    var data = db.sp_Bonus_Calculator_List(year, step).ToList();

                    foreach (var item in data)
                    {
                        BonusCalculatorStep2ViewModel step2 = new BonusCalculatorStep2ViewModel();
                        step2.Year = item.Year;
                        step2.Seq = item.Seq;
                        step2.UpStep = item.UpStep;
                        step2.UserID = item.UserID;
                        step2.FullNameTh = item.FullNameTh;
                        step2.Bonus = item.Bonus;
                        step2.Salary = item.Salary;
                        step2.M1 = item.M1;
                        step2.M2 = item.M2;
                        step2.M3 = item.M3;
                        step2.M4 = item.M4;
                        step2.M5 = item.M5;
                        step2.M6 = item.M5;
                        step2.M7 = item.M5;
                        step2.M8 = item.M5;
                        step2.M9 = item.M5;
                        step2.M10 = item.M5;
                        step2.M11 = item.M5;
                        step2.M12 = item.M5;
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

        public ResponseData BonusCalculatorConfirm(BonusCalculatorViewModel data)
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