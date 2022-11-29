using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblProductionPerproductCost")]
    public class ProductionPerproductCost
    {

        [Key]
        public long ProductionPerproductCostId { get; set; }
        public long FinishGoodProductId { get; set; }
        public long FGRId { get; set; }
        public double PerProductRMtotalCost { get; set; }
        public double PerProductOtherCost { get; set; }
        public double PerProductMainCost { get; set; }




        public DateTime? UpdateDate { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUser { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
    }
}
