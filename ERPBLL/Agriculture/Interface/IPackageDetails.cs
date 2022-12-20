﻿using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IPackageDetails
    {
        IEnumerable<PackageDetailsDTO> GetPackageDetailsView(long id, long orgId);

        IEnumerable<PackageDetails> GetPackageDetailsBY(long PackageId);


   
    }
}
