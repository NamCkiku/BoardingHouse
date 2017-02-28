using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            this._roomService = roomService;
        }
        // GET: Room
        public ActionResult Room()
        {
            return View();
        }
        public JsonResult LoadAllRoom()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                if (ValidateRequestHeader(request))
                {
                    jsonResult = Json(new { success = true, lstData = _roomService.GetAll() }, JsonRequestBehavior.AllowGet);
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