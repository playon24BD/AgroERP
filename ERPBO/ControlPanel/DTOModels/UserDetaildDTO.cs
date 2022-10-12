using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
    public class UserDetaildDTO
    {
        public string EmployeeId { get; set; }
        public string OrganizationName { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string RoleName { get; set; }
        public string RoleStatus { get; set; }
        public string UserStatus { get; set; }
        public string Address { get; set; }
    }
}
