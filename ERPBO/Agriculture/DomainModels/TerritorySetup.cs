﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblTerritoryInfos")]
    public class TerritorySetup
    {
        [Key]
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string Status { get; set; }

        //forignkey
        public long AreaId { get; set; }

        //default
        public long OrganizationId { get; set; }
        public long RoleId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
    }
}
