using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblProductPricingHistory")]
    public class ProductPricingHistory
    {

        [Key]
        public long ProductPricingHistoryId { get; set; }
        public long FinishGoodProductId { get; set; }
        public long FGRId { get; set; }
        public double ProductPrice { get; set; }




        public DateTime? EntryDate { get; set; }
        public long? EntryUser { get; set; }
        public string Status { get; set; }

        public string Flag { get; set; }
    }
}
