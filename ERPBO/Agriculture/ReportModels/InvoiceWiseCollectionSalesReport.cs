using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class InvoiceWiseCollectionSalesReport
    {
        public long ProductSalesInfoId { get; set; }
        public string AreaName { get; set; }
        public string ZoneUserName { get; set; }
        public string ZoneUserMobile { get; set; }
        public string TerritoryName { get; set; }
        public string TerritoryUserName { get; set; }
        public string TerritoryUserMobile { get; set; }
        public string StockiestName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double InvoiceTK { get; set; }
        public double Collaction { get; set; }
        public double DueAmount { get; set; }
        public double DiscountTk { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string Remarks { get; set; }
        public string todate { get; set; }
        public string fromDate { get; set; }

        //public long FinishGoodProductInfoId { get; set; }
        //public long FinishGoodProductId { get; set; }
        //public string FinishGoodProductName { get; set; }

        //public string ChallanNo { get; set; }
        //public DateTime? ChallanDate { get; set; }

        //public string ZoneName { get; set; }
        //public string DivisionName { get; set; }
        //public string RegionName { get; set; }


        ////public string ProductName { get; set; }
        //public string Depot { get; set; }
        //public string VehicleType { get; set; }
        //public string VehicleNumber { get; set; }
        //public string DriverName { get; set; }
        //public string DeliveryPlace { get; set; }
        //public string Do_ADO_DA { get; set; }
        //public string DoADO_Name { get; set; }
        //public string PaymentMode { get; set; }
        //public double Quanity { get; set; }
        //public string MeasurementSize { get; set; }
        //public double Discount { get; set; }
        //public double DiscountPercent { get; set; }

        //public double DiscountAmount { get; set; }
        //public double Price { get; set; }
        //public double PaidAmount { get; set; }

        //public string FullName { get; set; }
        //public string Address { get; set; }
        //public string MobileNo { get; set; }
        //public double Total { get; set; }
        //public string TotalAmountText { get; set; }

    }
}
