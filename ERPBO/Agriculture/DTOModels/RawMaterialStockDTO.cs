using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class RawMaterialStockInfoDTO
    {
        public long RawMaterialStockId { get; set; }
        public string BatchCode { get; set; }
        public long OrganizationId { get; set; }
        public long RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public List<RawMaterialStockDetailDTO> RawMaterialStockDetails { get; set; }
    }
}
