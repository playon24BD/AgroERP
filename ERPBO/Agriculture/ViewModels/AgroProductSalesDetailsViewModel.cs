using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class AgroProductSalesDetailsViewModel
    {
        public long ProductSalesDetailsId { get; set; }
        public long ProductSalesInfoId { get; set; }
        public long FinishGoodProductInfoId { get; set; }
        public string FinishGoodProductName { get; set; }
        public double Quanity { get; set; }
        public double Price { get; set; }
        public long MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementSize { get; set; }
        public double Discount { get; set; }
        public double DiscountTk { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }


     
       
    }
}
