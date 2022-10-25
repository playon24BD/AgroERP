using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RegionUserBusiness : IRegionUserBusiness
    {
        public List<RegionSetupViewModel> GetAllRegion(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegionUser> GetAllRegionByUserIdAndRegionId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveRegionUser(List<string> regions, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRegion(List<string> regions, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
