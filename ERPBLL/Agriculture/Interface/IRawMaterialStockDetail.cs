﻿using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialStockDetail
    {
        IEnumerable<RawMaterialStockDetail> GetRawMaterialStockDetailsById(long infoId, long orgId);
        bool SaverawMaterialStockDetail(long OrganizationId,long RawMaterialId,long SupplierId,int Quantity, string Unit, DateTime? StockDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, long? UpdateUserId, string Status, long RawMaterialStockId);
        //bool SaverawMaterialStockDetail(List<RawMaterialStockDetail> details, string BatchCodes, long userId, long orgId);

        RawMaterialStockDetail GetRawMaterialStockById(long RMDetailsId, long orgId);
        
        bool updateRawMaterialStockDetails(RawMaterialStockInfoDTO info, List<RawMaterialStockDetailDTO> rawMaterialStockDetailsDTO, long userId, long orgId);
    }
}
