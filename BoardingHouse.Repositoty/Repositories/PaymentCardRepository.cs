using BoardingHouse.Entities.Models;
using BoardingHouse.Repositoty.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Repositories
{
    public interface IPaymentCardRepository : IRepository<PaymentCard>
    {
    }
    public class PaymentCardRepository : RepositoryBase<PaymentCard>, IPaymentCardRepository
    {
        public PaymentCardRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
