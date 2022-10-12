using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
   public class UserAssignViewModel
    {
        public long UserAssignId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long ZoneId { get; set; }
        public string ZoneName { get; set; }
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public long AreaId { get; set; }
        public string AreaName { get; set; }
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public long StockiestId { get; set; }
        public string StockiestName { get; set; }

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
