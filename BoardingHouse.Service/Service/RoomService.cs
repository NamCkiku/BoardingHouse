using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Repositoty.Infrastructure;

namespace BoardingHouse.Service.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            this._roomRepository = roomRepository;
            this._unitOfWork = unitOfWork;
        }
        public Room Add(Room info)
        {
            throw new NotImplementedException();
        }

        public bool ChangeStatus(int id)
        {
            throw new NotImplementedException();
        }

        public Room Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }

        public Room GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Room info)
        {
            throw new NotImplementedException();
        }
    }
}
