﻿using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRMStockDashboardGrap
    {
        IEnumerable<RawMaterialTrackDTO> GetMainStockRMName(long orgId);
        IEnumerable<RawMaterialTrackDTO> GetMainStockRMCurrentStock(long orgId);
    }
}