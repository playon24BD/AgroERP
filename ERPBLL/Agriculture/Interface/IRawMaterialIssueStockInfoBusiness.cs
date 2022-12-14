using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRawMaterialIssueStockInfoBusiness
    {
        IEnumerable<RawMaterialIssueStockInfoDTO> GetRawMaterialIssueStockInfos(long orgId, long? rawMaterialId);


        RawMaterialIssueStockInfo GetRawMaterialIssueStockById(long id, long orgId);

        RawMaterialIssueStockInfo RawMaterialStockIssueInfobyRawMaterialid(long rawMaterialId, long orgId);



        RawMaterialIssueStockInfo GetRawMaterialIssueStockUnitById(long id, long orgId);
        RawMaterialIssueStockInfo GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId);



       
        //IEnumerable<RawMaterialIssueStockInfo> GetRawMaterialIssueStockUnitById(long id, long orgId);



        bool SaveProductIssueRawMaterialStock(RawMaterialIssueStockInfoDTO info, List<RawMaterialIssueStockDetailsDTO> details, long userId, long orgId);
        bool UpdateProductIssueRawMaterialStock(List<RawMaterialIssueStockInfoDTO> issueStockInfoDTOs);

        bool DeleteRawMaterialIssueStock(long id, long userId, long orgId);
        IEnumerable<RawMaterialIssueStockInfoDTO> RawMaterialStockIssueMinQty(string RawMaterialIdList, long orgId);
        bool UpdateRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO mRawMaterialIssueStockInfoDTO, long userId);
    }
}
