using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class AccessoriesPurchaseDetailsDTO
    {
        public long AccessoriesPurchaseDetailsId { get; set; }
        public long AccessoriesId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public string Status { get; set; }

        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public long AccessoriesPurchaseInfoId { get; set; }
    }
}
