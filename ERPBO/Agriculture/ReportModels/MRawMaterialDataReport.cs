using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class MRawMaterialDataReport
    {
        public double Quantity { get; set; }
        
        public DateTime? EntryDate { get; set; }
        
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }

        public double CurrentStock { get; set; }
        public double StockOut { get; set; }
        public double StockIN { get; set; }
        public double PendingStock { get; set; }

        public double BadReturn { get; set; }
        public double ReturnQty { get; set; }
        public double GoodReturn { get; set; }
        public string FinishGoodProductionBatch { get; set; }
    }
}
