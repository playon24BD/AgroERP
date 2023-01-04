using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class DateWiseCollectionReport
    {
        public string ZoneName { get; set; }
        public string DivisionName { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }
        public string StockiestName { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentMode { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string AccountNumber { get; set; }
        public string todate { get; set; }
        public string fromDate { get; set; }

    }
}
