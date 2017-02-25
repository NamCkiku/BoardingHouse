using BoardingHouse.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardingHouse.Repositoty.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private DbContexEntities dbContext;

        public DbContexEntities Init()
        {
            return dbContext ?? (dbContext = new DbContexEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
