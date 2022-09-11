using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERPBO.ControlPanel.DTOModels
{
    public class OrganizationDTO
    {
        public long OrgId { get; set; }
        [StringLength(150)]
        public string OrganizationName { get; set; }
        [StringLength(50)]
        public string ShortName { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> ContractDate { get; set; }
        [StringLength(250)]
        public string OrgLogoPath { get; set; }
        [StringLength(250)]
        public string ReportLogoPath { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string AppType { get; set; }
        // Custom Prop
        public string StateStatus { get; set; }
        public HttpPostedFileBase OrgImage { get; set; }
        public HttpPostedFileBase ReportImage { get; set; }
        public string EntryUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
