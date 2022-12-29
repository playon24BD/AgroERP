using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblAccessoriesTrackInfo")]
    public class AccessoriesTrackInfo
    {
        [Key]
        public long AccessoriesTrackInfoId { get; set; }
        public long AccessoriesId { get; set; }
        public double Quantity { get; set; }
        public string IssueStatus { get; set; }



        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }

        public long AccessoriesPurchaseInfoId { get; set; }
        public long ProductSalesInfoId { get; set; }
    }
}
