using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    [AuthorizeUser]
    public class MenuController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            var data = new PermissionRepository().MenuByUser();
            return PartialView(data);
        }
    }
}