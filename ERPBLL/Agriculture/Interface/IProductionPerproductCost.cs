using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IProductionPerproductCost
    {
        bool SaveProductionPerproductCost(ProductionPerproductCostDTO productionPerproductCostDTO, long userId);

        ProductionPerproductCost GetProductionPerproductCostById(long FGRId);




    }
}
