using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{

    [Table("tblRawMaterialTrackInfo")]
    public class RawMaterialTrack
    {
        [Key]
        public long RawMaterialTrackId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueStatus { get; set; }
        public string type { get; set; }
        


        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
    }
}
