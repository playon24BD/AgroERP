using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class ZoneSetupDTO
    {

        public long ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string Remarks { get; set; }

        public string MobileNumber { get; set; }
        public string ZoneAsignName { get; set; }


        //default
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }


        //dto
        public string UserName { get; set; }
        public string OrganizationName { get; set; }
    }
}
