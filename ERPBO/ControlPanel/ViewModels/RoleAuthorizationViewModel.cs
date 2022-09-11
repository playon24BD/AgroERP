using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class RoleAuthorizationViewModel
    {
        public long TaskId { get; set; }
        [Range(0, long.MaxValue)]
        public long RoleId { get; set; }
        [Range(1, long.MaxValue)]
        public long ModuleId { get; set; }
        [Range(1, long.MaxValue)]
        public long MainmenuId { get; set; }
        [Range(1, long.MaxValue)]
        public long SubmenuId { get; set; }
        public long ParentSubmenuId { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Detail { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
        public long OrganizationId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
