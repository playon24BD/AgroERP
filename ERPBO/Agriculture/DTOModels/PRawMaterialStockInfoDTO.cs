using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class PRawMaterialStockInfoDTO
    {
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
        public string ProductCode { get; set; }



        public ICollection<PRawMaterialStockIDetails> PRawMaterialStockIDetails { get; set; }

        //dto
        public string RawMaterialSupplierName { get; set; }
        public string OrganizationName { get; set; }
        public string UserName { get; set; }

    }
}
