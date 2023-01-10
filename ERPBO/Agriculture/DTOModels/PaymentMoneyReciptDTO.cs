using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class PaymentMoneyReciptDTO
    {
        public long PaymentMoneyReciptId { get; set; }
        public string MoneyReciptNo { get; set; }

        public long StockiestId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }

        public string BankName { get; set; }
        public string BranchName { get; set; }

        public string CustomerName { get; set; }

        public string PaymentMode { get; set; }
        public string AccounrNumber { get; set; }

        public string StockiestName { get; set; }

        public string flag { get; set; }
        public double DueAmount { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double TAmount { get; set; }
        public double PAmount { get; set; }
        public double DAmount { get; set; }
        public double PaymentAmount { get; set; }
        public double CommisionPercent { get; set; }
        public double CommisionAmount { get; set; }

        public string Status { get; set; }
    }
}
