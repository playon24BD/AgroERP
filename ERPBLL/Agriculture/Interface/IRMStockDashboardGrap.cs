using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRMStockDashboardGrap
    {
        IEnumerable<RawMaterialTrackDTO> GetMainStockRMName(long orgId);
        IEnumerable<RawMaterialTrackDTO> GetMainStockRMCurrentStock(long orgId);

        IEnumerable<FinishGoodProductionInfoDTO> GetMainStockFGProductName(long orgId);
        IEnumerable<FinishGoodProductionInfoDTO> GetMainStockFGProductCurrentStock(long orgId);
        IEnumerable<Last30DaysSalesGraph> Last30DaysSellsChart(string fromDate, string toDate, long orgId);
        IEnumerable<Last30DaysPaymentGraph> Last30DaysPaymentChart(string fromDate, string toDate, long orgId);
        IEnumerable<GetAllMonthSalesTerget> GetMonth(long orgId);
        IEnumerable<GetAllMonthSalesTerget> GetMonthsales(long orgId);


    }
}
