using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DomainModels
{
    [Table("tblApplicationUsers")]
    public class AppUser
    {
        [Key]
        public long UserId { get; set; }
        [StringLength(150)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoleActive { get; set; }
        public long RoleId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [ForeignKey("Branch")]
        public long BranchId { get; set; }
        public Branch Branch { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Desigation { get; set; }
        public string ConfirmPassword { get; set; }
        public long OrganizationId { get; set; }
    }
}
