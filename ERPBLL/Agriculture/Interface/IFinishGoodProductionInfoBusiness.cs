﻿using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
   public interface IFinishGoodProductionInfoBusiness
    {
        IEnumerable<FinishGoodProductionInfo> GetFinishGoodProductionInfo(long orgId);

     
        FinishGoodProductionInfo GetProductionInfoById(long id, long orgId);
        FinishGoodProductionInfo GetFinishGoodProductionByAny(string any, long orgId);
        bool SaveFinishGoodInfo(FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, List<FinishGoodProductionDetailsDTO> details, long userId, long orgId);
    }
}