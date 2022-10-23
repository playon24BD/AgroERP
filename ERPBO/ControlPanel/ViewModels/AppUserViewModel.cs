using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class AppUserViewModel
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
        [Range(1, long.MaxValue)]
        public long RoleId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [Range(1, long.MaxValue)]
        public long BranchId { get; set; }

        [StringLength(50)]
        public string MobileNo { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Desigation { get; set; }
        public string ConfirmPassword { get; set; }
        [Range(1, long.MaxValue)]
        public long OrganizationId { get; set; }

        //custom
        [StringLength(100)]
        public string BranchName { get; set; }
        [StringLength(100)]
        public string RoleName { get; set; }
        [StringLength(100)]
        public string OrganizationName { get; set; }
        public string StateStatus { get; set; }
        public string StateStatusRole { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        public List<string> DivisionId { get; set; }
        public List<DivisionInfoViewModel> DivisionName { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string StockiestId { get; set; }
        public string StockiestName { get; set; }
    }

}
