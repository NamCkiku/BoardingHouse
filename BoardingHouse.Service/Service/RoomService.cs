﻿using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Repositoty.Infrastructure;
using BoardingHouse.Entities.Entities;
using BoardingHouse.Entities.SearchEntity;

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
                        FullName = info.FullName,
                        Email = info.Email,
                        UserID = info.UserID,
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
        public IEnumerable<RoomEntity> GetAllRoomFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow)
        {
            List<RoomEntity> lstroom = new List<RoomEntity>();
            if (filter.Keywords == null)
            {
                filter.Keywords = "";
            }
            try
            {
                lstroom = _roomRepository.GetAllListRoom().Where(x => x.Status == false
                && (x.RoomTypeID == filter.RoomTypeID || filter.RoomTypeID == null)
                && (x.Price >= filter.PriceFrom || filter.PriceFrom == null)
                && (x.Price <= filter.PriceTo || filter.PriceTo == null)
                && (x.ProvinceID == filter.ProvinceID || filter.ProvinceID == null)
                && (x.DistrictID == filter.DistrictID || filter.DistrictID == null)
                && (x.WardID == filter.WardID || filter.WardID == null)
                && (x.RoomName.Contains(filter.Keywords) || string.IsNullOrEmpty(filter.Keywords))
                ).OrderByDescending(x => x.CreateDate).ToList();
                totalRow = lstroom.Count();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("AddRoom('{0}')", lstroom);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                totalRow = 0;
                return null;
            }
            return lstroom.Skip((page) * pageSize).Take(pageSize);
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
                lstroom = _roomRepository.GetAllListRoom().Where(x => x.Status == filter.Status
                && (filter.RoomType == null || x.RoomTypeID == filter.RoomType)
                && (filter.Province == null || x.ProvinceID == filter.Province)
                && (filter.District == null || x.DistrictID == filter.District)
                && (filter.Ward == null || x.WardID == filter.Ward)
                && (filter.StartDate == null || x.CreateDate >= st || x.CreateDate == null)
                && (filter.EndDate == null || x.CreateDate < et || x.CreateDate == null)
                && (x.RoomName.Contains(filter.Keywords) || x.Description.Contains(filter.Keywords) || string.IsNullOrEmpty(filter.Keywords))
                ).OrderByDescending(x => x.CreateDate).ToList();
                totalRow = lstroom.Count();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllPaging('{0}')", lstroom);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                totalRow = 0;
                return null;
            }
            return lstroom.Skip((page) * pageSize).Take(pageSize);
        }
        public IEnumerable<RoomEntity> GetAllListRoom(int page, int pageSize, out int totalRow)
        {
            List<RoomEntity> lstroom = new List<RoomEntity>();
            try
            {
                lstroom = _roomRepository.GetAllListRoom().Where(x => x.Status == false).OrderByDescending(x => x.CreateDate)
                    .ToList();
                totalRow = lstroom.Count();
                if (lstroom != null)
                {
                    lstroom.Skip(page * pageSize).Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoom('{0}')", "");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                throw ex;
            }
            return lstroom.Skip(page * pageSize).Take(pageSize);
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
                    lstroom.Skip(page * pageSize).Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoomByUser('{0}')", "");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                throw ex;
            }
            return lstroom;
        }
        public Room GetById(int id)
        {
            var room = new Room();
            if (id > 0)
            {
                room = _roomRepository.GetSingleByCondition(x => x.RoomID == id);
            }
            return room;
        }

        public RoomEntity GetRoomById(int id)
        {
            var room = new RoomEntity();
            if (id > 0)
            {
                room = _roomRepository.GetAllListRoom().Where(x => x.RoomID == id).FirstOrDefault();
            }
            return room;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
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
