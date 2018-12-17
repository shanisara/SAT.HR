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
        public BonusCalculatorStep1ViewModel BonusCalculator()
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

        public List<BonusCalculatorStep2ViewModel> GetEmpBonusCalculator(int year, decimal step)
        {
            List<BonusCalculatorStep2ViewModel> list = new List<BonusCalculatorStep2ViewModel>();
            using (SATEntities db = new SATEntities())
            {
                try
                {
                    var data = db.sp_Bonus_Calculator_List(year, step).ToList();
                    foreach (var item in data)
                    {
                        BonusCalculatorStep2ViewModel model = new BonusCalculatorStep2ViewModel();
                        model.Year = item.Year;
                        model.Seq = item.Seq;
                        model.UpStep = item.UpStep;
                        model.UserID = item.UserID;
                        model.FullNameTh = item.FullNameTh;
                        model.Bonus = item.Bonus;
                        model.Salary = item.Salary;
                        model.M1 = item.M1;
                        model.M2 = item.M2;
                        model.M3 = item.M3;
                        model.M4 = item.M4;
                        model.M5 = item.M5;
                        model.M6 = item.M5;
                        model.M7 = item.M5;
                        model.M8 = item.M5;
                        model.M9 = item.M5;
                        model.M10 = item.M5;
                        model.M11 = item.M5;
                        model.M12 = item.M5;
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

        public ResponseData BonusCalculatorConfirm(BonusCalculatorStep1ViewModel step1, List<BonusCalculatorStep2ViewModel> step2, BonusCalculatorStep3ViewModel step3)
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