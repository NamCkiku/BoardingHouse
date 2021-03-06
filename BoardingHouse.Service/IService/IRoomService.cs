﻿using BoardingHouse.Entities.Entities;
using BoardingHouse.Entities.Models;
using BoardingHouse.Entities.SearchEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface IRoomService
    {
        Room Add(RoomEntity info);

        void Update(Room info);

        Room Delete(int id);
        IEnumerable<RoomEntity> GetAllRoomFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow);
        IEnumerable<RoomEntity> GetAllPaging(SearchEntity filter, int page, int pageSize, out int totalRow);
        IEnumerable<RoomEntity> GetAllListRoom(int page, int pageSize, out int totalRow);
        IEnumerable<RoomEntity> GetAllListRoomByUser(string userID, int page, int pageSize, out int totalRow);
        IEnumerable<Room> GetAll();

        Room GetById(int id);
        RoomEntity GetRoomById(int id);

        bool ChangeStatus(int id);

        void SaveChanges();
    }
}
