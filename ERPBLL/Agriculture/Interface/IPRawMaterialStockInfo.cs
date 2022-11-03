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
        IEnumerable<PRawMaterialStockInfoDTO> GetAllPRawMaterialStockInfo(long OrgId, string name, string ChallanNo, string PONumber, long? supplierId, long? rsupid);

        bool SaveRawMaterialPurchaseStock(PRawMaterialStockInfoDTO info, List<PRawMaterialStockIDetailsDTO> details, long userId, long orgId);

        PRawMaterialStockInfo GetRawmaterialPuschaseInfoOneById(long id, long orgId);
    }
}
