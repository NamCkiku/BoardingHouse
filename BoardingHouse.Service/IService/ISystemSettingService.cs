using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface ISystemSettingService
    {
        bool Settings(string Key, string Value);
        string GetSettings(string Key);
        void SettingUpdate(string Key, string Value);
        void LoadSystemSettingConfiguration();
        void LoadDefaultValuesSetings();
    }
}
