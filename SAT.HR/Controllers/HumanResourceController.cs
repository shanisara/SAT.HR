using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT.HR.Models;
using SAT.HR.Data.Repository;
using SAT.HR.Helpers;

namespace SAT.HR.Controllers
{
    public class HumanResourceController : BaseController
    {
        #region สมรรถนะ - Capability

        public ActionResult Capability()
        {
            return View();
        }

        public ActionResult CapabilityHeader(int? id)
        {
            CapabilityViewModel model = new CapabilityViewModel();
            if (id.HasValue)
            {
                model = new CapabilityRepository().GetByID((int)id);
            }
            ViewBag.CapabilityType = DropDownList.GetCapabilityType(model.CapTID);
            ViewBag.CapabilityGroup = DropDownList.GetCapabilityGroup(model.CapGID);
            ViewBag.CapabilityGroupType = DropDownList.GetCapabilityGroupType(model.CapGID, model.CapGTID);
            return PartialView("_Capability", model);
        }

        public JsonResult CapabilityGroupType(int capgid)
        {
            var result = DropDownList.GetCapabilityGroupType(capgid, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Capability(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        {
            var search = Request["search[value]"];
            var dir = order[0]["dir"].ToLower();
            var column = columns[int.Parse(order[0]["column"])]["data"];
            var dataTableData = new CapabilityRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveCapability(CapabilityViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.CapID != 0)
                result = new CapabilityRepository().UpdateByEntity(model);
            else
                result = new CapabilityRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCapability(int id)
        {
            var result = new CapabilityRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CapabilityDetail(int? id)
        {
            List<CapabilityDetailViewModel> model = new List<CapabilityDetailViewModel>();
            if (id.HasValue)
            {
                model = new CapabilityDetailRepository().GetByCap((int)id);
            }

            return PartialView("_CapabilityDetail", model);
        }

        public JsonResult SaveCapabilityDetail(CapabilityDetailViewModel model)
        {
            ResponseData result = new Models.ResponseData();
            if (model.CapID != 0)
                result = new CapabilityDetailRepository().UpdateByEntity(model);
            else
                result = new CapabilityDetailRepository().AddByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubmitCapabilityDetail(List<CapabilityDetailViewModel> model)
        {
            ResponseData result = new Models.ResponseData();
            result = new CapabilityDetailRepository().SubmitByEntity(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteCapabilityDetail(int id)
        {
            var result = new CapabilityDetailRepository().RemoveByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion
        
        #region  บันทึกข้อมูลสมรรถนะ

        public ActionResult Evaluation()
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