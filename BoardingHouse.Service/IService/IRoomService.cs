using BoardingHouse.Entities.Entities;
using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface IRoomService
    {
        Room Add(Room info);

        void Update(Room info);

        Room Delete(int id);
        IEnumerable<RoomEntity> GetAll(SearchEntity filter, int page, int pageSize, out int totalRow);

        Room GetById(int id);

        bool ChangeStatus(int id);

        void SaveChanges();
    }
}
