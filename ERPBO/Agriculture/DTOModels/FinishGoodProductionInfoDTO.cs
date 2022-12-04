using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
   public class FinishGoodProductionInfoDTO
    {
        public long FinishGoodProductionInfoId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string FinishGoodProductionBatch { get; set; }
        public string ReceipeBatchCode { get; set; }
        public long FinishGoodProductId { get; set; }
        
        public double Quanity { get; set; }
        public double TargetQuantity { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string flag { get; set; }
        public long OrganizationId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public long FGRId { get; set; }


        public double ProductionTotal { get; set; }
        public double SalesTotal { get; set; }
        public double ReturnTotal { get; set; }
        public double CurrentStock { get; set; }
        public int FGRQty { get; set; }
        public long UnitId { get; set; }

        public string UnitName { get; set; }

        public long FinishGoodProductInfoId { get; set; }
    }
}
