using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class DistributionUserViewModel
    {
        public long DistributionUserId { get; set; }
        public long UserId { get; set; }
        public long? ZoneId { get; set; }
        public long? DivisionId { get; set; }
        public long? RegionId { get; set; }
        public long? AreaId { get; set; }
        public long? TerritoryId { get; set; }
        public long? StockiestId { get; set; }
        public string DistributionType { get; set; }
        public long OrganizationId { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
    }
}
