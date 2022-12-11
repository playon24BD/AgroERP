using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class PaymentMoneyReciptViewModel
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
    }
}
