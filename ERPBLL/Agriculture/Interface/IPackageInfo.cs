using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IPackageInfo
    {

        bool SaveAgroProductSalesInfo(PackageInfoDTO packageInfoDTO, List<PackageDetailsDTO> details, long userId, long orgId);
    }
}
