using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblRawMaterialInfo")]
    public class RawMaterial
    {
        [Key]
        public long RawMaterialId { get; set; }
        public long OrganizationId { get; set; }
        public string RawMaterialName { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long EntryUserId { get; set; }
        public long DepotId { get; set; }
        public string Status { get; set; }
        //public DateTime? ExpireDate { get; set; }
    }
}
