using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class FinishGoodProductionAcceptDataReport
    {
        public long FinishGoodProductionInfoId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string FinishGoodProductionBatch { get; set; }
        public double Quanity { get; set; }
        public double TargetQuantity { get; set; }
        public string Status { get; set; }
        public DateTime? EntryDate { get; set; }
        public string RawMaterialName { get; set; }
        public double FGRRawMaterQty { get; set; }
        public double TotalQuantity { get; set; }
        public double RequiredQuantity { get; set; }
        public string UnitQuantity { get; set; }

    }
}
