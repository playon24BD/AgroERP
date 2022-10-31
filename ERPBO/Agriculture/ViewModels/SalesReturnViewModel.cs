using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class SalesReturnViewModel
    {
        public long SalesReturnId { get; set; }
        public string ReturnCode { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double ReturnQuanity { get; set; }
        public double ReturnPerUnitPrice { get; set; }
        public double ReturnTotalPrice { get; set; }
        public string Status { get; set; }




        public long FinishGoodProductInfoId { get; set; }
        public long MeasurementId { get; set; }
        public long ProductSalesInfoId { get; set; }

        public DateTime? ReturnDate { get; set; }
        public DateTime? AdjustmentDate { get; set; }
        public long? EntryUserId { get; set; }
    }
}
