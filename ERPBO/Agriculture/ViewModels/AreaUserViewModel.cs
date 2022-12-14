using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class AreaUserViewModel
    {
        public long AreaUserId { get; set; }
        public long UserId { get; set; }
        public long AreaId { get; set; }
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
