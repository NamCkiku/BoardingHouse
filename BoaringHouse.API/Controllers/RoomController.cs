using AutoMapper;
using BoardingHouse.Entities.Entities;
using BoardingHouse.Service.IService;
using BoaringHouse.API.Infrastructure.Core;
using BoaringHouse.API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page=0, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listRoom = _roomService.GetAllListRoom(page, pageSize, out totalRow).OrderByDescending(x => x.CreateDate);
                var listRoomVm = Mapper.Map<List<RoomViewModel>>(listRoom);
                var paginationSet = new PaginationSet<RoomViewModel>()
                {
                    Items = listRoomVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }
        [Route("addroom")]
        [HttpPost]
        public HttpResponseMessage AddRoom(HttpRequestMessage request, RoomEntity roomEntity)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    if (roomEntity != null)
                    {
                        var room = _roomService.Add(roomEntity);
                        response = request.CreateResponse(HttpStatusCode.Created, room);
                    }
                    else
                    {
                        request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
                return response;
            });
        }
        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var result = _roomService.GetById(id);
                if (result == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
                else
                {
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images");

                    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                    // Read the MIME multipart asynchronously 
                    Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                    string _localFileName = multipartFormDataStreamProvider
                        .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                    // Create response
                    FileUploadResult fileUploadResult = new FileUploadResult();
                    fileUploadResult.LocalFilePath = _localFileName;

                    fileUploadResult.FileName = Path.GetFileName(_localFileName);

                    //fileUploadResult.FileLength = new FileInfo(_localFileName).Length;

                    // update database
                    result.Image = fileUploadResult.FileName;
                    _roomService.Update(result);
                    response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
                }

                return response;
            });
        }
    }
}
