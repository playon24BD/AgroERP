using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class ProductWiseSalesStementReport
    {
        public string FinishGoodProductName { get; set; }
        public string PackSize { get; set; }
        public double QtyCTN { get; set; }
        public double UnitKG { get; set; }
        public double QtyKG { get; set; }
        public double Total { get; set; }
        public double TotalAmount { get; set; }
        public DateTime EntryDate { get; set; }
        public string todate { get; set; }
        public string fromDate { get; set; }
        public long ProductId { get; set; }
    }
}
