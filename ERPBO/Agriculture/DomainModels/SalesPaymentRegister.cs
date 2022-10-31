using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblProductSalesPaymentHistory")]
    public class SalesPaymentRegister
    {
        [Key]
        public long PaymentRegisterID { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public long ProductSalesInfoId { get; set; }//foregin key
        public long? EntryUserId { get; set; }

        public string PaymentMode { get; set; }
        public string AccounrNumber { get; set; }


    }
}
