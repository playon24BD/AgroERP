using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Common
{
    public class UserInformation
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }
        public long OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrgLogoPath { get; set; }
        public bool IsOrgActive { get; set; }
        public string ReportLogoPath { get; set; }
        public string RoleName { get; set; }
        public long RoleId { get; set; }
        public bool IsRoleActive { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public string AppType { get; set; }
        public long ZoneId { get; set; }
        public string ZoneName { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
    }
}
