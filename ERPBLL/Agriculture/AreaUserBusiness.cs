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
    public class AreaUserBusiness : IAreaUserBusiness
    {
        public List<AreaSetupViewModel> GetAllArea(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AreaUser> GetAllAreaByUserIdAndAreaId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveAreasUser(List<string> areas, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAreaUser(List<string> areas, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
