using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{

    [Table("tblMRawMaterialIssueStockDetails")]
    public class MRawMaterialIssueStockDetails
    {
        [Key]
        public long RawMaterialIssueStockDetailId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public long UnitID { get; set; }
        public string IssueStatus { get; set; }




        public DateTime? EntryDate { get; set; }




        [ForeignKey("MRawMaterialIssueStockInfo")]
        public long RawMaterialIssueStockId { get; set; }

        public MRawMaterialIssueStockInfo MRawMaterialIssueStockInfo { get; set; }
        public string FinishGoodProductionBatch { get; set; }
    }
}
