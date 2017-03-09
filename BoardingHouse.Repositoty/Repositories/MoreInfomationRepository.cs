using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface IMoreInfomationRepository : IRepository<MoreInfomation>
    {
    }
    public class MoreInfomationRepository : RepositoryBase<MoreInfomation>, IMoreInfomationRepository
    {
        public MoreInfomationRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
