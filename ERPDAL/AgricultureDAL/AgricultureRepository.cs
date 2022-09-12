
using ERPBO.Agriculture.DomainModels;
using ERPBO.ControlPanel.DomainModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPDAL.AgricultureDAL
{
    public class DepotSetupRepository : AgricultureBaseRepository<DepotSetup>
    {
        public DepotSetupRepository(IAgricultureUnitOfWork AgricultureUnitOfWork) : base(AgricultureUnitOfWork) { }
    }
   
}
