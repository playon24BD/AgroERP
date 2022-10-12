using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.ControlPanel.DTOModels
{
    public class OrganizationAuthorizationDTO
    {
        public long OAuthId { get; set; }
        public long OrganizationId { get; set; }
        public long MainmenuId { get; set; }
        public long ModuleId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        // Custom Property//
        public string ModuleName { get; set; }
        public string MainmenuName { get; set; }
        public string OrganizationName { get; set; }
    }
}
