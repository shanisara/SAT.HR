using SAT.HR.Data.Entities;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SAT.HR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult LockScreen()
        {

            return PartialView();
        }


        [HttpPost]
        public ActionResult Login()
        {
            return RedirectToAction("Dashboard");
        }


    }
}