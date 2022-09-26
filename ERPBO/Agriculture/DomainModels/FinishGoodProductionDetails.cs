using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    public class FinishGoodProductionDetails
    {
        [Key]
        public long FinishGoodProductDetailId { get; set; }
        public string FinishGoodProductionBatch { get; set; }
        public string ReceipeBatchCode { get; set; }
        public long RawMaterialId { get; set; }
        public int FGRRawMaterQty { get; set; }
        public int TotalQuantity { get; set; }
        public int RequiredQuantity { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string flag { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
    }
}
