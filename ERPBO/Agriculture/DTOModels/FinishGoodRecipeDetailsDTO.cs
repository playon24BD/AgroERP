using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
   public class FinishGoodRecipeDetailsDTO
    {
        public long FGRDetailsId { get; set; }
        public long RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public int FGRRawMaterQty { get; set; }
        public string FGRRawMaterUnit { get; set; }
        public long OrganizationId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public long FGRId { get; set; }
    }
}
