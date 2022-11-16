using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using ERPBO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IReturnRawMaterialBusiness
    {
        IEnumerable<ReturnRawMaterialDTO> GetReturnReportList(long? rawMaterialId, string returnType, string status, string fromDate, string toDate);
        IEnumerable<ReturnRawMaterialDataReport> GetReturnRawMaterialDataReport(long? rawMaterialId, string returnType, string status,string fromDate,string toDate);
        
        IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialInfosId(long rawMaterialId);

        IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialSearch();
        IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialSearch1();
        IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialSearch2();

        List<AgroDropdown> GetIssueRawMaterials(long orgId);

        bool SaveRawMaterialReturnInfo(List<ReturnRawMaterialDTO> detailsDTO, long userId, long orgId);


        IEnumerable<ReturnRawMaterial> GetAllReturnRawMaterial();

        IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialInfos(string name);

        IEnumerable<ReturnRawMaterial> GetReturnRawMaterialBYRMId(long Id, /*string ReturnType,*/ string Status);

        bool updateReturnStatus(List<ReturnRawMaterialDTO> returnRawMaterialDTOs, long userId, long orgId);


        ReturnRawMaterial GetReturnsById(long ReturnRawMaterialId);

        IEnumerable<ReturnRawMaterialDTO> GetallReturns( long? rawMaterialId, string returnType,string status);





    }
}
