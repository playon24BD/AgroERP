using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblCommissionProductHistory")]
    public class CommisionOnProductHistory
    {
        [Key]
        public long CommisionOnProductHistoryId { get; set; }
        public long CommissionOnProductId { get; set; }
        public long FGRId { get; set; }
        public long FinishGoodProductId { get; set; }//productId
        public int CalenderYear { get; set; }
        public double Credit { get; set; }
        public double Cash { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //Not need bellow two
        public double? Quantity { get; set; }
        public long? UnitId { get; set; }



        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Flag { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public long OrganizationId { get; set; }

    }
}
