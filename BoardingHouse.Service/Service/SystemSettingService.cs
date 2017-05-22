using BoardingHouse.Common.CommonConstants;
using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using BoardingHouse.Repositoty.Repositories;
using BoardingHouse.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.Service
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SystemSettingService(ISystemSettingRepository systemSettingRepository, IUnitOfWork unitOfWork)
        {
            this._systemSettingRepository = systemSettingRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool Settings(string Key, string Value)
        {
            bool flag = false;
            try
            {
                SystemSetting setting = new SystemSetting();
                setting.Field = Key;
                setting.Value = Value;
                //Get Connection string from db
                var iResult = _systemSettingRepository.Add(setting);
                _unitOfWork.Commit();
                if (iResult != null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return flag;
        }
        public void SettingUpdate(string Key, string Value)
        {
            try
            {
                var setting = _systemSettingRepository.GetSingleByCondition(x => x.Field == Key);
                setting.Field = Key;
                setting.Value = Value;
                //Get Connection string from db
                _systemSettingRepository.Update(setting);
                _unitOfWork.Commit();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public string GetSettings(string Key)
        {
            string Value = string.Empty;
            try
            {
                //Get Connection string from db
                var obj = _systemSettingRepository.GetSingleByCondition(x => x.Field == Key);
                if (obj != null)
                {
                    Value = obj.Value.ToString();
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return Value;
        }
        public void LoadDefaultValuesSetings()
        {
            if (string.IsNullOrEmpty(GetSettings("DateFormat")))
            {
                Settings("DateFormat", "yyyy MM dd, ddd");
            }
        }
        public void LoadSystemSettingConfiguration()
        {
            Var.sDateFormat = GetSettings("DateFormat");
        }
    }
}
