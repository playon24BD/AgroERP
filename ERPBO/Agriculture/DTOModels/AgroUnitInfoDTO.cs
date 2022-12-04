using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class AgroUnitInfoDTO
    {
        
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public string Status { get; set; }

        //default
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public string UserName { get; set; }
    }
}
