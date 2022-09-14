﻿using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialBusiness
    {
        bool SaveRawMaterial(RawMaterialDTO rawMaterial,long userId,long orgId);

        RawMaterial GetRawMaterialById(long rawMaterialId,long orgId);
        IEnumerable<RawMaterial> GetRawMaterials(long orgId );
    }
}