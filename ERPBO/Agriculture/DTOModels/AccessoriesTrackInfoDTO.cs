using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class AccessoriesTrackInfoDTO
    {
        public long AccessoriesTrackInfoId { get; set; }
        public long AccessoriesId { get; set; }
        public double Quantity { get; set; }
        public string IssueStatus { get; set; }



        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }

        public long AccessoriesPurchaseInfoId { get; set; }
        public long ProductSalesInfoId { get; set; }
        public double StockIn { get; set; }
        public double StockOut { get; set; }
        public double TotalStock { get; set; }
        public string AccessoriesName { get; set; }
        public double Quanity { get; set; }

    }
}
