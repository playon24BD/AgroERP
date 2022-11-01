using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblMRawMaterialIssueStockInfo")]
    public class MRawMaterialIssueStockInfo
    {
        [Key]
        public long RawMaterialIssueStockId { get; set; }
        public long? RawMaterialRequisitionInfoId { get; set; }
        public string ProductBatchCode { get; set; }
        public string Status { get; set; }
        public string type { get; set; }



        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public long OrganizationId { get; set; }



        public ICollection<MRawMaterialIssueStockDetails> MRawMaterialIssueStockDetails { get; set; }
    }
}
