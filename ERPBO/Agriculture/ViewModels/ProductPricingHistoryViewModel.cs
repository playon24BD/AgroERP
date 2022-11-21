using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class ProductPricingHistoryViewModel
    {
        public long ProductPricingHistoryId { get; set; }
        public long FinishGoodProductId { get; set; }
        public long FGRId { get; set; }
        public double ProductPrice { get; set; }




        public DateTime? EntryDate { get; set; }
        public long? EntryUser { get; set; }
        public string Status { get; set; }

        public string Flag { get; set; }
        public string FinishGoodProductName { get; set; }
        public string UserName { get; set; }
    }
}
