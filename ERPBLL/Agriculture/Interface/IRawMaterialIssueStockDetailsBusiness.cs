using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialIssueStockDetailsBusiness
    {
        bool SaveIssuerawMaterialStockDetail(long OrganizationId, long RawMaterialId, int Quantity, string Unit, DateTime? IssueDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, long? UpdateUserId, string Status, long RawMaterialIssueStockId);
    }
}
