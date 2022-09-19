﻿using ERPBO.Agriculture.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IFinishGoodRecipeDetailsBusiness
    {
        IEnumerable<FinishGoodRecipeDetails> GetFinishGoodRecipeDetailsByInfoId(long infoId, long orgId);
    }
}
