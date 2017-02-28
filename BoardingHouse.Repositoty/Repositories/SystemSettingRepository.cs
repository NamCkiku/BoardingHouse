using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface ISystemSettingRepository : IRepository<SystemSetting>
    {
    }
    public class SystemSettingRepository : RepositoryBase<SystemSetting>, ISystemSettingRepository
    {
        public SystemSettingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
