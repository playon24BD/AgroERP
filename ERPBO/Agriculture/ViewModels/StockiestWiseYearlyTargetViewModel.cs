using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class StockiestWiseYearlyTargetViewModel
    {
        public long StockiestWiseYearlyTargetId { get; set; }
        public long StockiestId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double TargetQty { get; set; }
        public string Flag { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
    }
}
