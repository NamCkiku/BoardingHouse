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
        private readonly IMoreInfomationRepository _moreInfomationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IRoomRepository roomRepository, IMoreInfomationRepository moreInfomationRepository, IUnitOfWork unitOfWork)
        {
            this._roomRepository = roomRepository;
            this._moreInfomationRepository = moreInfomationRepository;
            this._unitOfWork = unitOfWork;
        }
        public Room Add(RoomEntity info)
        {
            Room resultRoom = new Room();
            try
            {
                if (info != null)
                {
                    var room = new Room
                    {
                        RoomName = info.RoomName,
                        Alias = info.Alias,
                        Phone = info.Phone,
                        Address = info.Address,
                        RoomTypeID = info.RoomTypeID,
                        Image = info.Image,
                        MoreImages = info.MoreImages,
                        WardID = info.WardID,
                        DistrictID = info.DistrictID,
                        ProvinceID = info.ProvinceID,
                        Acreage = info.Acreage,
                        Price = info.Price,
                        Lat = info.Lat,
                        Lng = info.Lng,
                        Description = info.Description,
                        Content = info.Content,
                        CreateDate = DateTime.Now,
                        Status = false,
                    };
                    if (info.MoreInfomations != null)
                    {
                        MoreInfomation modal = new MoreInfomation
                        {
                            BedroomNumber = info.MoreInfomations.BedroomNumber,
                            Compass = info.MoreInfomations.Compass,
                            Convenient = info.MoreInfomations.Convenient,
                            ElectricPrice = info.MoreInfomations.ElectricPrice,
                            FloorNumber = info.MoreInfomations.FloorNumber,
                            ToiletNumber = info.MoreInfomations.ToiletNumber,
                            WaterPrice = info.MoreInfomations.WaterPrice
                        };
                        var result = _moreInfomationRepository.Add(modal);
                        _unitOfWork.Commit();
                        room.MoreInfomationID = result.MoreInfomationID;
                    }
                    resultRoom = _roomRepository.Add(room);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("AddRoom('{0}')", info);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return resultRoom;
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
        public IEnumerable<RoomEntity> GetAllListRoom(int page, int pageSize, out int totalRow)
        {
            List<RoomEntity> lstroom = new List<RoomEntity>();
            try
            {
                lstroom = _roomRepository.GetAllListRoom().Where(x => x.Status == false)
                    .ToList();
                totalRow = lstroom.Count();
                if (lstroom != null)
                {
                    lstroom.Skip((page - 1) * pageSize).Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("AddRoom('{0}')", "");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                throw ex;
            }
            return lstroom.Skip((page - 1) * pageSize).Take(pageSize);
        }
        public IEnumerable<RoomEntity> GetAllListRoomByUser(string userID, int page, int pageSize, out int totalRow)
        {
            List<RoomEntity> lstroom = new List<RoomEntity>();
            try
            {
                lstroom = _roomRepository.GetAllListRoom().Where(x => x.UserID == userID)
                    .ToList();
                totalRow = lstroom.Count();
                if (lstroom != null)
                {
                    lstroom.Skip((page - 1) * pageSize).Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoomByUser('{0}')", "");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                throw ex;
            }
            return lstroom.Skip((page - 1) * pageSize).Take(pageSize);
        }
        public Room GetById(int id)
        {
            var room = new Room();
            if (id > 0)
            {
                room = _roomRepository.GetSingleById(id);
            }
            return room;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Room info)
        {
            _roomRepository.Update(info);
            _unitOfWork.Commit();
        }

        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }
    }
}
