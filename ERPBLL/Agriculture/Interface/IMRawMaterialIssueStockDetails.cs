using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IMRawMaterialIssueStockDetails
    {
        
        IEnumerable<MRawMaterialIssueStockDetails> GetRawMatwrialissueDetailsByInfoId(long infoId,string Status);
        IEnumerable<MRawMaterialIssueStockDetails> RawMaterialStockIssueInfobyRawMaterialid(long rawMaterialId, long orgId);
        IEnumerable<MRawMaterialIssueStockDetails> RawMaterialStockIssueInfobyRawMaterialidOut(long rawMaterialId, long orgId);
        bool SaveRawMaterialIssueDetails(List<MRawMaterialIssueStockDetailsDTO> finishGoodProductionDetailsDTOs, long userId, long orgId);
        MRawMaterialIssueStockDetails GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId);

        IEnumerable<MRawMaterialIssueStockDetails> GetAllRawMaterialIssueStock();

        IEnumerable<MRawMaterialIssueStockDetailsDTO> GetIssueInOutInfos(string name);

    }
}
