﻿using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAccessoriesInfo
    {
        IEnumerable<AccessoriesInfoDTO> GetAccessoriesList(string accessoriesName);
        AccessoriesInfo GetAccessoriesNamebyId(long accessoriesId);
        bool SaveAccessoriesInfo(AccessoriesInfoDTO accessoriesInfoDTO, long userId);
        IEnumerable<AccessoriesInfo> GetAllAccessories();
    }
}
