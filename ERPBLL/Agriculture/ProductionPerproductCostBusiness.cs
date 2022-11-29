using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{



    public class ProductionPerproductCostBusiness: IProductionPerproductCost
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ProductionPerproductCostRepository _productionPerproductCostRepository;

        public ProductionPerproductCostBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._productionPerproductCostRepository = new ProductionPerproductCostRepository(this._agricultureUnitOfWork);
        }


    }
}
