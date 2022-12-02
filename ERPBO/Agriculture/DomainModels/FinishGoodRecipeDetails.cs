using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DomainModels
{
    [Table("tblFinishGoodRecipeDetails")]
    public class FinishGoodRecipeDetails
    {
        [Key]
        public long FGRDetailsId { get; set; }
        public long RawMaterialId { get; set; }
        public string ReceipeBatchCode { get; set; }
        public double FGRRawMaterQty { get; set; }
        //public string FGRRawMaterUnit { get; set; }
        public long UnitId { get; set; }
        public long OrganizationId { get; set; }
        public long? EUserId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public long? UpUserId { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        [ForeignKey("FinishGoodRecipeInfo")]
        public long FGRId { get; set; }
        public FinishGoodRecipeInfo FinishGoodRecipeInfo { get; set; }
    }
}
