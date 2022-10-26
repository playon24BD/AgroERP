using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAreaUserBusiness
    {
        bool SaveAreasUser(List<string> areas, long userId, long suserId, long orgId,string action);
        IEnumerable<AreaUser> GetAllAreaByUserIdAndAreaId(long userId, long orgId);
        bool UpdateAreaUser(List<string> areas, long userId, long suserId, long orgId);
        List<AreaSetupViewModel> GetAllArea(long userId, long orgId);
    }
}
