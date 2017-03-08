using BoardingHouse.Repositoty.Infrastructure;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardingHouse.Entities.Entities;
using BoardingHouse.Entities.Models;

namespace BoardingHouse.Service.Service
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork)
        {
            this._roomTypeRepository = roomTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public RoomType Add(RoomType info)
        {
            RoomType addroom = new RoomType();
            try
            {
                if (info != null)
                {
                    addroom = _roomTypeRepository.Add(info);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("AddRoomtype('{0}')", info);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return addroom;
        }


        public RoomType Delete(int id)
        {
            RoomType room = new RoomType();
            try
            {
                room = _roomTypeRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("DeleteRoomtype('{0}')", id);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return room;
        }

        public IEnumerable<RoomType> GetAll()
        {
            List<RoomType> lstroomType = new List<RoomType>();
            try
            {
                lstroomType = _roomTypeRepository.GetAll().Where(x => x.Status == true)
                    .ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetRoomtype('{0}')");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return lstroomType;
        }

        public RoomType GetById(int id)
        {
            RoomType room = new RoomType();
            try
            {
                room = _roomTypeRepository.GetSingleById(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("DeleteRoomtype('{0}')", id);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return room;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(RoomType info)
        {
            try
            {
                if (info != null)
                {
                    _roomTypeRepository.Update(info);
                }

            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("UpdateRoomtype('{0}')", info);
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
            }

        }
    }
}
