using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class MonyReceptDetails
    {
        public long PaymentMoneyReciptId { get; set; }

        public double CommisionPercent { get; set; }
        public double CommisionAmount { get; set; }
        public double PaymentAmount { get; set; }
        public string InvoiceNo { get; set; }
    }
}
