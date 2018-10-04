using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class ReportController : Controller
    {
        #region // รายงาน:ส่วนงานทรัพยากรบุคคล
        public ActionResult Index1()
        {
            return View();
        }

        #endregion 

        #region // รายงาน:ส่วนงานสวัสดิการ
        public ActionResult Index2()
        {
            return View();
        }

        #endregion 

        #region // รายงาน:ส่วนงานพัฒนาบุคลากร
        public ActionResult Index3()
        {
            return View();
        }

        #endregion 
    }
}