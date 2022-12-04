using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IZoneUserBusiness
    {
        bool SaveZoneUser(List<string> zones, long userId, long suserId, long orgId,string action);
        IEnumerable<ZoneUser> GetAllZoneByUserIdAndZoneId(long userId, long orgId);
        bool UpdateZone(List<string> zones, long userId, long suserId, long orgId);
        List<ZoneSetupViewModel> GetAllZone(long userId, long orgId);
    }
}
