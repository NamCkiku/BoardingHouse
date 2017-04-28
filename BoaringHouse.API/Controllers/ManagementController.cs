using AutoMapper;
using BoardingHouse.Service.IService;
using BoaringHouse.API.Infrastructure.Core;
using BoaringHouse.API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BoaringHouse.API.Controllers
{
    [RoutePrefix("api/management")]
    public class ManagementController : ApiControllerBase
    {
        IRoomTypeService _roomTypeService;
        ICommonService _commonService;
        public ManagementController(IErrorService errorService, IRoomTypeService roomTypeService, ICommonService commonService) : base(errorService)
        {
            this._roomTypeService = roomTypeService;
            this._commonService = commonService;
        }

        [Route("getallroomtype")]
        public HttpResponseMessage GetAllRoomType(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listRoomType = _roomTypeService.GetAll().OrderByDescending(x => x.CreatedDate).ToList();

                var listRoomTypeVm = Mapper.Map<List<RoomTypeViewModel>>(listRoomType);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listRoomTypeVm);

                return response;
            });
        }
        [Route("getallprovince")]
        public HttpResponseMessage GetAllProvince(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listprovince = _commonService.GetAllProvince().OrderByDescending(x => x.name).ToList();

                var listprovinceVm = Mapper.Map<List<ProvinceViewModel>>(listprovince);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listprovinceVm);

                return response;
            });
        }
        [Route("getalldistrict")]
        public HttpResponseMessage GetAllDistrict(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listdistrict = _commonService.GetAllDistrict().OrderByDescending(x => x.name).ToList();

                var listdistrictVm = Mapper.Map<List<DistrictViewModel>>(listdistrict);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listdistrictVm);

                return response;
            });
        }
        [Route("getallward")]
        public HttpResponseMessage GetAllWard(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listward = _commonService.GetAllWard().OrderByDescending(x => x.name).ToList();

                var listwardVm = Mapper.Map<List<WardViewModel>>(listward);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listwardVm);

                return response;
            });
        }


        [Route("images/uploadFile")]
        public async Task UploadSingleFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, new NotSupportedException("Media type not supported"));
            }
            var root = HttpContext.Current.Server.MapPath("~/Content/images");
            var dataFolder = HttpContext.Current.Server.MapPath("~/Content/images");
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            string fileName = string.Empty;
            foreach (MultipartFileData fileData in provider.FileData)
            {
                fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = result.FormData["model"] + "_"
                              + Path.GetFileName(fileName);
                }
                if (File.Exists(Path.Combine(dataFolder, fileName)))
                    File.Delete(Path.Combine(dataFolder, fileName));
                File.Move(fileData.LocalFileName, Path.Combine(dataFolder, fileName));
                File.Delete(fileData.LocalFileName);
            }

            Request.CreateResponse(HttpStatusCode.OK, new { fileName = fileName });
        }
    }
}
