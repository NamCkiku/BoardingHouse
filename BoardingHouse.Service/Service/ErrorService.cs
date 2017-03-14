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
    public class ErrorService : IErrorService
    {
        IErrorRepository _errorRepository;
        IUnitOfWork _unitOfWork;
        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }
        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
