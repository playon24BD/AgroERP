using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblPRawMaterialStockInfo")]
    public class PRawMaterialStockInfo
    {
        [Key]
        public long PRawMaterialStockId { get; set; }
        public string BatchCode { get; set; }
        public string ChalanNo { get; set; }
        public DateTime ChalanDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string IssueStatus { get; set; }


        public double TotalAmount { get; set; }
        public long RawMaterialSupplierId { get; set; }

        public long OrganizationId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }



        public ICollection<PRawMaterialStockIDetails> PRawMaterialStockIDetails { get; set; }

    }
}
