using BoardingHouse.Common.Pagination;
using BoardingHouse.Entities.Entities;
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
        public JsonResult LoadAllRoom(SearchEntity filter, int page, int pageSize = 20)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                if (ValidateRequestHeader(request))
                {
                    int totalRow = 0;
                    var data = _roomService.GetAllPaging(filter, page, pageSize, out totalRow);
                    var paginationSet = new PaginationSet<RoomEntity>()
                    {
                        Items = data,
                        TotalCount = totalRow,
                        Success = true
                    };
                    jsonResult = Json(new { lstData = paginationSet }, JsonRequestBehavior.AllowGet);
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

        public JsonResult ChangeStatus(int id)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                HttpRequestBase request = this.HttpContext.Request;
                if (ValidateRequestHeader(request))
                {
                    var data = _roomService.ChangeStatus(id);
                    if (data)
                    {
                        jsonResult = Json(new { success = true, message = "Chúc mừng bạn đã duyệt bài thành công" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonResult = Json(new { success = true, message = "Chúc mừng bạn đã hủy bài thành công" }, JsonRequestBehavior.AllowGet);
                    }
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