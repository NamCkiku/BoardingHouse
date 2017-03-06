using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Web.Controllers
{
    public class PatialViewController : Controller
    {
        // GET: PatialView
        public PartialViewResult _Header()
        {
            return PartialView();
        }
    }
}