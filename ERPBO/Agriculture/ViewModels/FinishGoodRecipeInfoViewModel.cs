using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class FinishGoodRecipeInfoViewModel
    {
        public long FGRId { get; set; }
        public long FinishGoodProductId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string ReceipeBatchCode { get; set; }
        public int FGRQty { get; set; }
        public long UnitId { get; set; }
        public long OrganizationId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string Status { get; set; }
    }
}
