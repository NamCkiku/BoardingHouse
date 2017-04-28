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
    public class CommonService : ICommonService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IAuditLogRepository _audilogRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CommonService(IProvinceRepository roomRepository, IDistrictRepository districtRepository, IWardRepository wardRepository, IUnitOfWork unitOfWork,
            IAuditLogRepository audilogRepository)
        {
            this._provinceRepository = roomRepository;
            this._districtRepository = districtRepository;
            this._wardRepository = wardRepository;
            this._audilogRepository = audilogRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<District> GetAllDistrict()
        {
            List<District> lstdistrict = new List<District>();
            try
            {
                lstdistrict = _districtRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllDistrict('{0}')");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return lstdistrict;
        }

        public IEnumerable<Province> GetAllProvince()
        {
            List<Province> lstprovince = new List<Province>();
            try
            {
                lstprovince = _provinceRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllProvince('{0}')");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return lstprovince;
        }

        public IEnumerable<Ward> GetAllWard()
        {
            List<Ward> lstward = new List<Ward>();
            try
            {
                lstward = _wardRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllWard('{0}')");
                Common.Logs.LogCommon.WriteError(ex.ToString(), FunctionName);
                return null;
            }
            return lstward;
        }

        public void ApptLog(AuditLog obj)
        {
            try
            {
                AuditLog oAuditLog = new AuditLog();
                oAuditLog.CreatedBy = obj.CreatedBy;
                oAuditLog.Description = obj.Description;
                oAuditLog.CreatedDate = DateTime.Now;
                oAuditLog.Device = obj.Device;
                oAuditLog.IPAddress = obj.IPAddress;
                oAuditLog.LogType = obj.LogType;
                oAuditLog.UserID = obj.UserID;
                _audilogRepository.Add(oAuditLog);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
