using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class SalesPaymentRegisterViewModel
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

    }
}
