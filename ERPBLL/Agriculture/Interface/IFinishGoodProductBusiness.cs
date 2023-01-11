using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IFinishGoodProductBusiness
    {
        IEnumerable<FinishGoodProduct> GetAllProductInfo(long OrgId);
        bool SaveFinishGoodProductName(FinishGoodProductDTO finishGoodProduct, long userId, long orgId);
        IEnumerable<FinishGoodProduct> GetProductNameByOrgId(long orgId);
        FinishGoodProduct GetFinishGoodProductById(long FinishGoodProductId, long orgId);

        IEnumerable<FinishGoodProductionInfoDTO> GetFGProductByDate(string EntryDate);
    }
}
