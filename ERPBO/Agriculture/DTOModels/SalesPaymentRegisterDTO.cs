using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class SalesPaymentRegisterDTO
    {
        public long PaymentRegisterID { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public long ProductSalesInfoId { get; set; }//foregin key
        public long? EntryUserId { get; set; }

        public string UserName { get; set; }


        public string PaymentMode { get; set; }
        public string AccounrNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ZoneName { get; set; }
        public string DivisionName { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }
        public string StockiestName { get; set; }
        public string InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
    }
}
