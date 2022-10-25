using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRegionUserBusiness
    {
        bool SaveRegionUser(List<string> regions, long userId, long suserId, long orgId);
        IEnumerable<RegionUser> GetAllRegionByUserIdAndRegionId(long userId, long orgId);
        bool UpdateRegion(List<string> regions, long userId, long suserId, long orgId);
        List<RegionSetupViewModel> GetAllRegion(long userId, long orgId);

    }
}
