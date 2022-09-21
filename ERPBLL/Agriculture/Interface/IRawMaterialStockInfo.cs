using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialStockInfo
    {
        bool SaveRawMaterialStock(RawMaterialStockInfoDTO info, List<RawMaterialStockDetailDTO> details, long userId, long orgId);


        List<AgroDropdown> GetDepotRawMaterials(long orgId);
    }
}
