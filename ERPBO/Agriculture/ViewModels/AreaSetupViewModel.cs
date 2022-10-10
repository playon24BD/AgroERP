using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class AreaSetupViewModel
    {
        public long AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaAsignName { get; set; }
        public string MobileNumber { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        public long ZoneId { get; set; }
        public long DivisionId { get; set; }
        public long RegionId { get; set; }



        //default
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
    }
}
