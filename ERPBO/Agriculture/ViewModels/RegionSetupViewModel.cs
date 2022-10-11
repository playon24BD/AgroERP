using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class RegionSetupViewModel
    {
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }

        //forignkey
        public long DivisionId { get; set; }

        //default
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }


        //dto
        public string UserName { get; set; }
        public string OrganizationName { get; set; }

        //forignDTO
        public string ZoneName { get; set; }
        public string DivisionName { get; set; }
    }
}
