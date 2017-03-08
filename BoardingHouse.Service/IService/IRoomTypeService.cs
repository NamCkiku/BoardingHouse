using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface IRoomTypeService
    {
        RoomType Add(RoomType info);

        void Update(RoomType info);

        RoomType Delete(int id);
        IEnumerable<RoomType> GetAll();

        RoomType GetById(int id);

        void SaveChanges();
    }
}
