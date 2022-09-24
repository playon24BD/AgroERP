using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
﻿using ERPBO.Agriculture.DomainModels;
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

        IEnumerable<RawMaterialStockInfoDTO> GetRawMaterialStockInfos(long orgId, long? rawMaterialId);
        IEnumerable<RawMaterialStockInfoDTO> GetCheckExpairDatewiseRawMaterials(long orgId);

        RawMaterialStockInfo GetRawMaterialStockById(long id, long orgId);
        List<RawMaterialStockInfoDTO> RawMaterialStockInfoIdGet(long orgId, string BatchCodes);
        //RawMaterialStockInfo RawMaterialStockInfoIdGet(string BatchCodes, long? RawMaterialId);

        bool DeleteRawMaterialStock(long id, long userId, long orgId);

    }
}
