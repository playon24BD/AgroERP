using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class MRawMaterialIssueStockInfoViewModel
    {
        public long RawMaterialIssueStockId { get; set; }
        public long? RawMaterialRequisitionInfoId { get; set; }
        public string ProductBatchCode { get; set; }
        public string Status { get; set; }
        public string type { get; set; }




        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public long OrganizationId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }

        public ICollection<MRawMaterialIssueStockDetails> MRawMaterialIssueStockDetails { get; set; }

        public string OrganizationName { get; set; }
        public string UserName { get; set; }
    }
}
