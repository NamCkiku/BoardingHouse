using BoardingHouse.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RoomDetail()
        {
            return View();
        }
        public ActionResult ListRoom()
        {
            return View();
        }
        public ActionResult CreateRoom()
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public JsonResult GetUserLogin()
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                if (HttpContext.Request.IsAuthenticated)
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var user = manager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        jsonResult = Json(new { success = true, user = user.Result }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(Server.MapPath("~\\FileUploadFolder"));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Images");

                    var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);

                }

            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}