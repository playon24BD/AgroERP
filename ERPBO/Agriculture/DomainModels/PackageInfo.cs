using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblPackageInfo")]
    public class PackageInfo
    {
        [Key]
        public long PackageId { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }

        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }

        public string Flag { get; set; }
    }
}
