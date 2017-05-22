using BoardingHouse.Administrator.Models;
using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
{
    public class SystemController : Controller
    {
        private readonly ISystemSettingService _systemSettingService;
        public SystemController(ISystemSettingService systemSettingService)
        {
            this._systemSettingService = systemSettingService;
        }
        // GET: System
        public ActionResult System()
        {
            return View();
        }
        public JsonResult SaveThemeSetting(SystemSettingModalView settings)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                if (settings != null)
                {
                    _systemSettingService.SettingUpdate("DateFormat", settings.DateFormat);
                    _systemSettingService.LoadSystemSettingConfiguration();
                    jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                throw ex;
            }
            return jsonResult;
        }
    }
}