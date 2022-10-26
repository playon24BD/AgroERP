using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class ProductSalesDataReport
    {
        public long ProductSalesInfoId { get; set; }
        public long FinishGoodProductInfoId { get; set; }
        public long FinishGoodProductId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? ChallanDate { get; set; }
        public string StockiestName { get; set; }
        public string ZoneName { get; set; }
        public string DivisionName { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }
        //public string ProductName { get; set; }
        public string Depot { get; set; }
        public string VehicleType { get; set; }
        public string VehicleNumber { get; set; }
        public string DriverName { get; set; }
        public string DeliveryPlace { get; set; }
        public string Do_ADO_DA { get; set; }
        public string DoADO_Name { get; set; }
        public string PaymentMode { get; set; }
        public double Quanity { get; set; }
        public string MeasurementSize { get; set; }
        public double Discount { get; set; }
        public double DiscountTk { get; set; }
        public double Price { get; set; }
        public float PaidAmount { get; set; }
        public float DueAmount { get; set; }
        public float TotalAmount { get; set; }

    }
}
