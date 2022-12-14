using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblProductSalesDetails")]
    public class AgroProductSalesDetails
    {
        [Key]
        public long ProductSalesDetailsId { get; set; }
        //public long ProductSalesInfoId { get; set; }
        public long FinishGoodProductInfoId { get; set; }//FinishGoodProductId
        public double Quanity { get; set; }
        public double BoxQuanity { get; set; }
        public double Price { get; set; }
        public long MeasurementId { get; set; }
        public string MeasurementSize { get; set; }
        public double Discount { get; set; }
        public double DiscountTk { get; set; }

        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public string ReceipeBatchCode { get; set; }
        public long FGRId { get; set; }
        public string QtyKG { get; set; }
        public string Status { get; set; }

        [ForeignKey("AgroProductSalesInfo")]
        public long ProductSalesInfoId { get; set; }
        public AgroProductSalesInfo AgroProductSalesInfo { get; set; }

    }
}
