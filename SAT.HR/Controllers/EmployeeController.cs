using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAT.HR.Controllers
{
    public class EmployeeController : Controller
    {
        // ระบบพนักงาน/ลูกจ้าง 

        #region 1. ทะเบียนประวัติ

        public ActionResult Employee()
        {
            return View();
        }

        public ActionResult _Profile()
        {
            return PartialView("_Profile");
        }

        public ActionResult _Family()
        {
            return PartialView("_Family");
        }

        public ActionResult _Education()
        {
            return PartialView("_Education");
        }

        public ActionResult _Position()
        {
            return PartialView("_Position");
        }

        public ActionResult _Trainning()
        {
            return PartialView("_Trainning");
        }

        public ActionResult _insignia()
        {
            return PartialView("_insignia");
        }

        public ActionResult _Remuneration()
        {
            return PartialView("_Remuneration");
        }

        public ActionResult _Certificate()
        {
            return PartialView("_Certificate");
        }

        public ActionResult _History()
        {
            return PartialView("_History");
        }

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

        public ActionResult PositionTransferDetail()
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