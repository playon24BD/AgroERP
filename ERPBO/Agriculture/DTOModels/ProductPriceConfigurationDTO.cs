using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class ProductPriceConfigurationDTO
    {

        public long ProductPriceConfigurationId { get; set; }
        public long FinishGoodProductId { get; set; }
        public long FGRId { get; set; }
        public double ProductPrice { get; set; }


        public DateTime? UpdateDate { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUser { get; set; }
        public string Status { get; set; }


        public string FinishGoodProductName { get; set; }
        public string Flag { get; set; }
    }
}
