using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class RawMaterialIssueStockInfoDTO
    {
        public long RawMaterialIssueStockId { get; set; }
        public string ProductBatchCode { get; set; }
        public long OrganizationId { get; set; }
        public long FinishGoodProductId { get; set; }
        public long RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string RawMaterialName { get; set; }
    }
}
