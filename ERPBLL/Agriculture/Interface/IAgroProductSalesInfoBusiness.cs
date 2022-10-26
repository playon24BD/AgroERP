using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAgroProductSalesInfoBusiness
    {
        IEnumerable<ProductSalesDataReport> GetProductSalesData();
        IEnumerable<AgroProductSalesInfoDTO> GetAgroSalesInfos(long orgId, long? ProductId);
        IEnumerable<AgroProductSalesInfo> GetUserName(long orgId);
        AgroProductSalesInfo GetAgroProductionInfoById(long id, long orgId);
        IEnumerable<AgroProductSalesInfo> GetAgroProductionSalesInfo(long orgId);
        bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId);
    }
}
