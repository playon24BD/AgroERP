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
    public interface IMRawMaterialIssueStockInfo
    {
        IEnumerable<MRawMaterialDataReport> MRawMaterialReport(long? rawMaterialId);
        
        IEnumerable<MRawMaterialIssueStockInfo> GetAllRawMaterialIssue(long OrgId);

        bool SaveRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO info, List<MRawMaterialIssueStockDetailsDTO> details, long userId, long orgId);

        MRawMaterialIssueStockInfo GetRawmaterialIssueInfoOneById(long id, long orgId);
        MRawMaterialIssueStockInfo GetRawMaterialIssueStockByMeterialId(long rawMaterialId, long orgId);

        //bool UpdateProductIssueRawMaterialStock(List<MRawMaterialIssueStockInfoDTO> issueStockInfoDTOs);
        bool SaveRawMaterialIssueStockWithRequistion(RawMaterialRequisitionInfoDTO info, List<RawMaterialRequisitionDetailsDTO> details, long userId, long orgId);


        IEnumerable<MRawMaterialIssueStockInfo> GetAllRawMaterialIssueforaccept();

        MRawMaterialIssueStockInfo GetRawmaterialIssueInfoByProbatch(string ProductBatchCode);

        bool UpdateRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO info, List<MRawMaterialIssueStockDetailsDTO> details, long userId);


        MRawMaterialIssueStockDetails GetRawmaterialIssueDetailsByISSueadndRMid(long RawMaterialIssueStockId, long RawMaterialId);
        RawMaterialTrack GetRawmaterialTrkDetailsByISSueadndRMid(long RawMaterialIssueStockId, long RawMaterialId);


    }
}
