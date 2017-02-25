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
        public HomeController(IRoomService roomService)
        {
            this._roomService = roomService;
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}