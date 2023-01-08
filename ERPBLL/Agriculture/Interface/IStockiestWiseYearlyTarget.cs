using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IStockiestWiseYearlyTarget
    {

        IEnumerable<StockiestWiseYearlyTargetDTO> GetYearlyTargetList(long? territoryId, long? stockiestId, long? productId, string fromDate, string toDate);
        bool SaveStockiestWiseYearlyTargetList(List<StockiestWiseYearlyTargetDTO> detailsDTO, long userId, long orgId);
    }
}
