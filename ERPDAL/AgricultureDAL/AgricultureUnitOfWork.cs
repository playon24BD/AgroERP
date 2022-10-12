using ERPDAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPDAL.AgricultureDAL
{
    public class AgricultureUnitOfWork : IAgricultureUnitOfWork
    {
        private readonly AgricultureDbContext _dbcontext;
        public AgricultureUnitOfWork() {
            _dbcontext = new AgricultureDbContext();
        }
        public DbContext Db { get { return _dbcontext; } }

        public void Dispose()
        {
            
        }
    }
}
