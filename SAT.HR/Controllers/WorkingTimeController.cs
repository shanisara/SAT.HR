using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class WorkingTimeController : Controller
    {
        // GET: การเข้าปฏิบัติงาน
        public ActionResult Index()
        {
            return View();
        }
    }
}