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
    public class ProductPricingHistoryBusiness : IProductPricingHistory
    {

        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ProductPriceConfigurationRepository _productPriceConfigurationRepository;
        private readonly ProductPricingHistoryRepository _productPricingHistoryRepository;


        public ProductPricingHistoryBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._productPriceConfigurationRepository = new ProductPriceConfigurationRepository(this._agricultureUnitOfWork);
            this._productPricingHistoryRepository = new ProductPricingHistoryRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<ProductPricingHistory> GetPriceByproANDfgrId(long pid, long rid)
        {
            try
            {
                return _productPricingHistoryRepository.GetAll(h => h.FinishGoodProductId == pid && h.FGRId == rid).ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
