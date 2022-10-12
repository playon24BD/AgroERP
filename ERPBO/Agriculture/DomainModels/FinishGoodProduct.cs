using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblFinishGoodProductInfo")]
    public class FinishGoodProduct
    {
        [Key]
        public long FinishGoodProductId { get; set; }
        public string FinishGoodProductName { get; set; }
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUser { get; set; }
        public string Status { get; set; }

    }
}
