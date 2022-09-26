using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblRawMaterialStockInfo")]
    public class RawMaterialStockInfo
    {
        [Key]
        public long RawMaterialStockId { get; set; }
        public string BatchCode { get; set; }
        public long OrganizationId { get; set; }
        public long RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string IssueStatus { get; set; }
        public DateTime? ExpireDate { get; set; }
       
        public ICollection<RawMaterialStockDetail> RawMaterialStockDetails { get; set; }
    }
}
