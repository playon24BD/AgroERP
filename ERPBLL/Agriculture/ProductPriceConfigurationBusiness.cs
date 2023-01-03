using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class ProductPriceConfigurationBusiness : IProductPriceConfiguration
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ProductPriceConfigurationRepository _productPriceConfigurationRepository;
        private readonly ProductPricingHistoryRepository _productPricingHistoryRepository;


        public ProductPriceConfigurationBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._productPriceConfigurationRepository = new ProductPriceConfigurationRepository(this._agricultureUnitOfWork);
            this._productPricingHistoryRepository = new ProductPricingHistoryRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<ProductPriceConfigurationDTO> GetAllproductPRicelist(string name)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<ProductPriceConfigurationDTO>(QueryforProductpricelist(name)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryforProductpricelist(string name)
        {
            try
            {

                string query = string.Empty;
                string param = string.Empty;
                if (name != null && name != "")
                {
                    param += string.Format(@" and fn.FinishGoodProductName like '%{0}%'", name);
                }
                query = string.Format(@"select p.UpdateDate,p.UpdateUser,fn.FinishGoodProductName,fn.FinishGoodProductId,p.FGRId,p.ProductPriceConfigurationId,p.ProductPrice,p.Flag,p.EntryDate,p.EntryUser from tblProductPriceConfiguration p
inner join tblFinishGoodProductInfo fn on p.FinishGoodProductId=fn.FinishGoodProductId
Where 1=1 {0} order by p.ProductPriceConfigurationId desc", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }




        public bool SaveProductPrice(List<ProductPriceConfigurationDTO> infoDTO, long userId)
        {
            bool isSuccess = false;

            List<ProductPriceConfiguration> productPriceConfigurations = new List<ProductPriceConfiguration>();
            List<ProductPricingHistory> ProductPricingHistorylist = new List<ProductPricingHistory>();

            foreach (var item in infoDTO)
            {
                var pp = GetexistproductPRicelist(item.FinishGoodProductId, item.FGRId).ToList();
                if (pp.Count == 0)
                {

                        ProductPriceConfiguration productPriceConfiguration = new ProductPriceConfiguration();
                        {
                            productPriceConfiguration.FinishGoodProductId = item.FinishGoodProductId;
                            productPriceConfiguration.FGRId = item.FGRId;
                            productPriceConfiguration.ProductPrice = item.ProductPrice;
                            productPriceConfiguration.EntryDate = DateTime.Now;
                            productPriceConfiguration.EntryUser = userId;
                            productPriceConfiguration.Flag = item.Flag;
                        };
                    //_productPriceConfigurationRepository.Insert(productPriceConfiguration);
                    productPriceConfigurations.Add(productPriceConfiguration);



                ProductPricingHistory productPricingHistory1 = new ProductPricingHistory();
                    {
                        productPricingHistory1.ProductPrice = item.ProductPrice;
                        productPricingHistory1.FinishGoodProductId = item.FinishGoodProductId;
                        productPricingHistory1.FGRId = item.FGRId;
                        productPricingHistory1.Flag = item.Flag;
                        productPricingHistory1.EntryDate = DateTime.Now;
                        productPricingHistory1.EntryUser = userId;
                    }
                    // _productPricingHistoryRepository.Insert(productPricingHistory1);

                    ProductPricingHistorylist.Add(productPricingHistory1);
                }


             }
            _productPriceConfigurationRepository.InsertAll(productPriceConfigurations);
            isSuccess = _productPriceConfigurationRepository.Save();

            _productPricingHistoryRepository.InsertAll(ProductPricingHistorylist);
            isSuccess = _productPricingHistoryRepository.Save();


            return isSuccess;
        }

        public bool UpdateProductPrice(ProductPriceConfigurationDTO updateDTOs, long userId)
        {
            bool isSuccess = false;

            if (updateDTOs != null)
            {
                ProductPriceConfiguration productPriceConfiguration = new ProductPriceConfiguration();
                productPriceConfiguration = GetProductPriceId(updateDTOs.ProductPriceConfigurationId);
                productPriceConfiguration.ProductPrice = updateDTOs.ProductPrice;
                productPriceConfiguration.UpdateDate = DateTime.Now;
                productPriceConfiguration.UpdateUser = userId;
                _productPriceConfigurationRepository.Update(productPriceConfiguration);
                isSuccess = _productPriceConfigurationRepository.Save();
            }
            if (updateDTOs != null)
            {
                ProductPricingHistory productPricingHistory = new ProductPricingHistory();
                productPricingHistory.FinishGoodProductId = updateDTOs.FinishGoodProductId;
                productPricingHistory.FGRId = updateDTOs.FGRId;
                productPricingHistory.ProductPrice = updateDTOs.ProductPrice;
                productPricingHistory.EntryDate = DateTime.Now;
                productPricingHistory.EntryUser = userId;
                productPricingHistory.Flag = updateDTOs.Flag;
                _productPricingHistoryRepository.Insert(productPricingHistory);
                isSuccess = _productPricingHistoryRepository.Save();

            }

            return isSuccess;
        }

        public ProductPriceConfiguration GetProductPriceId(long ProductPriceConfigurationId)
        {
            return _productPriceConfigurationRepository.GetOneByOrg(d => d.ProductPriceConfigurationId == ProductPriceConfigurationId);

        }

        public ProductPriceConfiguration GetPriceByproandfgrId(long pid, long rid)
        {
            return _productPriceConfigurationRepository.GetOneByOrg(t => t.FinishGoodProductId == pid && t.FGRId == rid);
        }

        public IEnumerable<ProductPriceConfiguration> GetexistproductPRicelist(long pid, long rid)
        {
            return _productPriceConfigurationRepository.GetAll(g=>g.FinishGoodProductId==pid && g.FGRId==rid);
        }
    }
}
