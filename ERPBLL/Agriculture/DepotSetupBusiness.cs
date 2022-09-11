using ERPBLL.ControlPanel.Interface;
using ERPDAL.AgricultureDAL;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class DepotSetupBusiness:IDepotSetup
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly DepotSetupRepository _depotSetupRepository;
        
        public DepotSetupBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._depotSetupRepository = new DepotSetupRepository(this._AgricultureUnitOfWork);
        }
    }
}
