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
    public class ZoneUserBusiness : IZoneUserBusiness
    {
        public List<ZoneSetupViewModel> GetAllZone(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZoneUser> GetAllZoneByUserIdAndZoneId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveZoneUser(List<string> zones, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateZone(List<string> zones, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
