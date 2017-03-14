using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly ISystemSettingService _settingService;
        public HomeController(IRoomService roomService, ISystemSettingService settingService)
        {
            this._roomService = roomService;
            this._settingService = settingService;
        }
        public ActionResult Home()
        {
            //_settingService.LoadDefaultValuesSetings();
            //_settingService.LoadSystemSettingConfiguration();
            return View();
        }
    }
}