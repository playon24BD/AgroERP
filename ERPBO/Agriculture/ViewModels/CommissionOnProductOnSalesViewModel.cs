using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class CommissionOnProductOnSalesViewModel
    {
        public long CommissionOnProductOnSalesId { get; set; }
        public long ProductSalesInfoId { get; set; }
        public long FinishGoodProductId { get; set; }//No Need
        public string InvoiceNo { get; set; }//No need
        public string PaymentMode { get; set; }//No need
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public long OrganizationId { get; set; }
        //
        public string FinishGoodProductName { get; set; }
        public double TotalCommission { get; set; }
        public string StockiestName { get; set; }
    }
}
