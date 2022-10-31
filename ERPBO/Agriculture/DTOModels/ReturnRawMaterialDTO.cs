using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class ReturnRawMaterialDTO
    {
        public long ReturnRawMaterialId { get; set; }
       // public long RawMaterialIssueStockId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public long UnitId { get; set; }
        public string ReturnType { get; set; }
        public string Status { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }

        public string UserName { get; set; }
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }


        public double TQuantity { get; set; }
       
    }
}
