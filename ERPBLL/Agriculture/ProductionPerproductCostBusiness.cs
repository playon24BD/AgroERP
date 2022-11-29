using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
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

        public ProductionPerproductCost GetProductionPerproductCostById(long FGRId)
        {
            return _productionPerproductCostRepository.GetOneByOrg(t=>t.FGRId== FGRId);
        }



        public bool SaveProductionPerproductCost(ProductionPerproductCostDTO productionPerproductCostDTO, long userId)
        {
            bool IsSuccess = false;
            ProductionPerproductCost productionPerproductCost = new ProductionPerproductCost
            {
                FinishGoodProductId = productionPerproductCostDTO.FinishGoodProductId,
                FGRId = productionPerproductCostDTO.FGRId,
                PerProductRMtotalCost = productionPerproductCostDTO.PerProductRMtotalCost,
                PerProductOtherCost = productionPerproductCostDTO.PerProductOtherCost,
                PerProductMainCost = productionPerproductCostDTO.PerProductRMtotalCost + productionPerproductCostDTO.PerProductOtherCost,
                EntryDate = DateTime.Now,
                EntryUser=userId

            };
            _productionPerproductCostRepository.Insert(productionPerproductCost);
            IsSuccess = _productionPerproductCostRepository.Save();
            return IsSuccess;
        }
    }
}
