using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DbContexEntities Init();
    }
}
