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
    [RoutePrefix("api/room")]
    public class RoomController : ApiControllerBase
    {
        IRoomService _roomService;
        public RoomController(IErrorService errorService, IRoomService roomService) : base(errorService)
        {
            this._roomService = roomService;
        }
        [Route("getall")]
        public HttpResponseMessage GetAllListInfoJoin(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listRoom = _roomService.GetAll().OrderByDescending(x => x.CreateDate).Take(9).ToList();

                var listRoomVm = Mapper.Map<List<RoomViewModel>>(listRoom);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listRoomVm);

                return response;
            });
        }
    }
}
