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
        IEnumerable<Room> GetAll();

        Room GetById(int id);

        bool ChangeStatus(int id);

        void SaveChanges();
    }
}
