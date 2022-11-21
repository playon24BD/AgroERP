using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class FinishGoodProductionDataReport
    {
        //public long FinishGoodProductId { get; set; }
        //public long FGRId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string FinishGoodProductionBatch { get; set; }
        public string ReceipeBatchCode { get; set; }
        public string ProductDetails { get; set; }
        public double ProductionTotal { get; set; }
        public double SalesTotal { get; set; }
        public double ReturnTotal { get; set; }
        public double CurrentPices { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
