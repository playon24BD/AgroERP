using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IProductPriceConfiguration
    {
        bool SaveProductPrice(List<ProductPriceConfigurationDTO> infoDTO, long userId);

        IEnumerable<ProductPriceConfigurationDTO> GetAllproductPRicelist (string name);

        bool UpdateProductPrice(ProductPriceConfigurationDTO updateDTOs, long userId);

        ProductPriceConfiguration GetProductPriceId(long ProductPriceConfigurationId);



        ProductPriceConfiguration GetPriceByproandfgrId(long pid, long rid);

        IEnumerable<ProductPriceConfiguration> GetexistproductPRicelist(long pid, long rid);
    }
}
