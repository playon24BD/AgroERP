using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class RawMaterialStockInfoViewModel
    {
        public long RawMaterialStockId { get; set; }
        public string BatchCode { get; set; }
        public long OrganizationId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string IssueStatus { get; set; }
        public string Status { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string RawMaterialName { get; set; }
        public DateTime? StockDate { get; set; }
        public double StockIn { get; set; }
        public double StockOut { get; set; }
        public double TotalStock { get; set; }
        //Custom 
        public DateTime? StockIssueDate { get; set; }
        public double CurrentStock { get; set; }

        public List<RawMaterialStockDetailViewModel> RawMaterialStockDetails { get; set; }
    }
}
