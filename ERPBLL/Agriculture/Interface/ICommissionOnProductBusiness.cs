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
        CommisionOnProduct GetCommisionOnProductbyId(long commissionOnProductId,long orgId);
        bool SaveCommisionOnProductby(List<CommisionOnProductDTO> commisionOnProductDTOs,long userId,long orgId);
    }
}
