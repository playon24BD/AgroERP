using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class RawMaterialSupplierDTO
    {
        [Key]
        public long RawMaterialSupplierId { get; set; }
        public string RawMaterialSupplierName { get; set; }
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string OrganizationName { get; set; }

        public string TradeLicense { get; set; }
        public string TIN { get; set; }
        public string BIN { get; set; }
    }
}
