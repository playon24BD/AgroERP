using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBO.Agriculture.DomainModels;
using ERPBO.ControlPanel.DomainModels;

namespace ERPDAL.AgricultureDAL
{
    public class AgricultureDbContext : DbContext
    {
        public AgricultureDbContext():base("Agriculture")
        {

        }
        public DbSet<DepotSetup> tblDepotInfo { get; set; }
      
    }
}
