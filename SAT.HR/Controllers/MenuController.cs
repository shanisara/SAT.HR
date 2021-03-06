﻿using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class MenuController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            int userid = UtilityService.User.UserID;
            var data = new MenuRepository().MenuByUser(userid);
            return PartialView(data);
        }
    }
}