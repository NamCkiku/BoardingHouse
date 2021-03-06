﻿using BoardingHouse.Web.Infrastructure.Core;
using BoardingHouse.Web.Models.SearchViewModel;
using BoardingHouse.Web.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BoardingHouse.Web.Controllers
{
    public class ManagementController : BaseController
    {
        HttpClient client;
        string url = "http://localhost:6568/api";
        public ManagementController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Management
        #region Room
        [OutputCache(Duration = int.MaxValue)]
        public JsonResult GetAllProvince()
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/management/getallprovince").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var lstProvince = JsonConvert.DeserializeObject<List<ProvinceViewModel>>(responseData);
                    jsonResult = Json(new { success = true, lstData = lstProvince }, JsonRequestBehavior.AllowGet);
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
        [OutputCache(Duration = int.MaxValue)]
        public JsonResult GetAllDistrict()
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/management/getalldistrict").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var lstDistrict = JsonConvert.DeserializeObject<List<DistrictViewModel>>(responseData);
                    jsonResult = Json(new { success = true, lstData = lstDistrict }, JsonRequestBehavior.AllowGet);
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
        [OutputCache( Duration = int.MaxValue)]
        public JsonResult GetAllWard()
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/management/getallward").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var lstward = JsonConvert.DeserializeObject<List<WardViewModel>>(responseData);
                    jsonResult = Json(new { success = true, lstData = lstward }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetAllRoom(int page = 0, int pageSize = 20)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/room/getall/?page=" + page + "&&pageSize=" + pageSize + "").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var lstroom = JsonConvert.DeserializeObject<PaginationSet<RoomViewModel>>(responseData);
                    jsonResult = Json(new { success = true, lstData = lstroom }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetAllRoomByUserID(int page = 0, int pageSize = 20)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                var userID = User.Identity.GetUserId();
                if (userID != null)
                {
                    HttpResponseMessage responseMessage = client.GetAsync(url + "/room/getallbyuserid/?userID=" + userID + "&&page=" + page + "&&pageSize=" + pageSize + "").Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var lstroom = JsonConvert.DeserializeObject<PaginationSet<RoomViewModel>>(responseData);
                        jsonResult = Json(new { success = true, lstData = lstroom }, JsonRequestBehavior.AllowGet);
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
            }
            else
            {
                jsonResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
        public JsonResult GetAllRoomFullSearch(SearchRoomViewModels filter, int page = 0, int pageSize = 20)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage =
                    client.GetAsync(url + "/room/getallroomfullsearch/?page=" + page + "&pageSize=" + pageSize +
                    "&PriceFrom=" + filter.PriceFrom + "&PriceTo=" + filter.PriceTo +
                    "&RoomTypeID=" + filter.RoomTypeID + "&Keywords=" + filter.Keywords +
                    "&WardID=" + filter.WardID + "&DistrictID=" + filter.DistrictID + "&ProvinceID=" + filter.ProvinceID + "").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var lstroom = JsonConvert.DeserializeObject<PaginationSet<RoomViewModel>>(responseData);
                    jsonResult = Json(new { success = true, lstData = lstroom }, JsonRequestBehavior.AllowGet);
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
        public JsonResult CreateRoom(RoomViewModel rooms)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                rooms.UserID = User.Identity.GetUserId();
                var json = new JavaScriptSerializer().Serialize(rooms);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = client.PostAsync(url + "/room/addroom", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var room = JsonConvert.DeserializeObject<RoomViewModel>(responseData);
                    jsonResult = Json(new { success = true, objData = room }, JsonRequestBehavior.AllowGet);
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
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.ServerAndClient, Duration = int.MaxValue, VaryByParam = "id")]
        public JsonResult GetRoomByID(int id)
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/room/getroombyid/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var ObjRoom = JsonConvert.DeserializeObject<RoomViewModel>(responseData);
                    jsonResult = Json(new { success = true, Objdata = ObjRoom }, JsonRequestBehavior.AllowGet);
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
        #endregion
        #region RoomType
        public JsonResult GetAllRoomType()
        {
            JsonResult jsonResult = new JsonResult();
            HttpRequestBase request = this.HttpContext.Request;
            if (ValidateRequestHeader(request))
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/management/getallroomtype").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var lstRoomType = JsonConvert.DeserializeObject<List<RoomTypeViewModel>>(responseData);
                    jsonResult = Json(new { success = true, lstData = lstRoomType }, JsonRequestBehavior.AllowGet);
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
        #endregion
    }
}