using SAT.HR.Data.Repository;
using SAT.HR.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    [AuthorizeUser]
    public class EmployeeController : Controller
    {
        // ระบบพนักงาน/ลูกจ้าง 

        #region 1. ทะเบียนประวัติ

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
            var dataTableData = new EmployeeRepository().GetPage(search, draw, start, length, dir, column);
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Employee()
        {
            return PartialView("_Employee");
        }

        public ActionResult Family()
        {
            return PartialView("_Family");
        }

        public ActionResult Education()
        {
            return PartialView("_Education");
        }

        public ActionResult Position()
        {
            return PartialView("_Position");
        }

        public ActionResult Trainning()
        {
            return PartialView("_Trainning");
        }

        public ActionResult Insignia()
        {
            return PartialView("_Insignia");
        }

        public ActionResult Outstanding()
        {
            return PartialView("_Outstanding");
        }

        public ActionResult Certificate()
        {
            return PartialView("_Certificate");
        }

        public ActionResult History()
        {
            return PartialView("_History");
        }

        public ActionResult Remuneration()
        {
            return PartialView("_Remuneration");
        }

        //[HttpPost]
        //public JsonResult Employee(int? draw, int? start, int? length, List<Dictionary<string, string>> order, List<Dictionary<string, string>> columns)
        //{
        //    var search = Request["search[value]"];
        //    var dir = order[0]["dir"].ToLower();
        //    var column = columns[int.Parse(order[0]["column"])]["data"];
        //    var dataTableData = new EmployeeRepository().GetPage(search, draw, start, length, dir, column);
        //    return Json(dataTableData, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region 2. อัตรากำลังพล

        public ActionResult PositionRate()
        {
            return View();
        }

        public ActionResult _PositionRate()
        {
            return PartialView("_PositionRate");
        }

        #endregion

        #region 3. โยกย้ายระดับ

        public ActionResult EmployeeTransfer()
        {
            return View();
        }

        public ActionResult EmployeeTransferDetail()
        {
            return View();
        }
        
        public ActionResult _EmployeeTransferDetail()
        {
            return PartialView("_EmployeeTransferDetail");
        }


        #endregion

        #region 4. โยกย้ายอัตรากำลังพล

        public ActionResult PositionTransfer()
        {
            return View();
        }

        public ActionResult _PositionTransferDetail()
        {
            return PartialView("_PositionTransferDetail");
        }

        #endregion

    }
}