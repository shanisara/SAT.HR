using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    [AuthorizeUser]
    public class HumanResourceController : Controller
    {
        // ระบบพัฒนาบุคลากร
        #region  บันทึกข้อมูลสมรรถนะ

        public ActionResult Capability()
        {
            return View();
        }

        #endregion

        #region  การฝึกอบรม

        public ActionResult Trainning()
        {
            return View();
        }

        #endregion

        #region  การวางแผนพัฒนารายบุคคล

        public ActionResult IndividualPlan()
        {
            return View();
        }

        #endregion

    }
}