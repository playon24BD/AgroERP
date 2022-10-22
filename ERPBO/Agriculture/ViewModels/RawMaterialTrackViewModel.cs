﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.ViewModels
{
    public class RawMaterialTrackViewModel
    {
        public long RawMaterialTrackId { get; set; }
        public long RawMaterialId { get; set; }
        public double Quantity { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueStatus { get; set; }



        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public string UserName { get; set; }
        public string RawMaterialName { get; set; }
    }
}
