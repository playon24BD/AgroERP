using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class SalesReturnReportData
    {
        public DateTime? ReturnDate { get; set; }
        public string InvoiceNo { get; set; }
        public double BoxQuanity { get; set; }
        public double ReturnQuanity { get; set; }
        public double ReturnPerUnitPrice { get; set; }
        public double ReturnTotalPrice { get; set; }
        public string Status { get; set; }
        public string MeasurementSize { get; set; }
        public string FinishGoodProductName { get; set; }
        public string StockiestName { get; set; }
    }
}
