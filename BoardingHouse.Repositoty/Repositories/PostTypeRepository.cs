using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface IPostTypeRepository : IRepository<PostType>
    {
    }
    public class PostTypeRepository : RepositoryBase<PostType>, IPostTypeRepository
    {
        public PostTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
