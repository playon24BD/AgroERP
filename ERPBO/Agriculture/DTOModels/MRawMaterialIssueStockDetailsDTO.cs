using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class MRawMaterialIssueStockDetailsDTO
    {
        public long RawMaterialIssueStockDetailId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public long UnitID { get; set; }
        public string IssueStatus { get; set; }




        public DateTime? EntryDate { get; set; }


        public long RawMaterialIssueStockId { get; set; }

        public MRawMaterialIssueStockInfo MRawMaterialIssueStockInfo { get; set; }


        //dto
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }

        public double CurrentStock { get; set; }
        public double StockOut { get; set; }
        public double StockIN { get; set; }

        public double BadReturn { get; set; }
        public double ReturnQty { get; set; }
        public double GoodReturn { get; set; }
        public string FinishGoodProductionBatch { get; set; }
    }
}
