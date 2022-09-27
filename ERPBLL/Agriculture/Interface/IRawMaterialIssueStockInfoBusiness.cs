using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialIssueStockInfoBusiness
    {
        IEnumerable<RawMaterialIssueStockInfoDTO> GetRawMaterialIssueStockInfos(long orgId, long? rawMaterialId);

        RawMaterialIssueStockInfo GetRawMaterialIssueStockById(long id, long orgId);
<<<<<<< Updated upstream
        RawMaterialIssueStockInfo RawMaterialStockIssueInfobyRawMaterialid(long rawMaterialId, long orgId);

=======
        RawMaterialIssueStockInfo GetRawMaterialIssueStockUnitById(long id, long orgId);
        
>>>>>>> Stashed changes
        bool SaveProductIssueRawMaterialStock(RawMaterialIssueStockInfoDTO info, List<RawMaterialIssueStockDetailsDTO> details, long userId, long orgId);


        bool DeleteRawMaterialIssueStock(long id, long userId, long orgId);
    }
}
