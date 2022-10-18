﻿using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class PRawMaterialStockIDetailsViewModel
    {
        public long PRawMaterialStockDetailId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public long UnitID { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public DateTime? StockDate { get; set; }
        public DateTime? StockIssueDate { get; set; }
        public string Status { get; set; }
        public long DepotId { get; set; }
        public DateTime? ExpireDate { get; set; }


        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }


        public long PRawMaterialStockId { get; set; }

        public PRawMaterialStockInfo PRawMaterialStockInfo { get; set; }


        //dto
        public string RawMaterialName { get; set; }
        public string UnitName { get; set; }
    }
}
