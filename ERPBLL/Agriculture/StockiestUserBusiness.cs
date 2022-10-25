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
    public class StockiestUserBusiness : IStockiestUserBusiness
    {
        public List<StockiestInfoViewModel> GetAllStockiest(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockiestUser> GetAllStockiestByUserIdAndStockiestId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveStockiestUser(List<string> stockiest, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStockiestUser(List<string> stockiest, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
