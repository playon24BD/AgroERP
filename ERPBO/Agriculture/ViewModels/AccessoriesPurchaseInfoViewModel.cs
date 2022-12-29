using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class AccessoriesPurchaseInfoViewModel
    {
        public long AccessoriesPurchaseInfoId { get; set; }
        public string BatchCode { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalAmount { get; set; }
        public long RawMaterialSupplierId { get; set; }


        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }

        public string Flag { get; set; }
        public string RawMaterialSupplierName { get; set; }
    }
}
