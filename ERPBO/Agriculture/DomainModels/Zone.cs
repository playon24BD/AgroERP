using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblZoneInfo")]
    public class Zone
    {
        [Key]
        public long ZoneId { get; set; }
        public string ZoneName { get; set; }
        public long DivisionId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string Status { get; set; }
        public long OrganizationId { get; set; }
        public string Remarks { get; set; }
        public ICollection<ZoneDetail> ZoneDetailsTable { get; set; }
    }
}
