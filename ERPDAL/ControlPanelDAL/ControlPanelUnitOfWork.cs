using ERPDAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPDAL.ControlPanelDAL
{
    public class ControlPanelUnitOfWork : IControlPanelUnitOfWork
    {
        private readonly ControlPanelDbContext _dbcontext;
        public ControlPanelUnitOfWork() {
            _dbcontext = new ControlPanelDbContext();
        }
        public DbContext Db { get { return _dbcontext; } }

        public void Dispose()
        {
            
        }
    }
}
