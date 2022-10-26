using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IStockiestUserBusiness
    {
        bool SaveStockiestUser(List<string> stockiest, long userId, long suserId, long orgId,string action);
        IEnumerable<StockiestUser> GetAllStockiestByUserIdAndStockiestId(long userId, long orgId);
        bool UpdateStockiestUser(List<string> stockiest, long userId, long suserId, long orgId);
        List<StockiestInfoViewModel> GetAllStockiest(long userId, long orgId);
    }
}
