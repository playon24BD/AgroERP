using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DomainModels
{
    [Table("tblOrganizations")]
    public class Organization
    {
        [Key]
        public long OrganizationId { get; set; }
        [StringLength(150)]
        public string OrganizationName { get; set; }
        [StringLength(50)]
        public string ShortName { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> ContractDate { get; set; }
        [StringLength(250)]
        public string OrgLogoPath { get; set; }
        [StringLength(250)]
        public string ReportLogoPath { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public string AppType { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public ICollection<Branch> Branches { get; set; }
    }
}
