using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblRawMaterialRequisitionInfo")]
    public class RawMaterialRequisitionInfo
    {
        [Key]
        public long RawMaterialRequisitionInfoId { get; set; }
        public string RawMaterialRequisitionCode { get; set; }     
        public string Type { get; set; }
        public string Status { get; set; }
        public string flag { get; set; }
        public string Remarks { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public long OrganizationId { get; set; }
    }
}
