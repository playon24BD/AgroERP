using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IMRawMaterialIssueStockInfo
    {
        IEnumerable<MRawMaterialIssueStockInfo> GetAllRawMaterialIssue(long OrgId);

        bool SaveRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO info, List<MRawMaterialIssueStockDetailsDTO> details, long userId, long orgId);

        MRawMaterialIssueStockInfo GetRawmaterialIssueInfoOneById(long id, long orgId);
        MRawMaterialIssueStockInfo GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId);

        //bool UpdateProductIssueRawMaterialStock(List<MRawMaterialIssueStockInfoDTO> issueStockInfoDTOs);
        bool SaveRawMaterialIssueStockWithRequistion(RawMaterialRequisitionInfoDTO info, List<RawMaterialRequisitionDetailsDTO> details, long userId, long orgId);

    }
}
