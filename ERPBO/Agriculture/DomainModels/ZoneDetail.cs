using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblZoneDetail")]
    public class ZoneDetail
    {
        [Key]
        public long ZoneDetailId { get; set; }
        public long DivisionId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        [ForeignKey("Zone")]
        public long ZoneId { get; set; }
        public Zone Zone { get; set; }
        public string Status { get; set; }
        public long OrganizationId { get; set; }
        public string Remarks { get; set; }

    }
}
