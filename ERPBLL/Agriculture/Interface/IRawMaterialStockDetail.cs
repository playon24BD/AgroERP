using ERPBO.Agriculture.DomainModels;
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
        bool SaverawMaterialStockDetail(long OrganizationId,long RawMaterialId,long SupplierId,double Quantity, string Unit, DateTime? StockDate, DateTime? StockIssueDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, DateTime? ExpireDate, long? UpdateUserId, string Status, long RawMaterialStockId);
        //bool SaverawMaterialStockDetail(List<RawMaterialStockDetail> details, string BatchCodes, long userId, long orgId);

        RawMaterialStockDetail GetRawMaterialStockById(long RMDetailsId, long orgId);
        
        bool updateRawMaterialStockDetails(RawMaterialStockInfoDTO info, List<RawMaterialStockDetailDTO> rawMaterialStockDetailsDTO, long userId, long orgId);

        bool updateRawmaterialstockdetails(long id, double UpdateRawMaterialStock, double IssueRawMaterialStockQty,long orgId, string Unit, DateTime? EntryDate, long? EntryUserId);
    }
}
