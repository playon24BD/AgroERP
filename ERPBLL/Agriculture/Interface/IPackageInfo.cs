using ERPBO.Agriculture.DomainModels;
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

        IEnumerable<PackageInfoDTO> GetAllPackageName(long orgId);

        bool SaveAgroProductPackageInfo(PackageInfoDTO packageInfoDTO, List<PackageDetailsDTO> details, long userId, long orgId);

        IEnumerable<PackageInfoDTO> GetPackageListView(string packageCode, long? packageId, string fromDate, string toDate);

    }
}
