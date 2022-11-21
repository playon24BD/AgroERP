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
   public interface IFinishGoodProductionInfoBusiness
    {
        IEnumerable<FinishGoodProductionDataReport> GetFinishGoodProductionReport(string ReceipeBatchCode);


        IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProduct(long orgId);

        //IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfosList(long? productId,string finishGoodProductionBatch);
        IEnumerable<FinishGoodProductionInfoDTO> GetReceiveBatchCode(long orgId);
        IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductionInfo(long orgId);
        //FinishGoodProductionInfo GetCheckFinishGoodQuantity(long FinishGoodProductInfoId, long orgId);
        IEnumerable<FinishGoodProductionInfoDTO> GetCheckFinishGoodQuantity(long FinishGoodProductInfoId,int FGRID, long orgId);

        FinishGoodProductionInfo GetProductionInfoById(long id, long orgId);
        FinishGoodProductionInfo GetFinishGoodProductionByAny(string any, long orgId);
        bool SaveFinishGoodInfo(FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, List<FinishGoodProductionDetailsDTO> details, long userId, long orgId);


        IEnumerable<FinishGoodProductionInfoDTO> FinishgoodproductInOutreturnStockInfos(string ReceipeBatchCode,long? productId);


        FinishGoodProductionInfo Getcheckqty(long FinishGoodProductInfoId, string ProductUnitQty);

        IEnumerable<FinishGoodStockReport> GetFinishGoodStockReport(long? productId, string fromDate, string toDate);

        IEnumerable<FinishGoodProductionInfoDTO> GetPendingFinishGoodInfos(string name);

        FinishGoodProductionInfo GetFGProductionInfoBybatchcode(string finishGoodProductionBatch);

        bool UpdateProductionStatus(List<FinishGoodProductionDetailsDTO> finishGoodProductionDetailsDTOs, FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, long userId, long orgId);

        FinishGoodProductionInfo getbatchcodebyid(string FinishGoodProductionBatch);
        FinishGoodProductionDetails getdetailsbatchcodebyid(long FinishGoodProductDetailId);
        MRawMaterialIssueStockDetails getissueidbyrmidprobatch(string FinishGoodProductionBatch, long RawMaterialId);




    }
}
