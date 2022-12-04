using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DomainModels
{
    [Table("tblBranch")]
    public class Branch
    {
        [Key]
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public string ShortName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }
        public long? EUserId { get; set; }
        [StringLength(150)]
        public string Remarks { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [ForeignKey("Organization")]
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public string Address { get; set; }
    }
}
