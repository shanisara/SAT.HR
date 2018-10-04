using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class EmployeeController : Controller
    {
        // ระบบพนักงาน / ลูกจ้าง
        public ActionResult Index()
        {
            return View();
        }
    }
}