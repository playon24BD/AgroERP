using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class AgroProductSalesInfoDTO
    {
        public long ProductSalesInfoId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ChallanNo { get; set; }
        public string Depot { get; set; }
        public DateTime? ChallanDate { get; set; }
        public long UserAssignId { get; set; }
        public long UserId { get; set; }
        public long ZoneId { get; set; }
        public long DivisionId { get; set; }
        public long RegionId { get; set; }
        public long AreaId { get; set; }
        public long TerritoryId { get; set; }
        public long StockiestId { get; set; }
        public string VehicleType { get; set; }
        public string VehicleNumber { get; set; }
        public string DriverName { get; set; }
        public string DeliveryPlace { get; set; }
        public string Do_ADO_DA { get; set; }
        public string DoADO_Name { get; set; }
        public string PaymentMode { get; set; }
        public double TotalAmount { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public string StockiestName { get; set; }
        public string EntryUser { get; set; }

        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public string MeasurementName { get; set; }

        public double AdjustTotalReturn { get; set; }
        public double NotAdjustTotalReturn { get; set; }

        public long SalesReturnId { get; set; }
        public string TerritoryName { get; set; }

        public long FinishGoodProductId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string PackSize { get; set; }
        public double QtyCTN { get; set; }
        public double QtyKG { get; set; }
        public double Total { get; set; }

        //public string StockiestName { get; set; }
        public string ZoneName { get; set; }
        //public string TerritoryName { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string DivisionName { get; set; }

        public string Status { get; set; }
        public string MeasurementSize { get; set; }
        public double Quanity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double DiscountTK { get; set; }
        public double CommisionPercent { get; set; }
        public double CommisionAmount { get; set; }
        public string flag { get; set; }
        public decimal InvoiceTk { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DAmount { get; set; }
        public decimal DiscountTks { get; set; }

        public int RT { get; set; }






    }
}
