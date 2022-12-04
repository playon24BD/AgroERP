using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class UserInfoDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string Designation { get; set; }
        public string DepartmentName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        //defalult
        public long RoleId { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
    }
}
