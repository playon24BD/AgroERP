using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class TerritorySetupViewModel
    {
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string Status { get; set; }

        //forignkey
        public long AreaId { get; set; }

        //default
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }

        //DTO
        public string UserName { get; set; }
        public string OrganizationName { get; set; }
        public string AreaName { get; set; }
        public long StockiestId { get; set; }
        public string StockiestName { get; set; }
    }
}
