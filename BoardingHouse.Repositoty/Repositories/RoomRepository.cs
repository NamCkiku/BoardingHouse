using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
    }
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
