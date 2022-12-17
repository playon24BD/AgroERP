using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class FinishGoodDetails
    {
        
        public string FinishGoodProductionBatch { get; set; }
        public string RawMaterialName { get; set; }
        public double FGRRawMaterQty { get; set; }
        public double TotalQuantity { get; set; }
        public double RequiredQuantity { get; set; }
    }
}
