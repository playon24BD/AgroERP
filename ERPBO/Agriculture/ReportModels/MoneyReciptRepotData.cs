using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ReportModels
{
    public class MoneyReciptRepotData
    {
        public string MoneyReciptNo { get; set; }
        public string StockiestName { get; set; }
        public string InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? EntryDate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string TotalAmountText { get; set; }
    }
}
