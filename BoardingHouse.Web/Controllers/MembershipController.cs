using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Web.Controllers
{
    public class MembershipController : Controller
    {
        // GET: Membership
        public ActionResult Membership()
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public PartialViewResult _PVProfileInformation()
        {
            return PartialView();
        }

    }
}