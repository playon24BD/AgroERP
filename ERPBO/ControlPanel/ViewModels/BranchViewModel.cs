using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.ViewModels
{
    public class BranchViewModel
    {
        public long BranchId { get; set; }
        [StringLength(150)]
        public string BranchName { get; set; }
        [StringLength(50)]
        public string ShortName { get; set; }
        [StringLength(50)]
        public string MobileNo { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(150)]
        public string PhoneNo { get; set; }
        [StringLength(150)]
        public string Fax { get; set; }
        public bool IsActive { get; set; }
        [StringLength(150)]
        public string Remarks { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public long OrgId { get; set; }
        //custom
        [StringLength(150)]
        public string OrganizationName { get; set; }
        public string StateStatus { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
        public string Address { get; set; }
    }
}
