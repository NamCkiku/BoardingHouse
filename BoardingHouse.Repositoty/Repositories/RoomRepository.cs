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
                         from d in DbContext.AspNetUsers.Where(x => x.Id == a.UserID).DefaultIfEmpty()
                         from e in DbContext.MoreInfomations.Where(x => x.MoreInfomationID == a.MoreInfomationID).DefaultIfEmpty()
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
                             Address = a.Address,
                             RoomTypeID = a.RoomTypeID,
                             ProvinceID = a.ProvinceID,
                             DistrictID = a.DistrictID,
                             Phone=a.Phone,
                             WardID = a.WardID,
                             UserAvatar = d.Avatar,
                             FullName = d.UserName,
                             Lat = a.Lat,
                             Lng = a.Lng,
                             FloorNumber = e.FloorNumber,
                             ToiletNumber = e.ToiletNumber,
                             WaterPrice = e.WaterPrice,
                             BedroomNumber = e.BedroomNumber,
                             ElectricPrice = e.ElectricPrice,
                             Convenient = e.Convenient,
                             Compass = e.Compass,
                             ViewCount = a.ViewCount,
                             Content = a.Content,
                             MoreImages = a.MoreImages,
                             Description=a.Description,
                         }).ToList();
            return query;
        }
    }
}
