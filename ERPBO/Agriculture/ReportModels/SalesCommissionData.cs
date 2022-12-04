using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class SalesCommissionData
    {
        public string InvoiceNo { get; set; }
        public string PaymentMode { get; set; }
        public string StockiestName { get; set; }
        public double TotalCommission { get; set; }
        public DateTime EntryDate { get; set; }
        public string toDate { get; set; }
        public string fromDate { get; set; }

    }
}
