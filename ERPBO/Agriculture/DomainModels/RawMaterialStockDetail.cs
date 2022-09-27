using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblRawMaterialStockDetail")]
    public class RawMaterialStockDetail
    {
        [Key]
        public long RawMaterialStockDetailId { get; set; }
        public long OrganizationId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime? StockDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string Status { get; set; }
        public long RawMaterialSupplierId { get; set; }
        [ForeignKey("RawMaterialStockInfo")]
        public long RawMaterialStockId { get; set; }
        public RawMaterialStockInfo RawMaterialStockInfo { get; set; }
        public DateTime? ExpireDate { get; set; }

    }
}
