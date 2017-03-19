using AutoMapper;
using BoardingHouse.Service.IService;
using BoaringHouse.API.Infrastructure.Core;
using BoaringHouse.API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BoaringHouse.API.Controllers
{
    [RoutePrefix("api/management")]
    public class ManagementController : ApiControllerBase
    {
        IRoomTypeService _roomTypeService;
        public ManagementController(IErrorService errorService, IRoomTypeService roomTypeService) : base(errorService)
        {
            this._roomTypeService = roomTypeService;
        }

        [Route("getallroomtype")]
        public HttpResponseMessage GetAllListInfoJoin(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listRoomType = _roomTypeService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();

                var listRoomTypeVm = Mapper.Map<List<RoomTypeViewModel>>(listRoomType);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listRoomTypeVm);

                return response;
            });
        }
    }
}
