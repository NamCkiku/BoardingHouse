using BoardingHouse.Entities.Entities;
using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface IPostService
    {
        PostEntity Add(PostEntity postEntity);

        void Update(PostEntity postEntity);

        bool Delete(int id);
        IEnumerable<PostEntity> GetAllPostPaging(int page, int pageSize, out int totalRow);

        Post GetById(int id);

        void SaveChanges();
    }
}
