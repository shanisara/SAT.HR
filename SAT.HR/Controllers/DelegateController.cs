using SAT.HR.Data.Repository;
using SAT.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class DelegateController : Controller
    {
        #region มอบสิทธิอนุมัติแทน

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new DelegateRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delegate()
        {
            var model = new DelegateRepository().GetAll();
            return View(model);
        }

        public ActionResult DelegateDetail(int? id)
        {
            DelegateViewModel model = new DelegateViewModel();
            if (id.HasValue)
            {
                model = new DelegateRepository().GetByID((int)id);
            }
            return PartialView("_Delegate", model);
        }

        public JsonResult SaveDelegate(DelegateViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.DelegateID != 0)
                result = new DelegateRepository().UpdateByEntity(model);
            else
                result = new DelegateRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDelegate(int id)
        {
            var result = new DelegateRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}