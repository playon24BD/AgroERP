using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
    public class AppUserDTO
    {
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
        public long BranchId { get; set; }

        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Desigation { get; set; }
        public string ConfirmPassword { get; set; }
        public long OrganizationId { get; set; }

        //custom
        public string BranchName { get; set; }
        public string RoleName { get; set; }
        public string OrganizationName { get; set; }
        public string StateStatus { get; set; }
        public string StateStatusRole { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
