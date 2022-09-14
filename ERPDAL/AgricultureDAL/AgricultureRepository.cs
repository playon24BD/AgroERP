
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
    public class BankSetupRepository : AgricultureBaseRepository<BankSetup>
    {
        public BankSetupRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class DepotSetupRepository : AgricultureBaseRepository<DepotSetup>
    {
        public DepotSetupRepository(IAgricultureUnitOfWork AgricultureUnitOfWork) : base(AgricultureUnitOfWork) { }


    }

    public class RawMaterialRepository : AgricultureBaseRepository<RawMaterial>
    {
        public RawMaterialRepository(IAgricultureUnitOfWork agricultureUnitOfWork):base(agricultureUnitOfWork)
        {

        }

    }

    public class FinishGoodProductRepository : AgricultureBaseRepository<FinishGoodProduct>
    {
        public FinishGoodProductRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }

}
