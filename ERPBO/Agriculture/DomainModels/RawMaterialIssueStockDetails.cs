using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblRawMaterialIssueStockDetails")]
    public class RawMaterialIssueStockDetails
    {
        [Key]
        public long RawMaterialIssueStockDetailsId { get; set; }
        public long OrganizationId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public long UnitId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string Status { get; set; }
        [ForeignKey("RawMaterialIssueStockInfo")]
        public long RawMaterialIssueStockId { get; set; }
        public RawMaterialIssueStockInfo RawMaterialIssueStockInfo { get; set; }
    }
}
