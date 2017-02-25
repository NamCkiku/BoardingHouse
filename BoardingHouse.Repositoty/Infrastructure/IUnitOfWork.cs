using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
