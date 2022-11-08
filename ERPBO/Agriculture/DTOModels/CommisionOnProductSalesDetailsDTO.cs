using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class CommisionOnProductSalesDetailsDTO
    {
        public long CommissionOnProductSalesDetailsId { get; set; }
        public long CommissionOnProductOnSalesId { get; set; }
        public long ProductSalesInfoId { get; set; }//No need
        public long CommissionOnProductId { get; set; }//No need
        public long FinishGoodProductId { get; set; }
        public string InvoiceNo { get; set; }//No need
        public string PaymentMode { get; set; }//No need
        public double Credit { get; set; }
        public double Cash { get; set; }
        public double TotalCommission { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public long OrganizationId { get; set; }
    }
}
