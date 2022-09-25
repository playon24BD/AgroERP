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
        bool SaveProductIssueRawMaterialStock(RawMaterialIssueStockInfoDTO info, List<RawMaterialIssueStockDetailsDTO> details, long userId, long orgId);
    }
}
