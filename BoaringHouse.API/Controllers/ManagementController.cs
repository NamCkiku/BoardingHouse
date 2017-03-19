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
        [Route("getalldistrict/{id:int}")]
        public HttpResponseMessage GetAllDistrict(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var listdistrict = _commonService.GetAllDistrict(id).OrderByDescending(x => x.name).ToList();

                var listdistrictVm = Mapper.Map<List<DistrictViewModel>>(listdistrict);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listdistrictVm);

                return response;
            });
        }
        [Route("getallward/{id:int}")]
        public HttpResponseMessage GetAllWard(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var listward = _commonService.GetAllWard(id).OrderByDescending(x => x.name).ToList();

                var listwardVm = Mapper.Map<List<WardViewModel>>(listward);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listwardVm);

                return response;
            });
        }
    }
}
