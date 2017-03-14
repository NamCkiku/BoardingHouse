using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Service.IService
{
    public interface IErrorService
    {
        Error Create(Error error);
        void SaveChanges();
    }
}
