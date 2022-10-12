using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
   public class RoleDTO
    {
        public long RoleId { get; set; }
        [StringLength(100)]
        public string RoleName { get; set; }
        public long OrganizationId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        //custom
        [StringLength(100)]
        public string OrganizationName { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
