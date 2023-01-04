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
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public double TotalAmount { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public double Total { get; set; }
        public double ProductTotal { get; set; }



        //public long ProductSalesDetailsId { get; set; }
        //public long ProductSalesInfoId { get; set; }
        //public long FinishGoodProductInfoId { get; set; }
        //public string FinishGoodProductName { get; set; }
        //public double Quanity { get; set; }
        //public double BoxQuanity { get; set; }
        //public double Price { get; set; }
        //public long MeasurementId { get; set; }
        //public string MeasurementName { get; set; }
        //public string MeasurementSize { get; set; }
        //public double Discount { get; set; }
        //public double DiscountTk { get; set; }
        //public long OrganizationId { get; set; }
        //public DateTime? UpdateDate { get; set; }
        //public long? UpdateUserId { get; set; }
        //public DateTime? EntryDate { get; set; }
        //public long? EntryUserId { get; set; }
        //public string ReceipeBatchCode { get; set; }
        //public long FGRId { get; set; }
        //public string QtyKG { get; set; }

        //public int box { get; set; }
        //public double CurrentQTY { get; set; }
        //public double TotalCurrentQTY { get; set; }
        //public double ProductTotal { get; set; }
        //public int RT { get; set; }
        //public string Status { get; set; }
        //public int MasterCarton { get; set; }
        //public int InnerBox { get; set; }
        //public double PackSize { get; set; }
        //public double CurrentStock { get; set; }
        //public int MQTY { get; set; }
        //public double TotalQty { get; set; }

        //public bool ISActive { get; set; }
        //public double RDiscountTk { get; set; }
        //public long PackageId { get; set; }

        //public string PackageName { get; set; }
        //public long AccessoriesId { get; set; }
        //public string AccessoriesName { get; set; }

        public string TotalAmountText { get; set; }

    }
}
