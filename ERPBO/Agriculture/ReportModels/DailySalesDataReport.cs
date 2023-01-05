using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class DailySalesDataReport
    {
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string StockiestName { get; set; }
        public string TerritoryName { get; set; }

        public string ZoneName { get; set; }
        
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string DivisionName { get; set; }
        public double Discount { get; set; }
        public double DiscountTK { get; set; }
        public decimal InvoiceTk { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DAmount { get; set; }
        public string todate { get; set; }
        public string fromDate { get; set; }
    }
}
