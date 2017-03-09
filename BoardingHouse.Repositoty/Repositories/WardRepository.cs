using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface IWardRepository : IRepository<Ward>
    {
    }
    public class WardRepository : RepositoryBase<Ward>, IWardRepository
    {
        public WardRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
