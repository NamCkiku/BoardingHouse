using BoaringHouse.Web.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

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
    }
}