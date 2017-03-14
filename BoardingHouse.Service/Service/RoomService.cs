using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Repositoty.Infrastructure;
using BoardingHouse.Entities.Entities;

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

        public IEnumerable<RoomEntity> GetAllPaging(SearchEntity filter, int page, int pageSize, out int totalRow)
        {
            DateTime st = filter.StartDate == null ? DateTime.MinValue : filter.StartDate.Value.Date;
            DateTime et = filter.EndDate == null ? DateTime.MaxValue : filter.EndDate.Value.Date.AddDays(1);
            if (filter.Keywords == null)
            {
                filter.Keywords = "";
            }
            List<RoomEntity> lstroom = new List<RoomEntity>();
            try
            {
                lstroom = _roomRepository.GetMulti(x => x.Status == filter.Status
                && ((filter.StartDate == null || x.CreateDate >= st || x.CreateDate == null))
                && ((filter.EndDate == null || x.CreateDate < et || x.CreateDate == null))
                && (x.RoomName.Contains(filter.Keywords) || x.Description.Contains(filter.Keywords) || string.IsNullOrEmpty(filter.Keywords))
                ).Select(x => new RoomEntity
                {
                    RoomID = x.RoomID,
                    RoomName = x.RoomName,
                    Address = x.Address,
                    Price = x.Price,
                    Acreage = x.Acreage,
                    Content = x.Content,
                    CreateDate = x.CreateDate
                }).ToList();
                totalRow = lstroom.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstroom.Skip((page - 1) * pageSize).Take(pageSize);
        }
        public IEnumerable<RoomEntity> GetAllListRoom()
        {
            List<RoomEntity> lstroom = new List<RoomEntity>();
            try
            {
                lstroom = _roomRepository.GetAllListRoom().Where(x => x.Status == true)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstroom;
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

        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }
    }
}
