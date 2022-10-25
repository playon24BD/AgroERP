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
    public class TerritoryUserBusiness : ITerritoryUserBusiness
    {
        public List<TerritorySetupViewModel> GetAllTerritory(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TerritoryUser> GetAllTerritoryByUserIdAndTerritory(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveTerritoryUser(List<string> territories, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTerritoryUser(List<string> territories, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
