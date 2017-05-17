using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
{
    public class ManagementController : BaseController
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly ICommonService _commonService;
        public ManagementController(IRoomTypeService roomTypeService, ICommonService commonService)
        {
            this._roomTypeService = roomTypeService;
            this._commonService = commonService;
        }
        public JsonResult LoadAllRoomType()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var listprovince = _roomTypeService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();
                //var listprovinceVm = Mapper.Map<List<ProvinceViewModel>>(listprovince);
                jsonResult = Json(new { success = true, lstData = listprovince }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
        public JsonResult LoadAllProvince()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var listprovince = _commonService.GetAllProvince().OrderByDescending(x => x.name).ToList();
                jsonResult = Json(new { success = true, lstData = listprovince }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
        public JsonResult LoadAllDistrict()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var listdistrict = _commonService.GetAllDistrict().OrderByDescending(x => x.name).ToList();
                jsonResult = Json(new { success = true, lstData = listdistrict }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
        public JsonResult LoadAllWard()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                var listward = _commonService.GetAllWard().OrderByDescending(x => x.name).ToList();
                jsonResult = Json(new { success = true, lstData = listward }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
    }
}