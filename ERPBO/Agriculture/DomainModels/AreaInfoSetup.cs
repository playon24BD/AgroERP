using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblAreaSetup")]
    public class AreaInfoSetup
    {
        [Key]
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
