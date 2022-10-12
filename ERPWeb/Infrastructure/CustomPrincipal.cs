using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ERPWeb.Infrastructure
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            return roles.Any(r => role.Contains(r));
        }
        public CustomPrincipal(string UserName)
        {
            this.Identity = new GenericIdentity(UserName);
        }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public DateTime LogInTime { get; set; }
        public long OrgId { get; set; }
        public string OrgName { get; set; }
        public bool IsOrgActive { get; set; }
        public string MacID { get; set; }
        public string[] roles { get; set; }
        public long? RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsRoleActive { get; set; }
        public bool IsUserActive { get; set; }
        // For Images //
        public string OrgLogo { get; set; }
        public string HeaderLogo { get; set; }
        public string[] LogoPaths { get; set; }
        // Org //
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string EmployeeId { get; set; }
        public string Designation { get; set; }
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