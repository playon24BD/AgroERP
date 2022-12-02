using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;

namespace ERPBLL.Agriculture.Interface
{
    public interface ICommissionOnProductBusiness
    {
        IEnumerable<CommisionOnProduct> GetCommisionOnProducts(long orgId);
        IEnumerable<CommisionOnProductDTO> GetAllCommisionOnProducts(long? product, int? year, long orgId);
        CommisionOnProduct GetCommisionOnProductbyId(long commissionOnProductId,long orgId);
        CommisionOnProduct GetCommisionOByProductId(long finishGoodProductId, long orgId);
        bool SaveCommisionOnProductby(List<CommisionOnProductDTO> commisionOnProductDTOs,long userId,long orgId);
       bool IsExistsSameYearProduct(int year, long product, long orgId);
    }
}
