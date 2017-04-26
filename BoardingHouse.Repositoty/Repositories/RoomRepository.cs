using BoardingHouse.Entities.Entities;
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
        IEnumerable<RoomEntity> GetAllListRoom();
    }
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<RoomEntity> GetAllListRoom()
        {
            var query = (from a in DbContext.Rooms
                         from b in DbContext.RoomTypes.Where(x => x.RoomTypeID == a.RoomTypeID)
                         from c in DbContext.Provinces.Where(x => x.provinceid == a.ProvinceID)
                         select new RoomEntity
                         {
                             RoomID = a.RoomID,
                             RoomName = a.RoomName,
                             RoomTypeName = b.RoomTypeName,
                             ProvinceName = c.name,
                             Price = a.Price,
                             Image = a.Image,
                             CreateDate = a.CreateDate,
                             Acreage = a.Acreage,
                             UserID = a.UserID,
                             Status = a.Status,
                             FullName = a.FullName,
                             Address = a.Address,
                             RoomTypeID = a.RoomTypeID,
                             ProvinceID = a.ProvinceID,
                             DistrictID = a.DistrictID,
                             WardID = a.WardID
                         }).ToList();
            return query;
        }
    }
}
