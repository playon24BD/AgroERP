using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialIssueStockDetailsBusiness
    {

        IEnumerable<RawMaterialIssueStockDetails> GetRawMaterialIssueStockDetailsById(long infoId, long orgId);
        bool SaveIssuerawMaterialStockDetail(long OrganizationId, long RawMaterialId, double Quantity, string Unit, DateTime? IssueDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, long? UpdateUserId, string Status, long RawMaterialIssueStockId);
        bool SaveRawMaterialIssueDetails(List<RawMaterialIssueStockDetailsDTO> finishGoodProductionDetailsDTOs,  long userId, long orgId);
    }
}
