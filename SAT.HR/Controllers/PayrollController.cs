using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    [AuthorizeUser]
    public class PayrollController : Controller
    {
        // ระบบเงินเดือนและโบนัส

        #region 1. การเลื่อนขั้นเงินเดือน

        public ActionResult SalaryIncrease()
        {
            return View();
        }

        #endregion

        #region 2. การคำนวณโบนัส

        public ActionResult BonusCalculator()
        {
            return View();
        }

        #endregion
    }
}