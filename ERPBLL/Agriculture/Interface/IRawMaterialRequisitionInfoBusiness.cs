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

        IEnumerable<GetSendAndReceiveReportData> GetSendAndReceiveReport(string RawMaterialRequisitionCode);
        
        IEnumerable<RawMaterialRequisitionInfo> GetRawMaterialRequisitionInfos(long orgId);
        RawMaterialRequisitionInfo GetRawMaterialRequisitionInfobyId(long infoId, long orgId);
        IEnumerable<RawMaterialRequisitionInfoDTO> GetAllRawMaterialRequisitionInfos(string RequisitonCode,string status, string fdate, string tdate, long orgId);
        bool SaveRawMaterialRequisition(RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO,long userId, long orgId);
    }
}
