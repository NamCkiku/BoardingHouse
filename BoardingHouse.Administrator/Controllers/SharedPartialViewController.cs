using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
{
    public class SharedPartialViewController : Controller
    {
        // GET: SharedPartialView
        public PartialViewResult _PVSearchControl()
        {
            return PartialView();
        }
    }
}