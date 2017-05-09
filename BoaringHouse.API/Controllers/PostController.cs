using AutoMapper;
using BoardingHouse.Entities.Entities;
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
    [RoutePrefix("api/post")]
    public class PostController : ApiControllerBase
    {
        IPostService _postService;
        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._postService = postService;
        }
        [Route("getallpaging")]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listpost = _postService.GetAllPostPaging(page, pageSize, out totalRow).OrderByDescending(x => x.CreateDate);
                var listPostVm = Mapper.Map<List<PostViewModel>>(listpost);
                var paginationSet = new PaginationSet<PostViewModel>()
                {
                    Items = listPostVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }
        [Route("getpostbyid/{id:int}")]
        public HttpResponseMessage GetPostByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var post = _postService.GetById(id);
                var postVm = Mapper.Map<PostViewModel>(post);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, postVm);
                return response;
            });
        }

        [Route("addpost")]
        [HttpPost]
        public HttpResponseMessage AddRoom(HttpRequestMessage request, PostEntity postEntity)
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
                    if (postEntity != null)
                    {
                        var room = _postService.Add(postEntity);
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


        [Route("updatepost")]
        [HttpPut]
        public HttpResponseMessage UpdateRoom(HttpRequestMessage request, PostEntity postEntity)
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
                    if (postEntity != null)
                    {
                        _postService.Update(postEntity);
                        response = request.CreateResponse(HttpStatusCode.Created);
                    }
                    else
                    {
                        request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
                return response;
            });
        }



        [Route("deletepost")]
        [HttpDelete]
        public HttpResponseMessage DeleteRoom(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var room = _postService.Delete(id);
                response = request.CreateResponse(HttpStatusCode.Created, room);
                return response;
            });
        }
    }
}
