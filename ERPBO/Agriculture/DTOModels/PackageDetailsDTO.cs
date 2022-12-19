﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBO.Agriculture.DTOModels
{
    public class PackageDetailsDTO
    {
        public long PackageDetailsId { get; set; }
        public long FinishGoodProductId { get; set; }
        public long MeasurementId { get; set; }
        public long FGRId { get; set; }
        public double Quanity { get; set; }
        public double Amount { get; set; }

        public string Status { get; set; }
        public string Flag { get; set; }



        //default
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime? EntryDate { get; set; }
        public long? EntryUserId { get; set; }
        public long PackageId { get; set; }
        public string FinishGoodProductName { get; set; }
        public string MeasurementName { get; set; }
        public double Rate { get; set; }
    }
}