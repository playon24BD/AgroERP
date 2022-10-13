using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblUserAssign")]
    public class UserAssign
    {
        [Key]
        public long UserAssignId { get; set; }
        public long UserId { get; set; }
        public long OrganizationId { get; set; }
        //public string UserName { get; set; }
        public long ZoneId { get; set; }
        public long DivisionId { get; set; }
        public long RegionId { get; set; }
        public long AreaId { get; set; }
        public long TerritoryId { get; set; }
        public long StockiestId { get; set; }

        //Common Column
        public string Remarks { get; set; }
        public string Flag { get; set; }
        public string Status { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
    }
}
