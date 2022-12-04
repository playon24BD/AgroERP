using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IStockiestInfo
    {
        IEnumerable<StockiestInfo> GetAllStockiestSetup(long OrgId);

        StockiestInfo GetStockiestInfoById(long stockiestId, long orgId);
        IEnumerable<StockiestInfoDTO> GetStockiestInfos(long? stockiestId, long? territoryId, long orgId);
        IEnumerable<StockiestInfoDTO> GetStockiestCodess(long orgId);

        bool SaveStockiestList(List<StockiestInfoDTO> infoDTO, long userId, long orgId);
        bool UpdateStockiestList(StockiestInfoDTO updateDTOs, long userId, long orgId);
    }
}
