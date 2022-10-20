using ERPBO.Agriculture.DomainModels;

using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IPRawMaterialStockInfo
    {
        IEnumerable<PRawMaterialStockInfo> GetAllPRawMaterialStockInfo(long OrgId);

        bool SaveRawMaterialPurchaseStock(PRawMaterialStockInfoDTO info, List<PRawMaterialStockIDetailsDTO> details, long userId, long orgId);
    }
}
