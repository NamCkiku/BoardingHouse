using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
    }
    public class RoomTypeRepository : RepositoryBase<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
