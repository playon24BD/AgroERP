using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblFinishGoodSupplierInfo")]
    public class FinishGoodSupplier
    {
        [Key]
        public long FinishGoodSupplierId { get; set; }
        public string FinishGoodSupplierName { get; set; }
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public string Status { get; set; }
    }
}
