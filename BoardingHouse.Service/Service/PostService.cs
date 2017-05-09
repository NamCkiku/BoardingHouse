using BoardingHouse.Repositoty.Infrastructure;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardingHouse.Entities.Entities;
using BoardingHouse.Entities.Models;

namespace BoardingHouse.Service.Service
{
    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        IUnitOfWork _unitOfWork;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        public PostEntity Add(PostEntity postEntity)
        {
            try
            {
                if (postEntity != null)
                {
                    var post = new Post();
                    post.Name = postEntity.Name;
                    post.Alias = postEntity.Alias;
                    post.Content = postEntity.Content;
                    post.Description = postEntity.Description;
                    post.ExpireDate = postEntity.ExpireDate;
                    post.HomeFlag = postEntity.HomeFlag;
                    post.HotFlag = postEntity.HotFlag;
                    post.Image = postEntity.Image;
                    post.MoreImages = postEntity.MoreImages;
                    post.PostTypeID = postEntity.PostTypeID;
                    post.Status = postEntity.Status;
                    post.Tags = postEntity.Tags;
                    _postRepository.Add(post);
                }
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("AddRoom('{0}')", postEntity);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }

            return postEntity;
        }

        public bool Delete(int id)
        {
            var result = false;
            try
            {
                var modal = _postRepository.Delete(id);
                if (modal != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("DeletePost('{0}')", id);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return false;
            }
            return result;
        }

        public IEnumerable<PostEntity> GetAllPostPaging(int page, int pageSize, out int totalRow)
        {
            var result = new List<PostEntity>();
            try
            {
                var modal = _postRepository.GetMulti(x => x.Status == true).Select(x => new PostEntity
                {
                    PostID = x.PostID,
                    Name = x.Name,
                    Alias = x.Alias,
                    Content = x.Content,
                    Description = x.Description,
                    ExpireDate = x.ExpireDate,
                    HomeFlag = x.HomeFlag,
                    HotFlag = x.HotFlag,
                    Image = x.Image,
                    MoreImages = x.MoreImages,
                    PostTypeID = x.PostTypeID,
                    Tags = x.Tags
                }).ToList();
                totalRow = modal.Count();
                if (modal != null)
                {
                    result = modal;
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("DeletePost('{0}')", null);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                totalRow = 0;
                return null;
            }
            return result.Skip((page) * pageSize).Take(pageSize);
        }

        public Post GetById(int id)
        {
            var result = new Post();
            try
            {
                var modal = _postRepository.GetSingleById(id);
                if (modal != null)
                {
                    result = modal;
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("DeletePost('{0}')", id);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return result;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostEntity postEntity)
        {
            try
            {
                if (postEntity != null)
                {
                    var post = _postRepository.GetSingleById(postEntity.PostID);
                    post.Name = postEntity.Name;
                    post.Alias = postEntity.Alias;
                    post.Content = postEntity.Content;
                    post.Description = postEntity.Description;
                    post.ExpireDate = postEntity.ExpireDate;
                    post.HomeFlag = postEntity.HomeFlag;
                    post.HotFlag = postEntity.HotFlag;
                    post.Image = postEntity.Image;
                    post.MoreImages = postEntity.MoreImages;
                    post.PostTypeID = postEntity.PostTypeID;
                    post.Status = postEntity.Status;
                    post.Tags = postEntity.Tags;
                    _postRepository.Update(post);
                }
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("AddRoom('{0}')", postEntity);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
            }
        }
    }
}
