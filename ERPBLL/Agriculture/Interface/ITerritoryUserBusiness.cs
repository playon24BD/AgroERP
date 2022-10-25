using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface ITerritoryUserBusiness
    {
        bool SaveTerritoryUser(List<string> territories, long userId, long suserId, long orgId);
        IEnumerable<TerritoryUser> GetAllTerritoryByUserIdAndTerritory(long userId, long orgId);
        bool UpdateTerritoryUser(List<string> territories, long userId, long suserId, long orgId);
        List<TerritorySetupViewModel> GetAllTerritory(long userId, long orgId);
    }
}
