using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class DivisionInfoDTO
    {
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
        public long OrganizationId { get; set; }
        public string DivisionAssignName { get; set; }
        public string MobileNumber { get; set; }
        public long ZoneId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string Remarks { get; set; }
        public string OrganizationName { get; set; }
        public string ZoneName { get; set; }

    }
}
