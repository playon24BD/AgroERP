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
    public interface IRawMaterialRequisitionInfoBusiness
    {
        IEnumerable<IssueRequisitionReportData> GetIssueRequisitionReportData(string RawMaterialRequisitionCode);
        IEnumerable<GetSendAndReceiveReportData> GetSendAndReceiveReport(string RawMaterialRequisitionCode);
        
        IEnumerable<RawMaterialRequisitionInfoDTO> GetRawMaterialRequisitionInfos(long orgId,string status);
        IEnumerable<RawMaterialRequisitionInfoDTO> GetRawMaterialRequisitionInfoReceives(long orgId, string status,long RawMaterialRequisitionInfoId);
        RawMaterialRequisitionInfo GetRawMaterialRequisitionInfobyId(long infoId, long orgId);
        IEnumerable<RawMaterialRequisitionInfoDTO> GetAllRawMaterialRequisitionInfos(string RequisitonCode,string status, string fdate, string tdate, long orgId);
        bool SaveRawMaterialRequisition(RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO,long userId, long orgId);
    }
}
